using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSL.Internal
{
    internal class StreamOutlet : LSLObject, IStreamOutlet
    {
        public bool HaveConsumers => (DllHandler.lsl_have_consumers(Obj) > 0);
        public bool WaitForConsumers(double timeout = Constants.Forever) => (DllHandler.lsl_wait_for_consumers(Obj, timeout) > 0);
        public IStreamInfo Info => StreamInfoFactory.Create(DllHandler.lsl_get_info(Obj));



        public StreamOutlet(IStreamInfo info, int chunkSize = 0, int maxBuffered = 360)
            : base(DllHandler.lsl_create_outlet(info.DangerousGetHandle, chunkSize, maxBuffered)) { }

        protected override void DestroyLSLObject(IntPtr obj)
        {
            DllHandler.lsl_destroy_outlet(obj);
        }

        public int PushSample(float[] data, double timestamp = 0.0, bool pushthrough = true) =>
            DllHandler.lsl_push_sample_ftp(Obj, data, timestamp, pushthrough ? 1 : 0);
        public int PushSample(double[] data, double timestamp = 0.0, bool pushthrough = true) =>
            DllHandler.lsl_push_sample_dtp(Obj, data, timestamp, pushthrough ? 1 : 0);
        public int PushSample(int[] data, double timestamp = 0.0, bool pushthrough = true) =>
            DllHandler.lsl_push_sample_itp(Obj, data, timestamp, pushthrough ? 1 : 0);
        public int PushSample(short[] data, double timestamp = 0.0, bool pushthrough = true) =>
            DllHandler.lsl_push_sample_stp(Obj, data, timestamp, pushthrough ? 1 : 0);
        public int PushSample(char[] data, double timestamp = 0.0, bool pushthrough = true) =>
            DllHandler.lsl_push_sample_ctp(Obj, data, timestamp, pushthrough ? 1 : 0);
        public int PushSample(string[] data, double timestamp = 0.0, bool pushthrough = true) =>
            DllHandler.lsl_push_sample_strtp(Obj, data, timestamp, pushthrough ? 1 : 0);

        public int PushChunk(float[,] data, double timestamp = 0.0, bool pushthrough = true) =>
            DllHandler.lsl_push_chunk_ftp(Obj, data, (uint)data.Length, timestamp, pushthrough ? 1 : 0);
        public int PushChunk(double[,] data, double timestamp = 0.0, bool pushthrough = true) =>
            DllHandler.lsl_push_chunk_dtp(Obj, data, (uint)data.Length, timestamp, pushthrough ? 1 : 0);
        public int PushChunk(int[,] data, double timestamp = 0.0, bool pushthrough = true) =>
            DllHandler.lsl_push_chunk_itp(Obj, data, (uint)data.Length, timestamp, pushthrough ? 1 : 0);
        public int PushChunk(short[,] data, double timestamp = 0.0, bool pushthrough = true) =>
            DllHandler.lsl_push_chunk_stp(Obj, data, (uint)data.Length, timestamp, pushthrough ? 1 : 0);
        public int PushChunk(char[,] data, double timestamp = 0.0, bool pushthrough = true) =>
            DllHandler.lsl_push_chunk_ctp(Obj, data, (uint)data.Length, timestamp, pushthrough ? 1 : 0);
        public int PushChunk(string[,] data, double timestamp = 0.0, bool pushthrough = true) =>
            DllHandler.lsl_push_chunk_strtp(Obj, data, (uint)data.Length, timestamp, pushthrough ? 1 : 0);

        public int PushChunk(float[,] data, double[] timestamps, bool pushthrough = true) =>
            DllHandler.lsl_push_chunk_ftnp(Obj, data, (uint)data.Length, timestamps, pushthrough ? 1 : 0);
        public int PushChunk(double[,] data, double[] timestamps, bool pushthrough = true) =>
            DllHandler.lsl_push_chunk_dtnp(Obj, data, (uint)data.Length, timestamps, pushthrough ? 1 : 0);
        public int PushChunk(int[,] data, double[] timestamps, bool pushthrough = true) =>
            DllHandler.lsl_push_chunk_itnp(Obj, data, (uint)data.Length, timestamps, pushthrough ? 1 : 0);
        public int PushChunk(short[,] data, double[] timestamps, bool pushthrough = true) =>
            DllHandler.lsl_push_chunk_stnp(Obj, data, (uint)data.Length, timestamps, pushthrough ? 1 : 0);
        public int PushChunk(char[,] data, double[] timestamps, bool pushthrough = true) =>
            DllHandler.lsl_push_chunk_ctnp(Obj, data, (uint)data.Length, timestamps, pushthrough ? 1 : 0);
        public int PushChunk(string[,] data, double[] timestamps, bool pushthrough = true) =>
            DllHandler.lsl_push_chunk_strtnp(Obj, data, (uint)data.Length, timestamps, pushthrough ? 1 : 0);

        public async Task WaitForConsumersAsync(double timeout = Constants.Forever)
        {
            int consumers = 0;
            await Task.Run(() => consumers = DllHandler.lsl_wait_for_consumers(Obj, timeout));
            // do something with consumers
        }
    }
}
