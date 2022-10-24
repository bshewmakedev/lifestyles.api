using Xunit;
using VmExistenceMap = Lifestyles.Api.Live.Models.VmExistence;

namespace Lifestyles.Test.Unit.Api.Live.Models
{
    public class VmExistence
    {
        [Fact]
        public void Should_GetVmExistence_GivenExistence()
        {
            var existence = Lifestyles.Domain.Live.Entities.Existence.Suggested;
            var vmExistence = VmExistenceMap.Map(existence);

            Assert.Equal(Lifestyles.Domain.Live.Entities.Existence.Suggested, existence);
        }
    }
}