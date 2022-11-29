using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Service.Budget.Models;
using Lifestyles.Service.Categorize.Models;
using Lifestyles.Service.Live.Models;
using Lifestyles.Service.Live.Map;
using RecurrenceEntity = Lifestyles.Domain.Live.Entities.Recurrence;
using ExistenceEntity = Lifestyles.Domain.Live.Entities.Existence;
using RecurrenceMap = Lifestyles.Domain.Live.Map.Recurrence;
using ExistenceMap = Lifestyles.Domain.Live.Map.Existence;

namespace Lifestyles.Service.Budget.Map
{
    public class Budget : Lifestyle, IBudget
    {
        public decimal Value { get; private set; }

        public void Valuate(decimal value = 0)
        {
            Value = value;
        }

        public Budget(
            decimal value = 0,
            Guid? id = null,
            string label = "",
            int? lifetime = null,
            RecurrenceEntity recurrence = RecurrenceEntity.Never,
            ExistenceEntity existence = ExistenceEntity.Expected
        ) : base(id, label, lifetime, recurrence, existence)
        {
            Valuate(value);
        }

        public Budget(DefaultBudget dfBudget) : base(
            label: dfBudget.Label,
            lifetime: dfBudget.Lifetime,
            recurrence: RecurrenceMap.Map(dfBudget.Recurrence),
            existence: ExistenceMap.Map(dfBudget.Existence))
        {
            Valuate(dfBudget.Value);
        }

        public Budget(IBudget budget) : base(
            budget.Id,
            budget.Label,
            budget.Lifetime,
            budget.Recurrence,
            budget.Existence)
        {
            Valuate(budget.Value);
        }

        public Budget(ILifestyle lifestyle, ICategory category) : base(
            label: category.Label,
            lifetime: lifestyle.Lifetime,
            recurrence: lifestyle.Recurrence,
            existence: lifestyle.Existence)
        {
            Valuate();
        }

        public Budget(ILifestyle lifestyle) : base(
            label: lifestyle.Label,
            lifetime: lifestyle.Lifetime,
            recurrence: lifestyle.Recurrence,
            existence: lifestyle.Existence)
        {
            Valuate();
        }
    }
}