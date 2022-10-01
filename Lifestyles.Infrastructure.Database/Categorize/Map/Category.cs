using Lifestyles.Infrastructure.Database.Categorize.Models;
using CategoryMap = Lifestyles.Service.Categorize.Map.Category;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Categorize.Map
{
    public class Category : CategoryMap
    {
        public Category(DbCategory dbCategory) : base(
            dbCategory.Id,
            dbCategory.Label)
        { }
    }
}