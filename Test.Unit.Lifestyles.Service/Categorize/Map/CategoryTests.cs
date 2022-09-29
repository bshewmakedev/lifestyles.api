using Xunit;
using Lifestyles.Service.Categorize.Map;

namespace Test.Unit.Lifestyles.Service.Categorize.Map
{
    public class BudgetTests
    {
        [Fact]
        public void Category_ShouldConstruct()
        {
            var category = new Category();
            
            Assert.NotNull(category.Budgets);
            Assert.False(string.IsNullOrWhiteSpace(category.Id.ToString()));
        }
    }
}