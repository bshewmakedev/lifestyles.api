using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Measure.Constants;
using Lifestyles.Service.Categorize.Map;

namespace Lifestyles.Service.Budget.Map
{
    public partial class Budget : Category, IBudget
    {
        public Budget(
            decimal amount,
            Guid? id = null,
            string label = "",
            Recurrence recurrence = Recurrence.Never
        ) : base()
        {
            Identify(id);
            Value(amount);
            Relabel(label);
            Recur(recurrence);
        }
    }

    public partial class Budget
    {
        public decimal Amount { get; private set; }
        public Direction Direction { get; private set; }

        public void Value(decimal amount)
        {
            Amount = Math.Abs(amount);
            Direction = amount > 0 ? Direction.In : Direction.Out;
        }
    }

    public partial class Budget
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

    public partial class Budget
    {
        public Existence Existence { get; private set; }

        public void Exclude()
        {
            Existence = Existence.Excluded;
        }

        public void Expect()
        {
            Existence = Existence.Expected;
        }

        public void Suggest()
        {
            Existence = Existence.Suggested;
        }
    }

    public partial class Budget
    {
        public decimal? Lifetime { get; private set; }
        public Recurrence Recurrence { get; private set; }

        public void Recur(Recurrence recurrence, decimal? lifetime = null)
        {
            Recurrence = recurrence;

            if (recurrence.Equals(Lifestyles.Domain.Measure.Constants.Recurrence.Never))
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