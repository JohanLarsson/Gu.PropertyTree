namespace Gu.PropertyTree.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    using NUnit.Framework;

    public class NodeTests
    {
        [Test]
        public void CreateSimple()
        {
            var instance = new Dummy();
            var node = Node.Create(instance);
            Assert.AreSame(instance, node.Value);
            Assert.AreEqual(4, node.Nodes.Count);
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
            Assert.AreEqual(4, node.Nodes.Count);
         
            var nextNode = node.Nodes.OfType<IPropertyNode>().Single(x => x.ParentProperty.Name == "Next");
            Assert.AreSame(instance.Next, nextNode.Value);
            Assert.AreEqual(4, nextNode.Nodes.Count);
        }

        [Test]
        public void SubscribesAndUpdates()
        {
            var instance = new Dummy();
            var node = Node.Create(instance);
            Assert.AreSame(instance, node.Value);
            Assert.AreEqual(4, node.Nodes.Count);

            instance.Next = new Dummy();
            var nextNode = node.Nodes.OfType<IPropertyNode>().Single(x => x.ParentProperty.Name == "Next");
            Assert.AreSame(instance.Next, nextNode.Value);
            Assert.AreEqual(4, nextNode.Nodes.Count);
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

        [Test]
        public void CollectionChangeTest()
        {
            var l = new ObservableCollection<Dummy>();
            var node = Node.Create(l);
            l.Add(new Dummy { Value = 1, Name = "Max" });
            var itemNode = node.Nodes.OfType<ItemsNode>()
                                         .Single();

            var enumerable = itemNode.Value as IEnumerable;
            Assert.AreEqual(1, enumerable.OfType<Dummy>().Count());
        }

        [Test]
        public void CollectionWithinClassChangedTest()
        {
            var l = new List<Dummy>() { new Dummy { Value = 1, Name = "Max" } };
            var dummie = new Dummy { Value = 2, Name = "Kalle" };
            var node = Node.Create(dummie);
            var element = node.Nodes.OfType<IPropertyNode>().Single(x => x.ParentProperty.Name == "Dummies");
            Assert.AreEqual(null, element.Value);
            dummie.Dummies = l;
            Assert.AreNotEqual(null, element.Value);
        }

        [Test]
        public void TimeStampTest()
        {

            var dummy = new TimeSpanDummy() { DateTime = new DateTime() };
            var node = Node.Create(dummy);

        }

        [Test]
        public void ArrayTest()
        {
            var arr = new[] { 1, 2 };
            var node = Node.Create(arr);

        }
    }

    public class TimeSpanDummy
    {
        public DateTime DateTime { get; set; }


    }
}
