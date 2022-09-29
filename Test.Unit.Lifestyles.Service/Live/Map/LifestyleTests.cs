using Xunit;
using Lifestyles.Domain.Measure.Constants;
using Lifestyles.Service.Live.Map;

namespace Test.Unit.Lifestyles.Service.Live.Map
{
    public class BudgetTests
    {
        [Theory]
        [InlineData(Direction.In, Recurrence.Never)]
        [InlineData(Direction.In, Recurrence.Daily)]
        [InlineData(Direction.Out, Recurrence.Never)]
        [InlineData(Direction.Out, Recurrence.Weekly)]
        public void Lifestyle_ShouldConstruct(Direction direction, Recurrence recurrence)
        {
            var lifestyle = new Lifestyle(direction, recurrence);

            Assert.NotNull(lifestyle.Budgets);
            Assert.NotNull(lifestyle.Categories);
            Assert.True(lifestyle.Amount > 0);
            Assert.True(lifestyle.Direction.Equals(direction));
            Assert.True(lifestyle.Existence.Equals(Existence.Excluded));
            Assert.False(string.IsNullOrWhiteSpace(lifestyle.Id.ToString()));
            Assert.True(string.IsNullOrWhiteSpace(lifestyle.Label));

            if (recurrence.Equals(Recurrence.Never))
            {
                Assert.Null(lifestyle.Lifetime);
            }
            else
            {
                Assert.NotNull(lifestyle.Lifetime);
            }

            Assert.True(lifestyle.Recurrence.Equals(recurrence));
        }
    }
}