using BudgetTypeEnum = Lifestyles.Domain.Budget.Constants.BudgetType;
using Lifestyles.Infrastructure.Database.Budget.Models;

namespace Lifestyles.Infrastructure.Database.Budget.Map
{
    public static class BudgetType
    {
        public static BudgetTypeEnum Map(DbBudgetType dbBudgetType)
        {
            // const string budgetTypeBudget = "budget";
            const string budgetTypeCategory = "category";
            const string budgetTypeLifestyle = "lifestyle";

            switch (dbBudgetType.Alias)
            {
                case budgetTypeCategory: return BudgetTypeEnum.Category;
                case budgetTypeLifestyle: return BudgetTypeEnum.Lifestyle;
                default: return BudgetTypeEnum.Budget;
            }
        }
    }
}