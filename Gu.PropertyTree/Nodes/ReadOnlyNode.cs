namespace Gu.PropertyTree
{
    using System.Reflection;

    public class ReadOnlyNode : PropertyNode
    {
        public ReadOnlyNode(object parent, PropertyInfo parentProperty)
            : base(parent, parentProperty)
        {
        }
    }
}