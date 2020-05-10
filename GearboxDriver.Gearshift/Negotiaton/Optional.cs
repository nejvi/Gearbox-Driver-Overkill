namespace GearboxDriver.Gearshift.Negotiaton
{
    internal class Optional<T> where T : class
    {
        public T InnerValue { get; }
        public bool HasValue { get; }

        public Optional(T innerValue)
        {
            InnerValue = innerValue;
            HasValue = innerValue != null;
        }
        public static Optional<T> Empty()
        {
            return new Optional<T>(null);
        }
    }
}
