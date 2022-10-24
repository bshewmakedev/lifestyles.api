using Lifestyles.Domain.Live.Entities;
using Xunit;
using VmLifestyleMap = Lifestyles.Api.Live.Models.VmLifestyle;
using LifestyleMap = Lifestyles.Service.Live.Map.Lifestyle;
using RecurrenceMap = Lifestyles.Api.Live.Map.Recurrence;
using ExistenceMap = Lifestyles.Api.Live.Map.Existence;

namespace Lifestyles.Test.Unit.Api.Live.Models
{
    public class VmLifestyle
    {
        [Fact]
        public void Should_GetVmLifestyle_GivenLifestyle()
        {
            var lifestyle = new LifestyleMap(
                Guid.NewGuid(),
                "label",
                6,
                Recurrence.Monthly,
                Existence.Suggested
            );
            var vmLifestyle = new VmLifestyleMap(lifestyle);

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