using BudgetTypeEnum = Lifestyles.Domain.Budget.Entities.BudgetType;

namespace Lifestyles.Api.Budget.Map
{
    public static class BudgetType
    {
        public static BudgetTypeEnum Map(this string budgetType)
        {
            switch (budgetType)
            {
                case "category": return BudgetTypeEnum.Category;
                case "lifestyle": return BudgetTypeEnum.Lifestyle;
                default: return BudgetTypeEnum.Budget;
            }
        }
    }
}