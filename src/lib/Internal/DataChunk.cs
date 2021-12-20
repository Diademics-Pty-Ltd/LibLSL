using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSL.Internal
{
    internal class DataChunk<T> : IDataChunk<T> where T : struct
    {
        private readonly StreamInfo _streamInfo;
        private readonly int _chunkSize;
        private readonly List<List<T>> _data;
        private readonly List<double> _timestamps;

        public ChannelFormatType ChannelFormatType => _streamInfo.ChannelFormat;
        public int ChannelCount => _streamInfo.Channels;
        public DataChunk(StreamInfo streamInfo, int chunkSize)
        { 

        }
    }
}
