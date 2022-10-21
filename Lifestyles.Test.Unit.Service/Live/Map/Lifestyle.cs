using Lifestyles.Domain.Live.Entities;
using Xunit;
using LifestyleMap = Lifestyles.Service.Live.Map.Lifestyle;

namespace Lifestyles.Test.Unit.Service.Live.Map
{
    public class Lifestyle
    {
        [Fact]
        public void Should_GetLifestyle_GivenNoParams()
        {
            var lifestyle = new LifestyleMap();

            Assert.NotNull(lifestyle.Id);
            Assert.NotEqual(lifestyle.Id, Guid.Empty);

            Assert.NotNull(lifestyle.Label);
            Assert.Equal(lifestyle.Label, "");

            Assert.Null(lifestyle.Lifetime);

            Assert.Equal(lifestyle.Recurrence, Recurrence.Never);

            Assert.Equal(lifestyle.Existence, Existence.Expected);
        }

        [Fact]
        public void Should_GetLifestyle_GivenParams()
        {
            var id = Guid.NewGuid();
            var label = "label";
            var lifetime = 6;
            var recurrence = Recurrence.Monthly;
            var existence = Existence.Suggested;
            var lifestyle = new LifestyleMap(id, label, lifetime, recurrence, existence);

            Assert.NotNull(lifestyle.Id);
            Assert.Equal(lifestyle.Id, id);

            Assert.NotNull(lifestyle.Label);
            Assert.Equal(lifestyle.Label, label);

            Assert.NotNull(lifestyle.Lifetime);
            Assert.Equal(lifestyle.Lifetime, lifetime);

            Assert.Equal(lifestyle.Recurrence, recurrence);

            Assert.Equal(lifestyle.Existence, existence);
        }
    }
}