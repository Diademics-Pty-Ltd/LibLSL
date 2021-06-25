using LSL.Internal;

namespace LSL
{
    public static class StreamInletFactory
    {
        public static IStreamInlet Create(IStreamInfo info, 
            int maxBufferLength = 360, 
            int maxChunkLength = 0, 
            bool recover = true, 
            PostProcessingOptions postProcessingOptions = PostProcessingOptions.None) => 
            new StreamInlet(info, maxBufferLength, maxChunkLength, recover,  postProcessingOptions);
    }
}
