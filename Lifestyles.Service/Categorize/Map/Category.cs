using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Categorize.Entities;

namespace Lifestyles.Service.Categorize.Map
{
    public partial class Category : ICategory
    {
        public Guid? Id { get; set; }
        public string Label { get; set; } = string.Empty;

        public Category(
            Guid? id = null,
            string label = "")
        {
            Identify(id);
            Relabel(label);
        }

        public void Identify(Guid? id = null)
        {
            Id = id.HasValue ? id.Value : Guid.NewGuid();
        }

        public void Relabel(string label = "")
        {
            Label = label;
        }
    }
}