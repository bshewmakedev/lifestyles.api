using Lifestyles.Domain.Live.Entities;
using Lifestyles.Api.Budget.Models;
using Xunit;
using VmBudgetMap = Lifestyles.Api.Budget.Models.VmBudget;
using BudgetMap = Lifestyles.Service.Budget.Map.Budget;
using DirectionMap = Lifestyles.Api.Live.Map.Direction;
using RecurrenceMap = Lifestyles.Api.Live.Map.Recurrence;
using ExistenceMap = Lifestyles.Api.Live.Map.Existence;

namespace Lifestyles.Test.Unit.Api.Budget.Models
{
    public class VmBudget
    {
        [Fact]
        public void Should_GetVmBudget_GivenBudget()
        {
            var budget = new BudgetMap(
                -15,
                Guid.NewGuid(),
                "label",
                6,
                Recurrence.Monthly,
                Existence.Suggested
            );
            var vmBudget = new VmBudgetMap(budget);

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