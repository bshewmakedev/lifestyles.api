using Xunit;
using Lifestyles.Domain.Measure.Constants;
using BudgetEntity = Lifestyles.Service.Budget.Map.Budget;

namespace Test.Unit.Lifestyles.Service.Budget.Map
{
    public class BudgetTests
    {
        [Theory]
        [InlineData(10, null, null, Recurrence.Never)]
        [InlineData(10, null, "", Recurrence.Daily)]
        [InlineData(-10, null, null, Recurrence.Never)]
        [InlineData(-10, null, "", Recurrence.Weekly)]
        public void Budget_ShouldConstruct(
            decimal amount,
            Guid id,
            string label,
            Recurrence recurrence)
        {
            var budget = new BudgetEntity(amount, id, label, recurrence);

            Assert.NotNull(budget.Categories);
            Assert.True(budget.Amount > 0);
            Assert.True(budget.Direction.Equals(amount > 0 ? Direction.In : Direction.Out));
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