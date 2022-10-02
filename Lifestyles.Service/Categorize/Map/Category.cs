using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;

namespace Lifestyles.Service.Categorize.Map
{
    public partial class Category : ICategory
    {
        public Category(
            Guid? id = null,
            string label = "")
        {
            Identify(id);
            Relabel(label);
        }
    }

    public partial class Category
    {
        public Guid Id { get; set; }

        public void Identify(Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();
        }
    }

    public partial class Category
    {
        public string Label { get; set; } = string.Empty;

        public void Relabel(string label = "")
        {
            Label = label;
        }
    }
}