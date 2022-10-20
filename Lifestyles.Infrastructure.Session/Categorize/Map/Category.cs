using Lifestyles.Infrastructure.Session.Categorize.Models;
using CategoryMap = Lifestyles.Service.Categorize.Map.Category;

namespace Lifestyles.Infrastructure.Session.Categorize.Map
{
    public class Category : CategoryMap
    {
        public Category(DbCategory dbCategory) : base(
            dbCategory.Id,
            dbCategory.Label)
        { }
    }
}