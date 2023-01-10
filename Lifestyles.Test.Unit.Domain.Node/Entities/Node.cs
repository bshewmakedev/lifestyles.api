using Xunit;
using Lifestyles.Domain.Node.Entities;
using EntityObj = Lifestyles.Domain.Node.Entities.Entity;

namespace Lifestyles.Test.Unit.Domain.Node.Entities
{
    public class Node
    {
        private EntityObj GetEntity()
        {
            var entity = new EntityObj();

            entity.Identify();
            entity.Relabel();

            return entity;
        }

        [Fact]
        public INode<EntityObj> Should_AddNoChildren_GivenNoChildren_GivenNoEntities()
        {
            var node = new Node<EntityObj>(GetEntity());

            Assert.Empty(node.Children);

            node.AddChildren(new EntityObj[] { });

            Assert.Equal(node.Children.Count, 0);

            return node;
        }

        [Fact]
        public INode<EntityObj> Should_AddChildren_GivenNoChildren_GivenSingleEntity()
        {
            var node = new Node<EntityObj>(GetEntity());

            Assert.Empty(node.Children);

            node.AddChildren(new EntityObj[] { GetEntity() });

            Assert.Equal(node.Children.Count, 1);

            return node;
        }

        [Fact]
        public INode<EntityObj> Should_AddChildren_GivenNoChildren_GivenMultiEntities()
        {    
            var node = new Node<EntityObj>(GetEntity());

            Assert.Empty(node.Children);

            node.AddChildren(new EntityObj[] {
                GetEntity(),
                GetEntity(),
                GetEntity(),
                GetEntity(),
            });

            Assert.Equal(node.Children.Count, 4);

            return node;
        }

        [Fact]
        public INode<EntityObj> Should_AddNoChildren_GivenChildren_GivenNoEntities()
        {
            var node = Should_AddChildren_GivenNoChildren_GivenMultiEntities();

            node.AddChildren(new EntityObj[] { });

            Assert.Equal(node.Children.Count, 4);

            return node;
        }

        [Fact]
        public INode<EntityObj> Should_AddChildren_GivenChildren_GivenSingleEntity()
        {
            var node = Should_AddChildren_GivenNoChildren_GivenMultiEntities();

            node.AddChildren(new EntityObj[] { GetEntity() });

            Assert.Equal(node.Children.Count, 5);

            return node;
        }

        [Fact]
        public INode<EntityObj> Should_AddChildren_GivenChildren_GivenMultiEntities()
        {    
            var node = Should_AddChildren_GivenNoChildren_GivenMultiEntities();

            node.AddChildren(new EntityObj[] {
                GetEntity(),
                GetEntity(),
                GetEntity(),
                GetEntity(),
            });

            Assert.Equal(node.Children.Count, 8);

            return node;
        }


        [Fact]
        public INode<EntityObj> Should_RemoveNoChildren_GivenNoChildren_GivenNoEntities()
        {
            var node = new Node<EntityObj>(GetEntity());

            Assert.Empty(node.Children);

            node.RemoveChildren(new EntityObj[] { });

            Assert.Equal(node.Children.Count, 0);

            return node;
        }

        [Fact]
        public INode<EntityObj> Should_RemoveNoChildren_GivenNoChildren_GivenSingleEntity()
        {
            var node = new Node<EntityObj>(GetEntity());

            Assert.Empty(node.Children);

            node.RemoveChildren(new EntityObj[] { GetEntity() });

            Assert.Equal(node.Children.Count, 0);

            return node;
        }

        [Fact]
        public INode<EntityObj> Should_RemoveNoChildren_GivenNoChildren_GivenMultiEntities()
        {    
            var node = new Node<EntityObj>(GetEntity());

            Assert.Empty(node.Children);

            node.RemoveChildren(new EntityObj[] {
                GetEntity(),
                GetEntity(),
                GetEntity(),
                GetEntity(),
            });

            Assert.Equal(node.Children.Count, 0);

            return node;
        }

        [Fact]
        public INode<EntityObj> Should_RemoveNoChildren_GivenChildren_GivenNoEntities()
        {
            var node = Should_AddChildren_GivenNoChildren_GivenMultiEntities();

            node.RemoveChildren(new EntityObj[] { });

            Assert.Equal(node.Children.Count, 4);

            return node;
        }

        [Fact]
        public INode<EntityObj> Should_RemoveChildren_GivenChildren_GivenSingleEntity()
        {
            var node = Should_AddChildren_GivenNoChildren_GivenMultiEntities();

            node.RemoveChildren(new EntityObj[] { node.Children[0].Entity });

            Assert.Equal(node.Children.Count, 3);

            return node;
        }

        [Fact]
        public INode<EntityObj> Should_RemoveChildren_GivenChildren_GivenMultiEntities()
        {    
            var node = Should_AddChildren_GivenNoChildren_GivenMultiEntities();

            node.RemoveChildren(new EntityObj[] {
                node.Children[0].Entity,
                node.Children[1].Entity
            });

            Assert.Equal(node.Children.Count, 2);

            return node;
        }

        [Fact]
        public INode<Guid> Should_SelectDownRecursive_AsGuid_GivenMultiEntities()
        {
            var nodeEntity = Should_AddChildren_GivenNoChildren_GivenMultiEntities();

            var nodeGuid = nodeEntity.SelectDownRecursive(n => n.Id);

            Assert.NotEqual(Guid.Empty, nodeGuid.Entity);
            Assert.NotEmpty(nodeGuid.Children);
            Assert.Equal(nodeEntity.Children.Count, nodeGuid.Children.Count);
            nodeGuid.Children.ToList().ForEach(c => Assert.NotEqual(Guid.Empty, c.Entity));

            return nodeGuid;
        }

        [Fact]
        public void Should_FlattenDownRecursive_GivenMultiEntities()
        {
            var nodeEntity = Should_AddChildren_GivenNoChildren_GivenMultiEntities();

            var entityList = nodeEntity.FlattenDownRecursive();
            Assert.Equal(entityList.Count, 5);
        }
    }
}