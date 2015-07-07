namespace Gu.PropertyTree
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    public class RootNode : INode
    {
        public RootNode(object value)
        {
            Value = value;
            var nodes = Node.CreatePropertyNodes(value).ToArray();
            Nodes = new ReadOnlyObservableCollection<IPropertyNode>(new ObservableCollection<IPropertyNode>(nodes));
            HasEditableSubNodes = Nodes.OfType<EditableNode>()
                                       .Any();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public object Value { get; private set; }

        public ReadOnlyObservableCollection<IPropertyNode> Nodes { get; private set; }

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
