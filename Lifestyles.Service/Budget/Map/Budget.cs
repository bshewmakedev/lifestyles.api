using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Constants;
using Lifestyles.Service.Live.Map;
using DirectionMap = Lifestyles.Domain.Live.Map.Direction;

namespace Lifestyles.Service.Budget.Map
{
    public partial class Budget : Lifestyle, IBudget
    {
        public decimal Amount { get; private set; }
        public Direction Direction { get; private set; }

        public Budget(
            decimal amount = 0,
            Guid? id = null,
            string label = "",
            int? lifetime = null,
            Recurrence recurrence = Recurrence.Never,
            Existence existence = Existence.Expected
        ) : base(id, label, lifetime, recurrence, existence)
        {
            Value(amount);
        }

        public void Value(decimal amount = 0)
        {
            Amount = Math.Abs(amount);
            Direction = DirectionMap.Map((int)(amount / Math.Max(Amount, 1)));
        }
    }
}