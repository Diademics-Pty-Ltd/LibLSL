using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSL;

namespace LSL
{
    public interface IChunk
    {
        ChannelFormatType ChannelFormatType { get; }
        int ChannelCount { get; }
    }
}
