namespace Gu.PropertyTree
{
    using System.Reflection;

    public interface IPropertyNode : INode
    {
        object Parent { get; }

        PropertyInfo ParentProperty { get; }
    }
}