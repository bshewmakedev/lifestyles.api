using Xunit;
using ExistenceMap = Lifestyles.Api.Live.Map.Existence;

namespace Lifestyles.Test.Unit.Api.Live.Map
{
    public class Existence
    {
        [Fact]
        public void Should_GetExistence_GivenVmExistence()
        {
            var vmExistence = "suggested";
            var existence = ExistenceMap.Map(vmExistence);

            Assert.Equal(Lifestyles.Domain.Live.Entities.Existence.Suggested, existence);
        }
    }
}