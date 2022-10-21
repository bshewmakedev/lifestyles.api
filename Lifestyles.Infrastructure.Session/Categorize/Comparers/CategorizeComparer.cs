using Lifestyles.Infrastructure.Session.Categorize.Models;

namespace Lifestyles.Infrastructure.Session.Categorize.Comparers
{
    public class CategorizeComparer : IEqualityComparer<JsonCategorize>
    {
        public bool Equals(JsonCategorize x, JsonCategorize y)
        {
            return x.EntityId.Equals(y.EntityId) && x.CategoryId.Equals(y.CategoryId);
        }

        public int GetHashCode(JsonCategorize x)
        {
            return HashCode.Combine(x.EntityId.GetHashCode(), x.CategoryId.GetHashCode());
        }
    }
}