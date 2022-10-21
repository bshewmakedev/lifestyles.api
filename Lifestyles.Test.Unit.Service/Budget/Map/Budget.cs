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

            Assert.Equal(budget.Amount, 0);
            Assert.Equal(budget.Direction, Direction.Neutral);

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
            var amount = 15;
            var direction = -1;
            var id = Guid.NewGuid();
            var label = "label";
            var lifetime = 6;
            var recurrence = Recurrence.Monthly;
            var existence = Existence.Suggested;
            var budget = new BudgetMap(amount * direction, id, label, lifetime, recurrence, existence);

            Assert.Equal(budget.Amount, amount);
            Assert.Equal(budget.Direction, Direction.Out);

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