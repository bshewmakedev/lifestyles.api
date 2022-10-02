using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Repositories;

namespace Lifestyles.Domain.Categorize.Repositories
{
    public interface ICategoryRepo : IRepository<ICategory> 
    { 
        IEnumerable<ICategory> FindCategorizedAs(Guid categoryId);
    }
}