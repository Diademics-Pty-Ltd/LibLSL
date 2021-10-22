using System;

namespace LSL
{
    public sealed class StreamOutlet : LSLObject
    {
        public Action<bool> OnGotConsumer { get; set; }

        public bool HaveConsumers => DllHandler.lsl_have_consumers(Obj) > 0;

        public StreamInfo Info => new(DllHandler.lsl_get_info(Obj));

        public bool WaitForConsumers(double timeout = Constants.Forever) => DllHandler.lsl_wait_for_consumers(Obj, timeout) > 0;

        public StreamOutlet(StreamInfo info, int chunkSize = 0, int maxBuffered = 360)
            : base(DllHandler.lsl_create_outlet(info.DangerousGetHandle, chunkSize, maxBuffered)) { }

        protected override void DestroyLSLObject(IntPtr obj)
        {
            DllHandler.lsl_destroy_outlet(obj);
        }

        public void PushSample<T>(T[] data, double timestamp = 0.0, bool pushthrough = true) where T : struct
        {
            switch (data)
            {
                case float[] floatData:
                    Error.Check(DllHandler.lsl_push_sample_ftp(Obj, floatData, timestamp, pushthrough ? 1 : 0));
                    break;
                case double[] doubleData:
                    Error.Check(DllHandler.lsl_push_sample_dtp(Obj, doubleData, timestamp, pushthrough ? 1 : 0));
                    break;
                case int[] intData:
                    Error.Check(DllHandler.lsl_push_sample_itp(Obj, intData, timestamp, pushthrough ? 1 : 0));
                    break;
                case short[] shortData:
                    Error.Check(DllHandler.lsl_push_sample_stp(Obj, shortData, timestamp, pushthrough ? 1 : 0));
                    break;
                case char[] charData:
                    Error.Check(DllHandler.lsl_push_sample_ctp(Obj, charData, timestamp, pushthrough ? 1 : 0));
                    break;
                default:
                    break;
            }
        }

        public void PushSample(string[] data, double timestamp = 0.0, bool pushthrough = true) =>
            Error.Check(DllHandler.lsl_push_sample_strtp(Obj, data, timestamp, pushthrough ? 1 : 0));

        public void PushChunk<T>(T[,] data, double timestamp = 0.0, bool pushthrough = true) where T : struct
        {
            switch (data)
            {
                case float[,] floatData:
                    Error.Check(DllHandler.lsl_push_chunk_ftp(Obj, floatData, (uint)data.Length, timestamp, pushthrough ? 1 : 0));
                    break;
                case double[,] doubleData:
                    Error.Check(DllHandler.lsl_push_chunk_dtp(Obj, doubleData, (uint)data.Length, timestamp, pushthrough ? 1 : 0));
                    break;
                case int[,] intData:
                    Error.Check(DllHandler.lsl_push_chunk_itp(Obj, intData, (uint)data.Length, timestamp, pushthrough ? 1 : 0));
                    break;
                case short[,] shortData:
                    Error.Check(DllHandler.lsl_push_chunk_stp(Obj, shortData, (uint)data.Length, timestamp, pushthrough ? 1 : 0));
                    break;
                case char[,] charData:
                    Error.Check(DllHandler.lsl_push_chunk_ctp(Obj, charData, (uint)data.Length, timestamp, pushthrough ? 1 : 0));
                    break;
                default:
                    break;
            }
        }

        public void PushChunk(string[,] data, double timestamp = 0.0, bool pushthrough = true)
        {
            Error.Check(DllHandler.lsl_push_chunk_strtp(Obj, data, (uint)data.Length, timestamp, pushthrough ? 1 : 0));
        }

        public void PushChunk<T>(T[,] data, double[] timestamps, bool pushthrough = true) where T : struct
        {
            switch (data)
            {
                case float[,] floatData:
                    Error.Check(DllHandler.lsl_push_chunk_ftnp(Obj, floatData, (uint)data.Length, timestamps, pushthrough ? 1 : 0));
                    break;
                case double[,] doubleData:
                    Error.Check(DllHandler.lsl_push_chunk_dtnp(Obj, doubleData, (uint)data.Length, timestamps, pushthrough ? 1 : 0));
                    break;
                case int[,] intData:
                    Error.Check(DllHandler.lsl_push_chunk_itnp(Obj, intData, (uint)data.Length, timestamps, pushthrough ? 1 : 0));
                    break;
                case short[,] shortData:
                    Error.Check(DllHandler.lsl_push_chunk_stnp(Obj, shortData, (uint)data.Length, timestamps, pushthrough ? 1 : 0));
                    break;
                case char[,] charData:
                    Error.Check(DllHandler.lsl_push_chunk_ctnp(Obj, charData, (uint)data.Length, timestamps, pushthrough ? 1 : 0));
                    break;
                default:
                    break;
            }
        }

        public void PushChunk(string[,] data, double[] timestamps, bool pushthrough = true) =>
            Error.Check(DllHandler.lsl_push_chunk_strtnp(Obj, data, (uint)data.Length, timestamps, pushthrough ? 1 : 0));

        //public async Task WaitForConsumersAsync(double timeout = Constants.Forever)
        //{
        //    int consumers = 0;
        //    _ = await Task.Run(() => consumers = DllHandler.lsl_wait_for_consumers(Obj, timeout));
        //    // do something with consumers
        //}
    }
}
