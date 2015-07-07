namespace Gu.PropertyTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    using Gu.PropertyTree.Annotations;

    public abstract class NodeBase : INode
    {
        private bool _disposed;

        protected NodeBase(object parent, PropertyInfo parentProperty)
        {
            Parent = parent;
            var inpc = parent as INotifyPropertyChanged;
            if (inpc != null)
            {
                inpc.PropertyChanged += OnParentPropertyChanged;
            }
            ParentProperty = parentProperty;
            Nodes = Node.CreateSubNodes(Value).ToArray();
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

        public IReadOnlyList<INode> Nodes { get; private set; }

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
                inpc.PropertyChanged += OnParentPropertyChanged;
            }
            foreach (var node in Nodes)
            {
                node.Dispose();
            }
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

        private void OnParentPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != ParentProperty.Name)
            {
                return;
            }
            foreach (var node in Nodes)
            {
                node.Dispose();
            }
            Nodes = Node.CreateSubNodes(Value).ToArray();
            OnPropertyChanged("Value");
            OnPropertyChanged("Nodes");
        }
    }
}