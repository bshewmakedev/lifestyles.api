using Lifestyles.Domain.Budget.Constants;
using BudgetTypeEnum = Lifestyles.Domain.Budget.Constants.BudgetType;

namespace Lifestyles.Domain.Budget.Map
{
    public static class BudgetType
    {
        public static BudgetTypeEnum Map(this string budgetType)
        {
            switch (budgetType)
            {
                case BudgetTypeAlias.Category: return BudgetTypeEnum.Category;
                case BudgetTypeAlias.Lifestyle: return BudgetTypeEnum.Lifestyle;
                default: return BudgetTypeEnum.Budget;
            }
        }

        public static string Map(this BudgetTypeEnum budgetType)
        {
            switch (budgetType)
            {
                case BudgetTypeEnum.Category: return BudgetTypeAlias.Category;
                case BudgetTypeEnum.Lifestyle: return BudgetTypeAlias.Lifestyle;
                default: return BudgetTypeAlias.Budget;
            }
        }
    }
}