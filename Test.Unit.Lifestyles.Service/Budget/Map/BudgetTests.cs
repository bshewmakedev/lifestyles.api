using Xunit;
using Lifestyles.Domain.Measure.Constants;
using BudgetEntity = Lifestyles.Service.Budget.Map.Budget;

namespace Test.Unit.Lifestyles.Service.Budget.Map
{
    public class BudgetTests
    {
        [Theory]
        [InlineData(Direction.In, Recurrence.Never)]
        [InlineData(Direction.In, Recurrence.Daily)]
        [InlineData(Direction.Out, Recurrence.Never)]
        [InlineData(Direction.Out, Recurrence.Weekly)]
        public void Budget_ShouldConstruct(Direction direction, Recurrence recurrence)
        {
            var budget = new BudgetEntity(direction, recurrence);

            Assert.NotNull(budget.Categories);
            Assert.True(budget.Amount > 0);
            Assert.True(budget.Direction.Equals(direction));
            Assert.True(budget.Existence.Equals(Existence.Excluded));
            Assert.False(string.IsNullOrWhiteSpace(budget.Id.ToString()));
            Assert.True(string.IsNullOrWhiteSpace(budget.Label));

            if (recurrence.Equals(Recurrence.Never))
            {
                Assert.Null(budget.Lifetime);
            }
            else
            {
                Assert.NotNull(budget.Lifetime);
            }

            Assert.True(budget.Recurrence.Equals(recurrence));
        }
    }
}