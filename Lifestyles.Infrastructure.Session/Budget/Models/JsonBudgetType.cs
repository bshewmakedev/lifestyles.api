using Lifestyles.Domain.Budget.Entities;

namespace Lifestyles.Infrastructure.Session.Budget.Models
{
    public static class JsonBudgetType
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