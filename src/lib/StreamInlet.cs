using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using LibLSL.Internal;
using System.Linq;

namespace LibLSL
{
    public class StreamInlet : LSLObject
    {
        private PostProcessingOptions _postProcessingOptions;
        private readonly GenericArrays _sampleBuffer;
        private readonly int _maxChunkLength;

        public int SamplesAvailable => (int)DllHandler.lsl_samples_available(Obj);
        public bool WasClockReset => DllHandler.lsl_was_clock_reset(Obj) != 0;

        public PostProcessingOptions PostProcessingOptions
        {
            get => _postProcessingOptions;
            set
            {
                Error.Check(DllHandler.lsl_set_postprocessing(Obj, value));
                _postProcessingOptions = value;
            }
        }

        public StreamInfo StreamInfo { get; }

        public StreamInlet(StreamInfo streamInfo, int maxBufferLength = 360, int maxChunkLength = 0, bool recover = true, PostProcessingOptions postProcessingOptions = PostProcessingOptions.None, double timeout = LSLConstants.Forever)
            : base(DllHandler.lsl_create_inlet(streamInfo.DangerousGetHandle, maxBufferLength, maxChunkLength, recover ? 1 : 0))
        {
            int ec = 0;
            IntPtr res = DllHandler.lsl_get_fullinfo(Obj, timeout, ref ec);
            Error.Check(ec);
            StreamInfo = new(res);
            PostProcessingOptions = postProcessingOptions;
            _sampleBuffer = new(streamInfo.Channels * streamInfo.ChannelFormat.Size);
            _maxChunkLength = maxChunkLength;
        }

        public void OpenStream(double timeout = LSLConstants.Forever)
        {
            int ec = 0;
            DllHandler.lsl_open_stream(Obj, timeout, ref ec);
            Error.Check(ec);
        }

        public void CloseStream() => DllHandler.lsl_close_stream(Obj);

        public double TimeCorrection(double timeout = LSLConstants.Forever)
        {
            int ec = 0;
            double timeCorrection = DllHandler.lsl_time_correction(Obj, timeout, ref ec);
            Error.Check(ec);
            return timeCorrection;
        }

        // this overload is only used internally in the case that we are using lists instead of arrays
        // the genericSampleBuffer
        private double PullSample(double timeout)
        {
            int ec = 0;
            double timestamp = 0.0;

            switch (StreamInfo.ChannelFormat.Type)
            {
                case ChannelFormatType.FloatThirtyTwo:
                    timestamp = DllHandler.lsl_pull_sample_f(Obj, _sampleBuffer.FloatBuffer, _sampleBuffer.FloatBufferCount, timeout, ref ec);
                    break;
                case ChannelFormatType.DoubleSixtyFour:
                    timestamp = DllHandler.lsl_pull_sample_d(Obj, _sampleBuffer.DoubleBuffer, _sampleBuffer.DoubleBufferCount, timeout, ref ec);
                    break;
                case ChannelFormatType.IntThirtyTwo:
                    timestamp = DllHandler.lsl_pull_sample_i(Obj, _sampleBuffer.IntBuffer, _sampleBuffer.IntBufferCount, timeout, ref ec);
                    break;
                case ChannelFormatType.IntSixteen:
                    timestamp = DllHandler.lsl_pull_sample_s(Obj, _sampleBuffer.ShortBuffer, _sampleBuffer.ShortBufferCount, timeout, ref ec);
                    break;
                case ChannelFormatType.IntEight:
                    timestamp = DllHandler.lsl_pull_sample_c(Obj, _sampleBuffer.CharBuffer, _sampleBuffer.CharBufferCount, timeout, ref ec);
                    break;
                default:
                    break;
            }
            Error.Check(ec);
            return timestamp;
        }

        public double PullSample<T>(List<T> sample, double timeout = LSLConstants.Forever) where T : struct
        {
            sample.Clear();
            double timestamp;
            try
            {
                timestamp = PullSample(timeout);
            }
            catch (InternalException ex)
            {
                throw (new InternalException(ex.Message));
            }
            for (int ch = 0; ch < StreamInfo.Channels; ch++)
            {
                switch (sample)
                {
                    case List<double>:
                        sample.Add((T)(object)BitConverter.ToDouble(_sampleBuffer, ch * StreamInfo.ChannelFormat.Size));
                        break;
                    case List<float>:
                        sample.Add((T)(object)BitConverter.ToSingle(_sampleBuffer, ch * StreamInfo.ChannelFormat.Size));
                        break;
                    case List<int>:
                        sample.Add((T)(object)BitConverter.ToInt32(_sampleBuffer, ch * StreamInfo.ChannelFormat.Size));
                        break;
                    case List<short>:
                        sample.Add((T)(object)BitConverter.ToInt16(_sampleBuffer, ch * StreamInfo.ChannelFormat.Size));
                        break;
                    case List<char>:
                        sample.Add((T)(object)BitConverter.ToChar(_sampleBuffer, ch * StreamInfo.ChannelFormat.Size));
                        break;
                }
            }
            return timestamp;
        }


