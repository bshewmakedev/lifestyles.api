using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Domain.Categorize.Entities
{
    public partial interface ICategory : IIdentified
    {
        IEnumerable<IBudget> Budgets { get; }

        void Recategorize(IEnumerable<IBudget> budgets);
        void Decategorize(IEnumerable<IBudget> budgets);
    }

    public partial interface ICategory
    {
        string Label { get; }

        void Relabel(string label = "");
    }
}