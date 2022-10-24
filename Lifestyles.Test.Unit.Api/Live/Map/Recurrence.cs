using Xunit;
using RecurrenceMap = Lifestyles.Api.Live.Map.Recurrence;

namespace Lifestyles.Test.Unit.Api.Live.Map
{
    public class Recurrence
    {
        [Fact]
        public void Should_GetRecurrence_GivenVmRecurrence()
        {
            var vmRecurrence = "annually";
            var recurrence = RecurrenceMap.Map(vmRecurrence);

            Assert.Equal(Lifestyles.Domain.Live.Entities.Recurrence.Annually, recurrence);
        }
    }
}