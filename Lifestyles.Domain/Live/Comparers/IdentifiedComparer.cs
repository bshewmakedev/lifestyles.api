using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Domain.Live.Comparers
{
    public class IdentifiedComparer : IEqualityComparer<IIdentified>
    {
        public bool Equals(IIdentified x, IIdentified y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(IIdentified obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}