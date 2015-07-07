namespace Gu.PropertyTree
{
    using System.Collections.Generic;
    using System.Reflection;

    public class EditableNode : INode
    {
        internal EditableNode(object instance, PropertyInfo property, IReadOnlyList<INode> nodes)
        {
            Parent = instance;
            Parent = property;
            Nodes = nodes;
        }

        public object Parent { get; private set; }

        public PropertyInfo ParentProperty { get; private set; }

        public object Value
        {
            get
            {
                return ParentProperty.GetValue(Parent);
            }
            set
            {
                ParentProperty.SetValue(Parent, value);
            }
        }

        public IReadOnlyList<INode> Nodes { get; private set; }
    }
}
