namespace Gu.PropertyTree
{
    using System.Diagnostics;
    using System.Reflection;

    [DebuggerDisplay("ReadonlyNode: Property: {ParentProperty.Name}, Value: {Value}")]
    public class ReadOnlyNode : NodeBase
    {
        public ReadOnlyNode(object parent, PropertyInfo parentProperty)
            : base(parent, parentProperty)
        {
        }
    }
}