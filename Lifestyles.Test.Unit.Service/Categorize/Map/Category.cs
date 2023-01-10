using Xunit;

namespace Lifestyles.Test.Unit.Service.Categorize.Map
{
    public class Category
    {
        [Fact]
        public void Should_GetCategory_GivenNoParams()
        {
            var category = new Lifestyles.Service.Categorize.Map.Category();

            Assert.NotNull(category.Id);
            Assert.NotEqual(category.Id, Guid.Empty);

            Assert.NotNull(category.Label);
            Assert.Equal(category.Label, "");
        }

        [Fact]
        public void Should_GetCategory_GivenParams()
        {
            var id = Guid.NewGuid();
            var label = "label";
            var category = new Lifestyles.Service.Categorize.Map.Category(id, label);

            Assert.NotNull(category.Id);
            Assert.Equal(category.Id, id);

            Assert.NotNull(category.Label);
            Assert.Equal(category.Label, label);
        }
    }
}