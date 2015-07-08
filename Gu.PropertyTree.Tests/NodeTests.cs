namespace Gu.PropertyTree.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    public class NodeTests
    {
        [Test]
        public void CreateSimple()
        {
            var instance = new Dummy();
            var node = Node.Create(instance);
            Assert.AreSame(instance, node.Value);
            Assert.AreEqual(3, node.Nodes.Count);
        }

        [Test]
        public void CreateWithValues()
        {
            var instance = new Dummy { Value = 2, Name = "Max" };
            var node = Node.Create(instance);
            var valueNode = node.Nodes.OfType<IPropertyNode>().Single(x => x.ParentProperty.Name == "Value");
            Assert.AreEqual(2, valueNode.Value);

            var nameNode = node.Nodes.OfType<IPropertyNode>().Single(x => x.ParentProperty.Name == "Name");
            Assert.AreEqual("Max", nameNode.Value);

            var nextNode = node.Nodes.OfType<IPropertyNode>().Single(x => x.ParentProperty.Name == "Next");
            Assert.AreEqual(null, nextNode.Value);
        }

        [Test]
        public void CreateNested()
        {
            var instance = new Dummy { Next = new Dummy() };
            var node = Node.Create(instance);
            Assert.AreSame(instance, node.Value);
            Assert.AreEqual(3, node.Nodes.Count);

            var nextNode = node.Nodes.OfType<IPropertyNode>().Single(x => x.ParentProperty.Name == "Next");
            Assert.AreSame(instance.Next, nextNode.Value);
            Assert.AreEqual(3, nextNode.Nodes.Count);
        }

        [Test]
        public void SubscribesAndUpdates()
        {
            var instance = new Dummy();
            var node = Node.Create(instance);
            Assert.AreSame(instance, node.Value);
            Assert.AreEqual(3, node.Nodes.Count);

            instance.Next = new Dummy();
            var nextNode = node.Nodes.OfType<IPropertyNode>().Single(x => x.ParentProperty.Name == "Next");
            Assert.AreSame(instance.Next, nextNode.Value);
            Assert.AreEqual(3, nextNode.Nodes.Count);
        }

        [Test]
        public void DummyListTest()
        {
            var instance = new Dummy { Value = 2, Name = "Max" };
            var l = new List<Dummy>() { instance };
            var node = Node.Create(l);
            Assert.AreEqual(1, node.Nodes.OfType<ItemsNode>().Count());
            var itemNode = node.Nodes.OfType<ItemsNode>()
                                    .Single();
            Assert.AreSame(instance, itemNode.Nodes.Single().Value);
        }
    }
}
