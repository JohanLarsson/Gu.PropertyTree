namespace Gu.PropertyTree.Demo
{
    using System.Windows;
    using System.Windows.Controls;

    public class ValueTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NullTemplate { get; set; }
      
        public DataTemplate StringTemplate { get; set; }
        
        public DataTemplate ReadonlyStringTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var node = item as IPropertyNode;
            if (node == null)
            {
                return NullTemplate;
            }

            if (node.ParentProperty.CanWrite)
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
