using Lifestyles.Domain.Budget.Entities;

namespace Lifestyles.Api.Budget.Models
{
    public static class VmBudgetType
    {
        public static string Map(this BudgetType budgetType)
        {
            switch (budgetType)
            {
                case BudgetType.Category: return "category";
                case BudgetType.Lifestyle: return "lifestyle";
                default: return "budget";
            }
        }
    }
}