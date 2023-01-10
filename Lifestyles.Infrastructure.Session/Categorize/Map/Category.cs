using Lifestyles.Infrastructure.Session.Budget.Models;

namespace Lifestyles.Infrastructure.Session.Categorize.Map
{
    public class Category : Lifestyles.Service.Categorize.Map.Category
    {
        public Category() { }

        public Category(JsonBudget jsonBudget) : base(
            jsonBudget.Id,
            jsonBudget.Alias,
            jsonBudget.Label)
        { }
    }
}