namespace Core.Infrastructure.Misc
{
    public class MutableKeyValuePair<TKeyType, TValueType>
    {
        public TKeyType Key { get; }
        public TValueType Value { get; set; }

        public MutableKeyValuePair() { }

        public MutableKeyValuePair(TKeyType key, TValueType val)
        {
            Key = key;
            Value = val;
        }
    }
}