using Xunit;
using DirectionMap = Lifestyles.Api.Live.Map.Direction;

namespace Lifestyles.Test.Unit.Api.Live.Map
{
    public class Direction
    {
        [Fact]
        public void Should_GetDirection_GivenVmAmount()
        {
            var vmAmount = -15;
            var direction = DirectionMap.Map(vmAmount);

            Assert.Equal(Lifestyles.Domain.Live.Entities.Direction.Out, direction);
        }
    }
}