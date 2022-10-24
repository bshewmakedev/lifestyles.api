using Xunit;
using VmRecurrenceMap = Lifestyles.Api.Live.Models.VmRecurrence;

namespace Lifestyles.Test.Unit.Api.Live.Models
{
    public class VmRecurrence
    {
        [Fact]
        public void Should_GetVmRecurrence_GivenRecurrence()
        {
            var recurrence = Lifestyles.Domain.Live.Entities.Recurrence.Annually;
            var vmRecurrence = VmRecurrenceMap.Map(recurrence);

            Assert.Equal(Lifestyles.Domain.Live.Entities.Recurrence.Annually, recurrence);
        }
    }
}