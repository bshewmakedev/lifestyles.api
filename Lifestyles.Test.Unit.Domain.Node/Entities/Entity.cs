using Xunit;
using EntityObj = Lifestyles.Domain.Node.Entities.Entity;

namespace Lifestyles.Test.Unit.Domain.Node.Entities
{
    public class Entity
    {
        [Fact]
        public void Should_ConstructDefault_GivenNoParams()
        {
            var entity = new EntityObj().Identify().As().Relabel();

            Assert.NotEqual(Guid.Empty, entity.Id);
            Assert.Equal(string.Empty, entity.Label);
        }


        [Fact]
        public void Should_ConstructSpecific_GivenParams()
        {
            var id = Guid.NewGuid();
            var label = "new label";
            var entity = new EntityObj(id: id, label: label);

            Assert.Equal(id, entity.Id);
            Assert.Equal(label, entity.Label);
        }


        [Fact]
        public void Should_Identify_GivenNoId()
        {
            var entity = new EntityObj().Identify();

            var id = entity.Id;
            entity.Identify();

            Assert.NotEqual(Guid.Empty, entity.Id);
            Assert.NotEqual(id, entity.Id);
        }


        [Fact]
        public void Should_Identify_GivenId()
        {
            var entity = new EntityObj().Identify();

            var id = Guid.NewGuid();
            entity.Identify(id);

            Assert.Equal(id, entity.Id);
        }


        [Fact]
        public void Should_Relabel_GivenNoLabel()
        {
            var entity = new EntityObj().Relabel();

            entity.Relabel();

            Assert.Equal(string.Empty, entity.Label);
        }


        [Fact]
        public void Should_Relabel_GivenLabel()
        {
            var entity = new EntityObj().Relabel();

            var label = "new label";
            entity.Relabel(label);

            Assert.Equal(label, entity.Label);
        }
    }
}