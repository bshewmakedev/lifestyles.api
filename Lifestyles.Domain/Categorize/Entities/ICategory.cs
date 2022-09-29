using Lifestyles.Domain.Budget.Entities;

namespace Lifestyles.Domain.Categorize.Entities
{
    public partial interface ICategory
    {
        Guid Id { get; }

        void Identify(Guid? id);
    }

    public partial interface ICategory
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