using Lifestyles.Api.Categorize.Models;
using CategoryMap = Lifestyles.Service.Categorize.Map.Category;

namespace Lifestyles.Api.Categorize.Map
{
    public class Category : CategoryMap
    {
        public Category(VmCategory vmCategory) : base(
            vmCategory.Id,
            vmCategory.Label)
        { }
    }
}