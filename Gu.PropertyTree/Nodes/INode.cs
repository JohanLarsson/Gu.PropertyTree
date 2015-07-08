namespace Gu.PropertyTree
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public interface INode : IDisposable, INotifyPropertyChanged
    {       
        object Value { get; }

        ReadOnlyObservableCollection<INode> Nodes { get; }

        bool HasEditableSubNodes { get; }
    }
}