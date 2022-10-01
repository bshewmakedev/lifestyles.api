using Lifestyles.Api.Budget.Models;
using BudgetMap = Lifestyles.Service.Budget.Map.Budget;

namespace Lifestyles.Api.Budget.Map
{
    public class Budget : BudgetMap
    {
        public Budget(VmBudget vmBudget) : base(
            vmBudget.Amount,
            vmBudget.Id,
            vmBudget.Label,
            Lifestyles.Domain.Live.Map.Recurrence.Map(vmBudget.Recurrence),
            Lifestyles.Domain.Live.Map.Existence.Map(vmBudget.Existence))
        { }
    }
}