using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSL
{
    public interface IStreamOutlet : IDisposable
    {
        public bool HaveConsumers { get; }
        public IStreamInfo Info { get; }

        public bool WaitForConsumers(double timeout);

        public int PushSample(float[] data, double timestamp = 0.0, bool pushthrough = true);
        public int PushSample(double[] data, double timestamp = 0.0, bool pushthrough = true);
        public int PushSample(int[] data, double timestamp = 0.0, bool pushthrough = true);
        public int PushSample(short[] data, double timestamp = 0.0, bool pushthrough = true);
        public int PushSample(char[] data, double timestamp = 0.0, bool pushthrough = true);
        public int PushSample(string[] data, double timestamp = 0.0, bool pushthrough = true);

        public int PushChunk(float[,] data, double timestamp = 0.0, bool pushthrough = true);
        public int PushChunk(double[,] data, double timestamp = 0.0, bool pushthrough = true);
        public int PushChunk(int[,] data, double timestamp = 0.0, bool pushthrough = true);
        public int PushChunk(short[,] data, double timestamp = 0.0, bool pushthrough = true);
        public int PushChunk(char[,] data, double timestamp = 0.0, bool pushthrough = true);
        public int PushChunk(string[,] data, double timestamp = 0.0, bool pushthrough = true);

        public int PushChunk(float[,] data, double[] timestamps, bool pushthrough = true);
        public int PushChunk(double[,] data, double[] timestamps, bool pushthrough = true);
        public int PushChunk(int[,] data, double[] timestamps, bool pushthrough = true);
        public int PushChunk(short[,] data, double[] timestamps, bool pushthrough = true);
        public int PushChunk(char[,] data, double[] timestamps, bool pushthrough = true);
        public int PushChunk(string[,] data, double[] timestamps, bool pushthrough = true);
    }
}
