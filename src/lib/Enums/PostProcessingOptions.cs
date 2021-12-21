namespace LSL
{
    public enum PostProcessingOptions
    {
        None = 0,
        ClockSync = 1,
        Dejitter = 2,
        Monotonize = 4,
        ThreadSafe = 8,
        All = 1 | 2 | 4 | 8
    }
}
