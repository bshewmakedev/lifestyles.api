using Lifestyles.Api.Live.Models;
using Lifestyles.Domain.Budget.Entities;
namespace Lifestyles.Api.Budget.Models
{
    public class VmBudget : VmLifestyle
    {
        public decimal Amount { get; set; }

        public VmBudget(IBudget budget) : base(budget)
        {
            Amount = budget.Amount;
        }
    }
}