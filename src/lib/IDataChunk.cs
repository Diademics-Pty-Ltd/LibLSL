using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSL
{
    public interface IDataChunk<T> : IChunk where T : struct
    {
        public IReadOnlyList<IReadOnlyList<T>> GetInletData();
        public IReadOnlyList<double> GetTimeStamps();
    }
}
