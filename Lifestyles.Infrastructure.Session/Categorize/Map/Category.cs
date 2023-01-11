using Lifestyles.Infrastructure.Session.Categorize.Models;

namespace Lifestyles.Infrastructure.Session.Categorize.Map
{
    public class Category : Lifestyles.Service.Categorize.Map.Category
    {
        public Category() { }

        public Category(JsonCategory jsonCategory) : base(
            jsonCategory.Id,
            jsonCategory.Alias,
            jsonCategory.Label)
        { }
    }
}