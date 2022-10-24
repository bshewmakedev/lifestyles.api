using Lifestyles.Domain.Live.Entities;
using Lifestyles.Api.Budget.Models;
using Xunit;
using BudgetMap = Lifestyles.Api.Budget.Map.Budget;
using DirectionMap = Lifestyles.Api.Live.Map.Direction;
using RecurrenceMap = Lifestyles.Api.Live.Map.Recurrence;
using ExistenceMap = Lifestyles.Api.Live.Map.Existence;

namespace Lifestyles.Test.Unit.Api.Budget.Map
{
    public class Budget
    {
        [Fact]
        public void Should_GetBudget_GivenVmBudget()
        {
            var vmBudget = new VmBudget
            {
                Amount = -15,
                Id = Guid.NewGuid(),
                Label = "label",
                Lifetime = 6,
                Recurrence = "monthly",
                Existence = "suggested"
            };
            var budget = new BudgetMap(vmBudget);

            Assert.Equal(budget.Amount, Math.Abs(vmBudget.Amount));
            Assert.Equal(budget.Direction, DirectionMap.Map(vmBudget.Amount));

            Assert.Equal(budget.Id, vmBudget.Id);

            Assert.NotNull(budget.Label);
            Assert.Equal(budget.Label, vmBudget.Label);

            Assert.NotNull(budget.Lifetime);
            Assert.Equal(budget.Lifetime, vmBudget.Lifetime);

            Assert.Equal(budget.Recurrence, RecurrenceMap.Map(vmBudget.Recurrence));

            Assert.Equal(budget.Existence, ExistenceMap.Map(vmBudget.Existence));
        }
    }
}