using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Categorize.Services;

namespace Lifestyles.Service.Categorize.Services
{
    public class CategorizeService : ICategorizeService
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategorizeService(ICategoryRepo categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public IEnumerable<ICategory> FindCategories()
        {
            return _categoryRepo.Find();
        }

        public IEnumerable<ICategory> FindCategoriesByCategoryId(Guid categoryId)
        {
            return _categoryRepo.FindCategorizedAs(categoryId);
        }

        public IEnumerable<ICategory> CategorizeCategories(Guid? categoryId, IEnumerable<ICategory> categories)
        {
            return _categoryRepo.Categorize(categoryId, categories);
        }

        public IEnumerable<ICategory> UpsertCategories(IEnumerable<ICategory> categories)
        {
            return _categoryRepo.Upsert(categories);
        }

        public IEnumerable<ICategory> RemoveCategories(IEnumerable<ICategory> categories)
        {
            return _categoryRepo.Remove(categories);
        }
    }
}