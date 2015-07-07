namespace Gu.PropertyTree
{
    using System.Collections.Generic;
    using System.Reflection;

    public interface INode
    {
        object Parent { get; }

        PropertyInfo ParentProperty { get; }
        
        object Value { get; }
        
        IReadOnlyList<INode> Nodes { get; }
    }
}