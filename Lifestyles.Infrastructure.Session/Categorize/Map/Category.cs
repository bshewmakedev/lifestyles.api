using Lifestyles.Infrastructure.Session.Budget.Models;
using CategoryMap = Lifestyles.Service.Categorize.Map.Category;

namespace Lifestyles.Infrastructure.Session.Categorize.Map
{
    public class Category : CategoryMap
    {
        public Category(JsonBudget jsonBudget) : base(
            jsonBudget.Id,
            jsonBudget.Label)
        { }
    }
}