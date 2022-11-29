using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Session.Live.Repositories;
using CategoryMap = Lifestyles.Infrastructure.Session.Categorize.Map.Category;

namespace Lifestyles.Infrastructure.Session.Categorize.Repositories
{
    public class CategoryRepo : EntityRepo<ICategory, CategoryMap>, ICategoryRepo
    {
        public CategoryRepo(IKeyValueRepo keyValueRepo) : base(
            keyValueRepo, 
            Lifestyles.Domain.Tree.Map.NodeType.Category) { }
    }
}