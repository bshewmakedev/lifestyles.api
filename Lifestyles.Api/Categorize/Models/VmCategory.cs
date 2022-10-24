using Lifestyles.Domain.Categorize.Entities;

namespace Lifestyles.Api.Categorize.Models
{
    public class VmCategory
    {
        public Guid? Id { get; set; }
        public string Label { get; set; } = "";

        public VmCategory() { }

        public VmCategory(ICategory category)
        {
            Id = category.Id;
            Label = category.Label;
        }
    }
}