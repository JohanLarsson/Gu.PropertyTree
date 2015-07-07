namespace Gu.PropertyTree
{
    using System.Collections.Generic;
    using System.Linq;

    public class RootNode
    {
        public RootNode(object value)
        {
            Value = value;
            Nodes = Node.CreateSubNodes(value).ToArray();
        }

        public object Value { get; private set; }

        public IReadOnlyList<INode> Nodes { get; private set; }
    }
}
