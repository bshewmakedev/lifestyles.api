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
    }
}