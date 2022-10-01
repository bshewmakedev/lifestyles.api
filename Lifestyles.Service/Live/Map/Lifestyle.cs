using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Constants;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Service.Categorize.Map;

namespace Lifestyles.Service.Live.Map
{
    public partial class Lifestyle : Category, ILifestyle
    {
        public Lifestyle(
            Guid? id = null,
            string label = "",
            Recurrence recurrence = Recurrence.Never,
            Existence existence = Existence.Excluded
        ) : base(id, label)
        {
            Recur(recurrence);
            Exist(existence);
        }
    }

    public partial class Lifestyle
    {
        public IEnumerable<ICategory> Categories { get; private set; } = new List<ICategory>();

        public void RecategorizeAs(IEnumerable<ICategory> categories)
        {
            Categories = Categories
                .Concat(categories)
                .GroupBy(c => c.Id)
                .Select(c => c.First());
        }

        public void DecategorizeAs(IEnumerable<ICategory> categories)
        {
            Categories = Categories
                .Where(c => categories.All(c2 => !c.Id.Equals(c2.Id)));
        }
    }

    public partial class Lifestyle
    {
        public Existence Existence { get; private set; }

        public void Exist(Existence existence)
        {
            Existence = existence;
        }
    }

    public partial class Lifestyle
    {
        public decimal? Lifetime { get; private set; }
        public Recurrence Recurrence { get; private set; }

        public void Recur(Recurrence recurrence, decimal? lifetime = null)
        {
            Recurrence = recurrence;

            if (recurrence.Equals(Lifestyles.Domain.Live.Constants.Recurrence.Never))
            {
                Lifetime = null;
            }
            else
            {
                Lifetime = Lifetime ?? 6;
            }
        }
    }
}