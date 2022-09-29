using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;

namespace Lifestyles.Service.Categorize.Map
{
    public class Category : ICategory
    {
        public IEnumerable<IBudget> Budgets { get; set; }
        public Guid Id { get; set; }
        public string Label { get; set; } = string.Empty;

        public Category()
        {
            Budgets = new List<IBudget>();
            Id = Guid.NewGuid();
        }
    }
}