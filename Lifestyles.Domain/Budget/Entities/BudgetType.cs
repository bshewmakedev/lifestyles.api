namespace Lifestyles.Domain.Budget.Entities
{
    public enum BudgetType
    {
        Budget,
        Category,
        Lifestyle
    }

    public class BudgetTypeAlias
    {
        public const string Budget = "budget";
        public const string Category = "category";
        public const string Lifestyle = "lifestyle";
    }
}