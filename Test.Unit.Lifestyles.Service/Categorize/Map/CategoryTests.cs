using Xunit;
using Lifestyles.Service.Categorize.Map;

namespace Test.Unit.Lifestyles.Service.Categorize.Map
{
    public class CategoryTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData(null, "")]
        public void Category_ShouldConstruct(
            Guid id, 
            string label)
        {
            var category = new Category(id, label);
            
            Assert.NotNull(category.Budgets);
            Assert.False(string.IsNullOrWhiteSpace(category.Id.ToString()));
        }
    }
}