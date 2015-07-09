namespace Gu.PropertyTree
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public class EditableNode : PropertyNode, INode
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
                var typedValue = Convert.ChangeType(value, ParentProperty.PropertyType);
                ParentProperty.SetValue(Parent, typedValue);
            }
        }
    }
}
