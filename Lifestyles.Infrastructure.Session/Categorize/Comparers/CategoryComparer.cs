using Lifestyles.Infrastructure.Session.Categorize.Models;

namespace Lifestyles.Infrastructure.Session.Categorize.Comparers
{
    public class CategoryComparer : IEqualityComparer<JsonCategorize>
    {
        public bool Equals(JsonCategorize x, JsonCategorize y)
        {
            return x.CategoryId.Equals(y.CategoryId);
        }

        public int GetHashCode(JsonCategorize x)
        {
            return x.CategoryId.GetHashCode();
        }
    }
}