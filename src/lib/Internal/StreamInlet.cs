using System;
using System.Runtime.InteropServices;

namespace LSL.Internal
{
    internal class StreamInlet : LSLObject, IStreamInlet
    {

        public int SamplesAvailable => (int)DllHandler.lsl_samples_available(Obj);
        public bool WasClockReset => (DllHandler.lsl_was_clock_reset(Obj) != 0);

        public StreamInlet(IStreamInfo info, int maxBufferLength = 360, int maxChunkLength = 0, bool recover = true, PostProcessingOptions postProcessingOptions = PostProcessingOptions.None)
            : base(DllHandler.lsl_create_inlet(info.DangerousGetHandle, maxBufferLength, maxChunkLength, recover ? 1 : 0))
        {
            _ = DllHandler.lsl_set_postprocessing(Obj, postProcessingOptions);
        }

        public IStreamInfo Info(double timeout = Constants.Forever)
        {
            int ec = 0; 
            IntPtr res = DllHandler.lsl_get_fullinfo(Obj, timeout, ref ec); 
            Error.Check(ec); 
            return StreamInfoFactory.Create(res);
        }

        public void OpenStream(double timeout = Constants.Forever)
        {
            int ec = 0;
            DllHandler.lsl_open_stream(Obj, timeout, ref ec);
            Error.Check(ec);
        }

        public void CloseStream() => DllHandler.lsl_close_stream(Obj);

        public double TimeCorrection(double timeout = Constants.Forever)
        {
            int ec = 0;
            double timeCorrection = DllHandler.lsl_time_correction(Obj, timeout, ref ec);
            Error.Check(ec);
            return timeCorrection;
        }

        public double PullSample(float[] sample, double timeout = Constants.Forever) 
        { 
            int ec = 0; 
            double timestamp = DllHandler.lsl_pull_sample_f(Obj, sample, sample.Length, timeout, ref ec); 
            Error.Check(ec); 
            return timestamp; 
        }

        public double PullSample(double[] sample, double timeout = Constants.Forever)
        {
            int ec = 0;
            double timestamp = DllHandler.lsl_pull_sample_d(Obj, sample, sample.Length, timeout, ref ec);
            Error.Check(ec);
            return timestamp;
        }

        public double PullSample(int[] sample, double timeout = Constants.Forever)
        {
            int ec = 0;
            double timestamp = DllHandler.lsl_pull_sample_i(Obj, sample, sample.Length, timeout, ref ec);
            Error.Check(ec);
            return timestamp;
        }

        public double PullSample(short[] sample, double timeout = Constants.Forever)
        {
            int ec = 0;
            double timestamp = DllHandler.lsl_pull_sample_s(Obj, sample, sample.Length, timeout, ref ec);
            Error.Check(ec);
            return timestamp;
        }

        public double PullSample(char[] sample, double timeout = Constants.Forever)
        {
            int ec = 0;
            double timestamp = DllHandler.lsl_pull_sample_c(Obj, sample, sample.Length, timeout, ref ec);
            Error.Check(ec);
            return timestamp;
        }

        public double PullSample(string[] sample, double timeout = Constants.Forever)
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

        public int PullChunk(float[,] data, double[] timestamps, double timeout = 0.0) 
        { 
            int ec = 0; 
            uint res = DllHandler.lsl_pull_chunk_f(Obj, data, timestamps, (uint)data.Length, (uint)timestamps.Length, timeout, ref ec); 
            Error.Check(ec); 
            return (int)res / data.GetLength(1); 
        }
        public int PullChunk(double[,] data, double[] timestamps, double timeout = 0.0)
        {
            int ec = 0;
            uint res = DllHandler.lsl_pull_chunk_d(Obj, data, timestamps, (uint)data.Length, (uint)timestamps.Length, timeout, ref ec);
            Error.Check(ec);
            return (int)res / data.GetLength(1);
        }
        public int PullChunk(int[,] data, double[] timestamps, double timeout = 0.0)
        {
            int ec = 0;
            uint res = DllHandler.lsl_pull_chunk_i(Obj, data, timestamps, (uint)data.Length, (uint)timestamps.Length, timeout, ref ec);
            Error.Check(ec);
            return (int)res / data.GetLength(1);
        }
        public int PullChunk(short[,] data, double[] timestamps, double timeout = 0.0)
        {
            int ec = 0;
            uint res = DllHandler.lsl_pull_chunk_s(Obj, data, timestamps, (uint)data.Length, (uint)timestamps.Length, timeout, ref ec);
            Error.Check(ec);
            return (int)res / data.GetLength(1);
        }
        public int PullChunk(char[,] data, double[] timestamps, double timeout = 0.0)
        {
            int ec = 0;
            uint res = DllHandler.lsl_pull_chunk_c(Obj, data, timestamps, (uint)data.Length, (uint)timestamps.Length, timeout, ref ec);
            Error.Check(ec);
            return (int)res / data.GetLength(1);
        }
        public int PullChunk(string[,] data, double[] timestamps, double timeout = 0.0)
        {
            int ec = 0;
            IntPtr[,] tmp = new IntPtr[data.GetLength(0), data.GetLength(1)];
            uint res = DllHandler.lsl_pull_chunk_str(Obj, tmp, timestamps, (uint)data.Length, (uint)timestamps.Length, timeout, ref ec);
            try
            {
                for (int s = 0; s < tmp.GetLength(0); s++)
                    for (int c = 0; c < tmp.GetLength(1); c++)
                        data[s, c] = Marshal.PtrToStringAnsi(tmp[s, c]);
            }
            finally
            {
                for (int s = 0; s < tmp.GetLength(0); s++)
                    for (int c = 0; c < tmp.GetLength(1); c++)
                        DllHandler.lsl_destroy_string(tmp[s, c]);
            }
            Error.Check(ec);
            return (int)res / data.GetLength(1);
        }

        protected override void DestroyLSLObject(IntPtr obj)
        {
            DllHandler.lsl_destroy_inlet(obj);
        }
    }
}
