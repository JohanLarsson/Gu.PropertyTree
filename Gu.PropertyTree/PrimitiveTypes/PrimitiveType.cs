namespace Gu.PropertyTree
{
    using System;

    public class PrimitiveType
    {
        public PrimitiveType(Type type)
        {
            Type = type;
            ToString = o => o == null ? "null" : o.ToString();
        }

        public Type Type { get; private set; }

        public Func<object, string> ToString { get; private set; }

        public string AsString(object o)
        {
            return ToString(o);
        }
    }
}