using Lifestyles.Infrastructure.Default.Categorize.Models;
using CategoryMap = Lifestyles.Service.Categorize.Map.Category;

namespace Lifestyles.Infrastructure.Default.Categorize.Map
{
    public class Category : CategoryMap
    {
        public Category(JsonCategory jsonCategory) : base(
            null,
            jsonCategory.Label)
        { }
    }
}