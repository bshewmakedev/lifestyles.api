using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Categorize.Services;

namespace Lifestyles.Service.Categorize.Services
{
    public class CategorizeService : ICategorizeService
    {
        public IEnumerable<ICategory> FindCategories()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ICategory> FindCategoriesByCategoryId(Guid categoryId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ICategory> UpsertCategories(IEnumerable<ICategory> budgets)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ICategory> RemoveCategories(IEnumerable<ICategory> budgets)
        {
            throw new System.NotImplementedException();
        }
    }
}