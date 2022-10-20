using Xunit;
using Lifestyles.Infrastructure.Default.Categorize.Repositories;
using Lifestyles.Service.Categorize.Map;

namespace Test.Integration.Lifestyles.Infrastructure.Default.Categorize.Repositories
{
    public class CategoryRepoTests
    {
        [Fact]
        public void Can_FindCategories_ByLifestyle()
        {
            var repo = new CategoryRepo();

            var entities = repo.FindBy(new Category(null, "Thru-Hiker"));

            Assert.NotEmpty(entities);
        }
    }
}