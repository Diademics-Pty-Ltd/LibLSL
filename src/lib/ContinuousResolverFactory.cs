using LSL.Internal;

namespace LSL
{
    public static class ContinuousResolverFactory
    {
        public static IContinuousResolver Create(double forgetAfter = 5.0) => new ContinuousResolver(forgetAfter);
        public static IContinuousResolver Create(string property, string value, double forgetAfter = 5.0) => new ContinuousResolver(property, value, forgetAfter);
        public static IContinuousResolver Create(string predicate, double forgetAfter = 5.0) => new ContinuousResolver(predicate, forgetAfter);
    }
}
