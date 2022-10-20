using Xunit;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Infrastructure.Default.Budget.Repositories;
using Lifestyles.Service.Categorize.Map;

namespace Test.Integration.Lifestyles.Infrastructure.Default.Budget.Repositories
{
    public class BudgetRepoTests
    {
        [Fact]
        public void Can_FindBudgets_ByLifestyleCategory()
        {
            var repo = new BudgetRepo();

            var entities = repo.FindBy(new[] { 
                new Category(null, "Thru-Hiker"),
                new Category(null, "Shelter"),
            });

            Assert.NotEmpty(entities);
        }
    }
}