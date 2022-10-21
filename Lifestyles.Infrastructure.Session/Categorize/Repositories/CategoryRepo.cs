using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Categorize.Repositories;

namespace Lifestyles.Infrastructure.Session.Categorize.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        public IEnumerable<ICategory> Find(Func<ICategory, bool>? predicate = null)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ICategory> FindCategorizedAs(Guid categoryId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ICategory> Upsert(IEnumerable<ICategory> entities)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ICategory> Remove(IEnumerable<ICategory> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}