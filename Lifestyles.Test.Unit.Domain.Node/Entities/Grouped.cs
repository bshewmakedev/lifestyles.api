using Xunit;
using Lifestyles.Domain.Node.Entities;
using EntityObj = Lifestyles.Domain.Node.Entities.Entity;

namespace Lifestyles.Test.Unit.Domain.Node.Entities
{
    public class Grouped
    {
        [Fact]
        public void Should_ConstructDefault_GivenNoParams()
        {
            var grouped = new Grouped<EntityObj>();

            Assert.NotNull(grouped.Entities);
            Assert.Empty(grouped.Entities);
        }
    }
}