using System;
using System.Threading.Tasks;

namespace LSL.Internal
{
    internal class StreamOutlet : LSLObject, IStreamOutlet
    {
        public Action<bool> OnGotConsumer { get; set; }

        public bool HaveConsumers => DllHandler.lsl_have_consumers(Obj) > 0;

        public IStreamInfo Info => StreamInfoFactory.Create(DllHandler.lsl_get_info(Obj));

        public bool WaitForConsumers(double timeout = Constants.Forever) => (DllHandler.lsl_wait_for_consumers(Obj, timeout) > 0);

        public StreamOutlet(IStreamInfo info, int chunkSize = 0, int maxBuffered = 360)
            : base(DllHandler.lsl_create_outlet(info.DangerousGetHandle, chunkSize, maxBuffered)) { }

        protected override void DestroyLSLObject(IntPtr obj)
        {
            DllHandler.lsl_destroy_outlet(obj);
        }

        public void PushSample(float[] data, double timestamp = 0.0, bool pushthrough = true)
        { 
            Error.Check( DllHandler.lsl_push_sample_ftp(Obj, data, timestamp, pushthrough ? 1 : 0));
        }

        public void PushSample(double[] data, double timestamp = 0.0, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_sample_dtp(Obj, data, timestamp, pushthrough ? 1 : 0));
        }

        public void PushSample(int[] data, double timestamp = 0.0, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_sample_itp(Obj, data, timestamp, pushthrough ? 1 : 0));
        }

        public void PushSample(short[] data, double timestamp = 0.0, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_sample_stp(Obj, data, timestamp, pushthrough ? 1 : 0));
        }

        public void PushSample(char[] data, double timestamp = 0.0, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_sample_ctp(Obj, data, timestamp, pushthrough ? 1 : 0));
        }

        public void PushSample(string[] data, double timestamp = 0.0, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_sample_strtp(Obj, data, timestamp, pushthrough ? 1 : 0));
        }

        public void PushChunk(float[,] data, double timestamp = 0.0, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_chunk_ftp(Obj, data, (uint)data.Length, timestamp, pushthrough ? 1 : 0));
        }

        public void PushChunk(double[,] data, double timestamp = 0.0, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_chunk_dtp(Obj, data, (uint)data.Length, timestamp, pushthrough ? 1 : 0));
        }

        public void PushChunk(int[,] data, double timestamp = 0.0, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_chunk_itp(Obj, data, (uint)data.Length, timestamp, pushthrough ? 1 : 0));
        }

        public void PushChunk(short[,] data, double timestamp = 0.0, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_chunk_stp(Obj, data, (uint)data.Length, timestamp, pushthrough ? 1 : 0));
        }

        public void PushChunk(char[,] data, double timestamp = 0.0, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_chunk_ctp(Obj, data, (uint)data.Length, timestamp, pushthrough ? 1 : 0));
        }

        public void PushChunk(string[,] data, double timestamp = 0.0, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_chunk_strtp(Obj, data, (uint)data.Length, timestamp, pushthrough ? 1 : 0));
        }

        public void PushChunk(float[,] data, double[] timestamps, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_chunk_ftnp(Obj, data, (uint)data.Length, timestamps, pushthrough ? 1 : 0));
        }

        public void PushChunk(double[,] data, double[] timestamps, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_chunk_dtnp(Obj, data, (uint)data.Length, timestamps, pushthrough ? 1 : 0));
        }

        public void PushChunk(int[,] data, double[] timestamps, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_chunk_itnp(Obj, data, (uint)data.Length, timestamps, pushthrough ? 1 : 0));
        }

        public void PushChunk(short[,] data, double[] timestamps, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_chunk_stnp(Obj, data, (uint)data.Length, timestamps, pushthrough ? 1 : 0));
        }

        public void PushChunk(char[,] data, double[] timestamps, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_chunk_ctnp(Obj, data, (uint)data.Length, timestamps, pushthrough ? 1 : 0));
        }

        public void PushChunk(string[,] data, double[] timestamps, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_chunk_strtnp(Obj, data, (uint)data.Length, timestamps, pushthrough ? 1 : 0));
        }

        //public async Task WaitForConsumersAsync(double timeout = Constants.Forever)
        //{
        //    int consumers = 0;
        //    _ = await Task.Run(() => consumers = DllHandler.lsl_wait_for_consumers(Obj, timeout));
        //    // do something with consumers
        //}
    }
}
