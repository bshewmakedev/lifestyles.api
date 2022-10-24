using Lifestyles.Domain.Live.Entities;
using Lifestyles.Api.Live.Models;
using Xunit;
using LifestyleMap = Lifestyles.Api.Live.Map.Lifestyle;
using DirectionMap = Lifestyles.Api.Live.Map.Direction;
using RecurrenceMap = Lifestyles.Api.Live.Map.Recurrence;
using ExistenceMap = Lifestyles.Api.Live.Map.Existence;

namespace Lifestyles.Test.Unit.Api.Live.Map
{
    public class Lifestyle
    {
        [Fact]
        public void Should_GetLifestyle_GivenVmLifestyle()
        {
            var vmLifestyle = new VmLifestyle
            {
                Id = Guid.NewGuid(),
                Label = "label",
                Lifetime = 6,
                Recurrence = "monthly",
                Existence = "suggested"
            };
            var lifestyle = new LifestyleMap(vmLifestyle);

            Assert.Equal(lifestyle.Id, vmLifestyle.Id);

            Assert.NotNull(lifestyle.Label);
            Assert.Equal(lifestyle.Label, vmLifestyle.Label);

            Assert.NotNull(lifestyle.Lifetime);
            Assert.Equal(lifestyle.Lifetime, vmLifestyle.Lifetime);

            Assert.Equal(lifestyle.Recurrence, RecurrenceMap.Map(vmLifestyle.Recurrence));

            Assert.Equal(lifestyle.Existence, ExistenceMap.Map(vmLifestyle.Existence));
        }
    }
}