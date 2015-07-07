namespace Gu.PropertyTree
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using Gu.PropertyTree.Annotations;

    [DebuggerDisplay("{GetType().Name} Property: {ParentProperty.Name}, Value: {Value}")]
    public abstract class PropertyNode : IPropertyNode
    {
        protected static readonly PropertyChangedEventArgs ValueChangedEventArgs = new PropertyChangedEventArgs("Value");
        private readonly ObservableCollection<IPropertyNode> _innerNodes;
        private readonly ReadOnlyObservableCollection<IPropertyNode> _nodes;

        private bool _disposed;

        protected PropertyNode(object parent, PropertyInfo parentProperty)
        {
            Parent = parent;
            var inpc = parent as INotifyPropertyChanged;
            if (inpc != null)
            {
                inpc.PropertyChanged += OnParentPropertyChanged;
            }
            ParentProperty = parentProperty;
            var propertyNodes = Node.CreatePropertyNodes(Value).ToArray();
            _innerNodes = new ObservableCollection<IPropertyNode>(propertyNodes);
            _nodes = new ReadOnlyObservableCollection<IPropertyNode>(_innerNodes);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object Parent { get; private set; }

        public PropertyInfo ParentProperty { get; private set; }

        public object Value
        {
            get
            {
                return ParentProperty.GetValue(Parent);
            }
        }

        public ReadOnlyObservableCollection<IPropertyNode> Nodes
        {
            get
            {
                return _nodes;
            }
        }

        /// <summary>
        /// Make the class sealed when using this. 
        /// Call VerifyDisposed at the start of all public methods
        /// </summary>
        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }
            _disposed = true;
            var inpc = Parent as INotifyPropertyChanged;
            if (inpc != null)
            {
                inpc.PropertyChanged -= OnParentPropertyChanged;
            }
            foreach (var node in Nodes)
            {
                node.Dispose();
            }
            _innerNodes.Clear();
        }

        private void VerifyDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnParentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != ParentProperty.Name)
            {
                return;
            }
            OnPropertyChanged(ValueChangedEventArgs);
            foreach (var node in Nodes)
            {
                node.Dispose();
            }
            _innerNodes.Clear();

            var nodes = Node.CreatePropertyNodes(Value).ToArray();
            foreach (var node in nodes)
            {
                _innerNodes.Add(node);
            }
        }
    }
}