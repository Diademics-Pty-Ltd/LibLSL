using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSL
{
    public interface IMarkerChunk<T> : IChunk
    {
        public IReadOnlyList<IReadOnlyList<T>> GetMarkers();
    }
}
