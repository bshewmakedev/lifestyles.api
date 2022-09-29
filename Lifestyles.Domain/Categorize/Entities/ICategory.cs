using Lifestyles.Domain.Budget.Entities;

namespace Lifestyles.Domain.Categorize.Entities
{
    public interface ICategory
    {
        IEnumerable<IBudget> Budgets { get; set; }
        Guid Id { get; set; }
        string Label { get; set; }
    }
}