namespace Gu.PropertyTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    public interface INode : IDisposable, INotifyPropertyChanged
    {
        object Parent { get; }

        PropertyInfo ParentProperty { get; }
        
        object Value { get; }
        
        IReadOnlyList<INode> Nodes { get; }
    }
}