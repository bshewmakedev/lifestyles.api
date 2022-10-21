using Lifestyles.Domain.Categorize.Entities;

namespace Lifestyles.Domain.Categorize.Services
{
    public interface ICategorizeService
    {
        IEnumerable<ICategory> FindCategories();
        IEnumerable<ICategory> FindCategoriesByCategoryId(Guid categoryId);
        IEnumerable<ICategory> CategorizeCategories(Guid? categoryId, IEnumerable<ICategory> categories);
        IEnumerable<ICategory> UpsertCategories(IEnumerable<ICategory> categories);
        IEnumerable<ICategory> RemoveCategories(IEnumerable<ICategory> categories);
    }
}