namespace Gu.PropertyTree.Tests
{
    using NUnit.Framework;

    public class NodeTests
    {
        [Test]
        public void TestNameTest()
        {
            var instance = new Dummy();
            var node = Node.Create(instance);
            Assert.AreSame(instance, node.Value);
            Assert.AreEqual(2, node.Nodes.Count);
        }
    }
}
