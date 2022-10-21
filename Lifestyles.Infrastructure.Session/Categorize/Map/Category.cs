using Lifestyles.Infrastructure.Session.Budget.Models;
using Lifestyles.Infrastructure.Session.Categorize.Models;
using CategoryMap = Lifestyles.Service.Categorize.Map.Category;

namespace Lifestyles.Infrastructure.Session.Categorize.Map
{
    public class Category : CategoryMap
    {
        public Category(
            JsonBudget jsonBudget,
            JsonCategorize? jsonCategorize = null) : base(
            jsonBudget.Id,
            jsonBudget.Label)
        { }
    }
}