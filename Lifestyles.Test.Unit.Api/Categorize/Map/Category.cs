using Lifestyles.Api.Categorize.Models;
using Xunit;
using CategoryMap = Lifestyles.Api.Categorize.Map.Category;

namespace Lifestyles.Test.Unit.Api.Categorize.Map
{
    public class Category
    {
        [Fact]
        public void Should_GetCategory_GivenVmCategory()
        {
            var vmCategory = new VmCategory
            {
                Id = Guid.NewGuid(),
                Label = "label"
            };
            var category = new CategoryMap(vmCategory);

            Assert.Equal(category.Id, vmCategory.Id);

            Assert.NotNull(category.Label);
            Assert.Equal(category.Label, vmCategory.Label);
        }
    }
}