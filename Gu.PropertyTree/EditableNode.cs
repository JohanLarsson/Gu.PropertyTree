namespace Gu.PropertyTree
{
    using System.Collections.Generic;
    using System.Reflection;

    public class EditableNode : NodeBase, INode
    {
        public EditableNode(object parent, PropertyInfo parentProperty)
            : base(parent, parentProperty)
        {
        }

        public new object Value
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
    }
}
