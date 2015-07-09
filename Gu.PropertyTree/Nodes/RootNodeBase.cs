namespace Gu.PropertyTree
{
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    public class RootNodeBase : INode
    {
        private readonly ObservableCollection<INode> _innerNodes = new ObservableCollection<INode>();
        private readonly ReadOnlyObservableCollection<INode> _nodes;

        public RootNodeBase(object value)
        {
            Value = value;
            var nodes = Node.CreatePropertyNodes(value).ToArray();
            _innerNodes = new ObservableCollection<INode>(nodes);
            _nodes = new ReadOnlyObservableCollection<INode>(_innerNodes);
            HasEditableSubNodes = Nodes.OfType<EditableNode>()
                                       .Any();
  

        }

        public RootNodeBase(IEnumerable enumerable)
        {
            Value = enumerable;
            _nodes = new ReadOnlyObservableCollection<INode>(_innerNodes);
            foreach (var item in enumerable)
            {
                _innerNodes.Add(new RootNode(item));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object Value { get; private set; }

        public ReadOnlyObservableCollection<INode> Nodes { get { return _nodes; } }



        public bool HasEditableSubNodes { get; private set; }

        public void Dispose()
        {
            foreach (var node in Nodes)
            {
                node.Dispose();
            }
        }
    }
}