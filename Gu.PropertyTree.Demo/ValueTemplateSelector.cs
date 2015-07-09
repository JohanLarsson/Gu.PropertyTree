namespace Gu.PropertyTree.Demo
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    public class ValueTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NullTemplate { get; set; }

        public DataTemplate StringTemplate { get; set; }

        public DataTemplate ReadonlyStringTemplate { get; set; }

        public DataTemplate RootNodeTemplate { get; set; }

        public DataTemplate DefaultTemplate { get; set; }

        public DataTemplate ListTemplate { get; set; }
        public DataTemplate ItemNodeTemplate { get; set; }

        public DataTemplate IntegerTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {

            if (item is ItemsNode)
            {
                return ItemNodeTemplate;
            }
            var node = item as IPropertyNode;
            if (node == null)
            {
                return NullTemplate;
            }
            if (node.Value is int && node.ParentProperty.CanWrite && node.ParentProperty.GetSetMethod(true).IsPublic)
            {
                return IntegerTemplate;
            }

            if ((node.Value is string) == false && node.Value is IEnumerable)
            {
                return ListTemplate;
            }
            if (node.ParentProperty.CanWrite && node.Value is string)
            {
                return StringTemplate;
            }
            else
            {
                return ReadonlyStringTemplate;
            }
            return base.SelectTemplate(item, container);
        }


    }
}
