namespace LSL
{
    public static class LSLUtils
    {
        public static int ProtocolVersion => DllHandler.lsl_protocol_version();
        public static int LibraryVersion => DllHandler.lsl_library_version();
        public static double LocalClock => DllHandler.lsl_local_clock();
    }
}
