using Xunit;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Service.Live.Map;

namespace Test.Unit.Lifestyles.Service.Live.Map
{
    public class NodeTests
    {
        public class Identified : IIdentified
        {
            public Guid Id { get; private set; }

            public Identified(
            Guid? id = null)
            {
                Identify(id);
            }

            public void Identify(Guid? id = null)
            {
                Id = id.HasValue ? id.Value : Guid.NewGuid();
            }
        }

        [Fact]
        public void Node_ShouldConstruct()
        {
            var node = new Node<IIdentified>(new Identified());

            Assert.NotNull(node.Value);
            Assert.Null(node.Parent);
            Assert.NotNull(node.Children);
            Assert.Empty(node.Children);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void AddValueAsChild_FromEmpty_ToN(int childCount)
        {
            var node = new Node<IIdentified>(new Identified());

            Assert.NotNull(node.Children);
            Assert.Empty(node.Children);

            for (var i = 0; i < childCount; i++)
            {
                node.AddValueAsChild(new Identified());
                Assert.True(node.Children.Count == i + 1);
            }
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        public void AddValuesAsChildren_FromEmpty_ToN(int childCount)
        {
            var node = new Node<IIdentified>(new Identified());

            Assert.NotNull(node.Children);
            Assert.Empty(node.Children);

            node.AddValuesAsChildren(Enumerable.Range(0, childCount)
                .Select(i => new Identified())
                .ToArray());

            Assert.True(node.Children.Count == childCount);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void RemoveChild_FromN_ToEmpty(int childCount)
        {
            var node = new Node<IIdentified>(new Identified());

            node.AddValuesAsChildren(Enumerable.Range(0, childCount)
                .Select(i => new Identified())
                .ToArray());
            
            Assert.NotNull(node.Children);
            Assert.NotEmpty(node.Children);
            
            node.RemoveChild(node.Children[0]);
            Assert.True(node.Children.Count() == childCount - 1);
        }
    }
}