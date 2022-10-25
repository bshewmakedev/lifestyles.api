using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Service.Budget.Models;
using Lifestyles.Service.Categorize.Models;
using Lifestyles.Service.Live.Models;
using Lifestyles.Service.Live.Map;
using DirectionEntity = Lifestyles.Domain.Live.Entities.Direction;
using RecurrenceEntity = Lifestyles.Domain.Live.Entities.Recurrence;
using ExistenceEntity = Lifestyles.Domain.Live.Entities.Existence;
using RecurrenceMap = Lifestyles.Service.Live.Map.Recurrence;
using ExistenceMap = Lifestyles.Service.Live.Map.Existence;

namespace Lifestyles.Service.Budget.Map
{
    public partial class Budget : Lifestyle, IBudget
    {
        public decimal Amount { get; private set; }
        public DirectionEntity Direction { get; private set; }

        public void Value(decimal amount = 0)
        {
            Amount = Math.Abs(amount);

            if (amount > 0)
            {
                Direction = DirectionEntity.In;
            }
            else if (amount < 0)
            {
                Direction = DirectionEntity.Out;
            }
            else
            {
                Direction = DirectionEntity.Neutral;
            }
        }

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

        public Budget(DefaultBudget dfBudget)
        {
            Value(dfBudget.Amount);
            Recur(RecurrenceMap.Map(dfBudget.Recurrence), dfBudget.Lifetime);
            Exist(ExistenceMap.Map(dfBudget.Existence));
            Identify();
            Relabel(dfBudget.Label);
        }

        public Budget(DefaultLifestyle dfLifestyle, DefaultCategory dfCategory)
        {
            Value();
            Recur(RecurrenceMap.Map(dfLifestyle.Recurrence), dfLifestyle.Lifetime);
            Exist(ExistenceMap.Map(dfLifestyle.Existence));
            Identify();
            Relabel(dfCategory.Label);
        }

        public Budget(DefaultLifestyle dfLifestyle)
        {
            Value();
            Recur(RecurrenceMap.Map(dfLifestyle.Recurrence), dfLifestyle.Lifetime);
            Exist(ExistenceMap.Map(dfLifestyle.Existence));
            Identify();
            Relabel(dfLifestyle.Label);
        }

        public Budget(ILifestyle lifestyle, ICategory category)
        {
            Value();
            Recur(lifestyle.Recurrence, lifestyle.Lifetime);
            Exist(lifestyle.Existence);
            Identify();
            Relabel(category.Label);
        }

        public Budget(ILifestyle lifestyle)
        {
            Value();
            Recur(lifestyle.Recurrence, lifestyle.Lifetime);
            Exist(lifestyle.Existence);
            Identify();
            Relabel(lifestyle.Label);
        }
    }
}