using Xunit;
using Lifestyles.Infrastructure.Default.Live.Repositories;

namespace Test.Integration.Lifestyles.Infrastructure.Default.Live.Repositories
{
    public class LifestyleRepoTests
    {
        [Fact]
        public void Can_FindLifestyles()
        {
            var repo = new LifestyleRepo();

            var entities = repo.Find();

            Assert.NotEmpty(entities);
        }
    }
}