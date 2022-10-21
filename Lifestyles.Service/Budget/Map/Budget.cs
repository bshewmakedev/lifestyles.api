using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Service.Live.Map;
using DirectionEntity = Lifestyles.Domain.Live.Entities.Direction;
using RecurrenceEntity = Lifestyles.Domain.Live.Entities.Recurrence;
using ExistenceEntity = Lifestyles.Domain.Live.Entities.Existence;

namespace Lifestyles.Service.Budget.Map
{
    public partial class Budget : Lifestyle, IBudget
    {
        public decimal Amount { get; private set; }
        public DirectionEntity Direction { get; private set; }

        public Budget(
            decimal amount = 0,
            Guid? id = null,
            string label = "",
            int? lifetime = null,
            RecurrenceEntity recurrence = RecurrenceEntity.Never,
            ExistenceEntity existence = ExistenceEntity.Expected
        ) : base(id, label, lifetime, recurrence, existence)
        {
            Value(amount);
        }

        public void Value(decimal amount = 0)
        {
            Amount = Math.Abs(amount);

            if ((int)amount > 0)
            {
                Direction = DirectionEntity.In;
            }
            else if ((int)amount < 0)
            {
                Direction = DirectionEntity.Out;
            }
            else
            {
                Direction = DirectionEntity.Neutral;
            }
        }
    }
}