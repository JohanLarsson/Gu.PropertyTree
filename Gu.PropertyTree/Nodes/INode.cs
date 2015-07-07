namespace Gu.PropertyTree
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public interface INode : IDisposable, INotifyPropertyChanged
    {       
        object Value { get; }
        
        ReadOnlyObservableCollection<IPropertyNode> Nodes { get; }

        bool HasEditableSubNodes { get; }
    }
}