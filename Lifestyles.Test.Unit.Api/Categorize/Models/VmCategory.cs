using Lifestyles.Api.Categorize.Models;
using Xunit;
using VmCategoryMap = Lifestyles.Api.Categorize.Models.VmCategory;
using CategoryMap = Lifestyles.Service.Categorize.Map.Category;

namespace Lifestyles.Test.Unit.Api.Categorize.Models
{
    public class VmCategory
    {
        [Fact]
        public void Should_GetVmCategory_GivenCategory()
        {
            var category = new CategoryMap(
                Guid.NewGuid(),
                "label");
            var vmCategory = new VmCategoryMap(category);

            Assert.Equal(category.Id, vmCategory.Id);

            Assert.NotNull(category.Label);
            Assert.Equal(category.Label, vmCategory.Label);
        }
    }
}