using Lifestyles.Infrastructure.Session.Categorize.Models;

namespace Lifestyles.Infrastructure.Session.Categorize.Comparers
{
    public class EntityComparer : IEqualityComparer<JsonCategorize>
    {
        public bool Equals(JsonCategorize x, JsonCategorize y)
        {
            return x.EntityId.Equals(y.EntityId);
        }

        public int GetHashCode(JsonCategorize x)
        {
            return x.EntityId.GetHashCode();
        }
    }
}