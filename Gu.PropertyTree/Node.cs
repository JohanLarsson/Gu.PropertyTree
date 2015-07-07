namespace Gu.PropertyTree
{
    using System.Collections.Generic;

    public static class Node
    {
        public static RootNode Create(object instance)
        {
            var node = new RootNode(instance);
            return node;
        }

        internal static IEnumerable<INode> CreateSubNodes(object value)
        {
            if (value == null)
            {
                yield break;
            }
            var properties = value.GetType()
                                                .GetProperties();
            foreach (var property in properties)
            {
                yield return new ReadOnlyNode(value, property);
            }
        }
    }
}