        public double PullSample<T>(T[] sample, double timeout = LSLConstants.Forever) where T : struct
        {
            int ec = 0;
            double timestamp = 0.0;
            switch (sample)
            {
                case float[] castData:
                    timestamp = DllHandler.lsl_pull_sample_f(Obj, castData, castData.Length, timeout, ref ec);
                    break;
                case double[] castData:
                    timestamp = DllHandler.lsl_pull_sample_d(Obj, castData, castData.Length, timeout, ref ec);
                    break;
                case int[] castData:
                    timestamp = DllHandler.lsl_pull_sample_i(Obj, castData, castData.Length, timeout, ref ec);
                    break;
                case short[] castData:
                    timestamp = DllHandler.lsl_pull_sample_s(Obj, castData, castData.Length, timeout, ref ec);
                    break;
                case char[] castData:
                    timestamp = DllHandler.lsl_pull_sample_c(Obj, castData, castData.Length, timeout, ref ec);
                    break;
                default:
                    break;
            }
            Error.Check(ec);
            return timestamp;
        }

        public double PullSample(string[] sample, double timeout = LSLConstants.Forever)
        {
            int ec = 0;
            var tmp = new IntPtr[sample.Length];
            double timestamp = DllHandler.lsl_pull_sample_str(Obj, tmp, tmp.Length, timeout, ref ec);
            Error.Check(ec);
            try
            {
                for (int k = 0; k < tmp.Length; k++)
                    sample[k] = Marshal.PtrToStringAnsi(tmp[k]);
            }
            finally
            {
                for (int k = 0; k < tmp.Length; k++)
                    DllHandler.lsl_destroy_string(tmp[k]);
            }
            return timestamp;
        }

        public void PullChunk<T>(List<List<T>> data, List<double> timestamps, double timeout = 0.0) where T : struct
        {
            try
            {
                data.Clear();
                List<T> sample = new();
                timestamps.Clear();
                int counter = 0;
                double timestamp;
                while ((timestamp = PullSample(sample, timeout)) != 0)
                {
                    data.Add(sample);
                    timestamps.Add(timestamp);
                    counter++;
                    if (counter == _maxChunkLength && _maxChunkLength != 0) break;
                }
            }
            catch (InternalException ex)
            {
                throw (new InternalException(ex.Message));
            }
        }

        public int PullChunk<T>(T[,] data, double[] timestamps, double timeout = 0.0) where T : struct
        {
            int ec = 0;
            uint res = 0;
            switch (data)
            {
                case float[,] castData:
                    res = DllHandler.lsl_pull_chunk_f(Obj, castData, timestamps, (uint)castData.Length, (uint)timestamps.Length, timeout, ref ec);
                    break;
                case double[,] castData:
                    res = DllHandler.lsl_pull_chunk_d(Obj, castData, timestamps, (uint)castData.Length, (uint)timestamps.Length, timeout, ref ec);
                    break;
                case int[,] castData:
                    res = DllHandler.lsl_pull_chunk_i(Obj, castData, timestamps, (uint)castData.Length, (uint)timestamps.Length, timeout, ref ec);
                    break;
                case short[,] castData:
                    res = DllHandler.lsl_pull_chunk_s(Obj, castData, timestamps, (uint)castData.Length, (uint)timestamps.Length, timeout, ref ec);
                    break;
                case char[,] castData:
                    res = DllHandler.lsl_pull_chunk_c(Obj, castData, timestamps, (uint)castData.Length, (uint)timestamps.Length, timeout, ref ec);
                    break;
                default:
                    break;
            }
            Error.Check(ec);
            return (int)res / data.GetLength(1);
        }

        protected override void DestroyLSLObject(IntPtr obj) => DllHandler.lsl_destroy_inlet(obj);
    }
}
