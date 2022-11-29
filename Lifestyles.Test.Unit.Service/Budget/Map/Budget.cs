using Lifestyles.Domain.Live.Entities;
using Xunit;
using BudgetMap = Lifestyles.Service.Budget.Map.Budget;

namespace Lifestyles.Test.Unit.Service.Budget.Map
{
    public class Budget
    {
        [Fact]
        public void Should_GetBudget_GivenNoParams()
        {
            var budget = new BudgetMap();

            Assert.Equal(budget.Value, 0);

            Assert.NotNull(budget.Id);
            Assert.NotEqual(budget.Id, Guid.Empty);

            Assert.NotNull(budget.Label);
            Assert.Equal(budget.Label, "");

            Assert.Null(budget.Lifetime);

            Assert.Equal(budget.Recurrence, Recurrence.Never);

            Assert.Equal(budget.Existence, Existence.Expected);
        }

        [Fact]
        public void Should_GetBudget_GivenParams()
        {
            var value = 15;
            var id = Guid.NewGuid();
            var label = "label";
            var lifetime = 6;
            var recurrence = Recurrence.Monthly;
            var existence = Existence.Suggested;
            var budget = new BudgetMap(value, id, label, lifetime, recurrence, existence);

            Assert.Equal(budget.Value, value);

            Assert.NotNull(budget.Id);
            Assert.Equal(budget.Id, id);

            Assert.NotNull(budget.Label);
            Assert.Equal(budget.Label, label);

            Assert.NotNull(budget.Lifetime);
            Assert.Equal(budget.Lifetime, lifetime);

            Assert.Equal(budget.Recurrence, recurrence);

            Assert.Equal(budget.Existence, existence);
        }
    }
}