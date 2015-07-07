namespace Gu.PropertyTree
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    [DebuggerDisplay("Property: {ParentProperty.Name}, Value: {Value}")]
    public class ReadOnlyNode : INode
    {
        internal ReadOnlyNode(object parent, PropertyInfo parentProperty)
        {
            Parent = parent;
            ParentProperty = parentProperty;
            Nodes = Node.CreateSubNodes(Value).ToArray();
        }

        public object Parent { get; private set; }

        public PropertyInfo ParentProperty { get; private set; }

        public object Value
        {
            get
            {
                return ParentProperty.GetValue(Parent);
            }
        }

        public IReadOnlyList<INode> Nodes { get; private set; }
    }
}