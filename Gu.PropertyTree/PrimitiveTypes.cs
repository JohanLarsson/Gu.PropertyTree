namespace Gu.PropertyTree
{
    using System.Collections;
    using System.Collections.Generic;

    public class PrimitiveTypes : IReadOnlyCollection<PrimitiveType>
    {
        private readonly List<PrimitiveType> _primitiveTypes = new List<PrimitiveType>();

        public int Count
        {
            get { return _primitiveTypes.Count; }
        }

        public IEnumerator<PrimitiveType> GetEnumerator()
        {
            return _primitiveTypes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
