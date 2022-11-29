using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Service.Categorize.Map;
using Lifestyles.Service.Live.Models;
using RecurrenceEntity = Lifestyles.Domain.Live.Entities.Recurrence;
using ExistenceEntity = Lifestyles.Domain.Live.Entities.Existence;
using RecurrenceMap = Lifestyles.Domain.Live.Map.Recurrence;
using ExistenceMap = Lifestyles.Domain.Live.Map.Existence;

namespace Lifestyles.Service.Live.Map
{
    public class Lifestyle : Category, ILifestyle
    {
        public decimal GetValue(
            IEnumerable<IBudget> budgets,
            int? interval = null)
        {
            return base.GetValue(this, budgets, interval);
        }

        public int? Lifetime { get; private set; }
        public RecurrenceEntity Recurrence { get; private set; }

        public void Recur(RecurrenceEntity recurrence = RecurrenceEntity.Never, int? lifetime = null)
        {
            Recurrence = recurrence;
            Lifetime = recurrence.Equals(Lifestyles.Domain.Live.Entities.Recurrence.Never) ? null : lifetime;
        }

        public ExistenceEntity Existence { get; private set; }

        public void Exist(ExistenceEntity existence = ExistenceEntity.Expected)
        {
            Existence = existence;
        }

        public Lifestyle(
            Guid? id = null,
            string label = "",
            int? lifetime = null,
            RecurrenceEntity recurrence = RecurrenceEntity.Never,
            ExistenceEntity existence = ExistenceEntity.Expected
        ) : base(id, label)
        {
            Recur(recurrence, lifetime);
            Exist(existence);
        }

        public Lifestyle(DefaultLifestyle dfLifestyle) : base(label: dfLifestyle.Label)
        {
            Recur(RecurrenceMap.Map(dfLifestyle.Recurrence), dfLifestyle.Lifetime);
            Exist(ExistenceMap.Map(dfLifestyle.Existence));
        }

        public Lifestyle(IBudget budget) : base(budget.Id, budget.Label)
        {
            Recur(budget.Recurrence, budget.Lifetime);
            Exist(budget.Existence);
        }
    }
}