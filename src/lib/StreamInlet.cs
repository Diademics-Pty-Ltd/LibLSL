using System;
using System.Runtime.InteropServices;

namespace LSL
{
    public class StreamInlet : LSLObject
    {
        private PostProcessingOptions _postProcessingOptions;
        public Action<double> OnGotNewSample { get; set; }

        public Action<int> OnGotNewChunk { get; set; }

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

        public StreamInlet(StreamInfo info, int maxBufferLength = 360, int maxChunkLength = 0, bool recover = true, PostProcessingOptions postProcessingOptions = PostProcessingOptions.None)
            : base(DllHandler.lsl_create_inlet(info.DangerousGetHandle, maxBufferLength, maxChunkLength, recover ? 1 : 0))
        {
            PostProcessingOptions = postProcessingOptions;
        }

        public StreamInfo Info(double timeout = LSLConstants.Forever)
        {
            int ec = 0;
            IntPtr res = DllHandler.lsl_get_fullinfo(Obj, timeout, ref ec);
            Error.Check(ec);
            return new(res);
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
                    timestamp = DllHandler.lsl_pull_sample_d(Obj, castData, castData.Length,  timeout, ref ec);
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
            }
            Error.Check(ec);
            return timestamp;
        }

        public double PullSample(string[] sample, double timeout = LSLConstants.Forever)
        {
            int ec = 0;
            IntPtr[] tmp = new IntPtr[sample.Length];
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
            }
            Error.Check(ec);
            return (int)res / data.GetLength(1);
        }
        
        protected override void DestroyLSLObject(IntPtr obj) => DllHandler.lsl_destroy_inlet(obj);
    }
}
