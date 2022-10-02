using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Constants;
using Lifestyles.Service.Live.Map;

namespace Lifestyles.Service.Budget.Map
{
    public partial class Budget : Lifestyle, IBudget
    {
        public Budget(
            decimal amount,
            Guid? id = null,
            string label = "",
            decimal? lifetime = null,
            Recurrence recurrence = Recurrence.Never,
            Existence existence = Existence.Excluded
        ) : base(id, label, lifetime, recurrence, existence)
        {
            Value(amount);
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
}