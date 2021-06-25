using LSL.Internal;

namespace LSL
{
    public static class StreamOutletFactory
    {
        public static IStreamOutlet Create(IStreamInfo info, int chunkSize) => new StreamOutlet(info, chunkSize);
    }
}
