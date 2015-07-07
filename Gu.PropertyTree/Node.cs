namespace Gu.PropertyTree
{
    using System;
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
                if (property.CanWrite && property.CanRead)
                {
                    yield return new EditableNode(value, property);
                }

                else if(property.CanRead)
                {
                    yield return new ReadOnlyNode(value, property);
                }

                else
                {
                    throw new NotSupportedException("Does not support write only properties");
                }
            }
        }
    }
}