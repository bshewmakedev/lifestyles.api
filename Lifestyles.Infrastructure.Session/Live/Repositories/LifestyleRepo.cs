using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Comparers;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Session.Budget.Models;
using LifestyleMap = Lifestyles.Infrastructure.Session.Live.Map.Lifestyle;

namespace Lifestyles.Infrastructure.Session.Live.Repositories
{
    public class LifestyleRepo : ILifestyleRepo
    {
        private List<JsonBudget> _jsonLifestyles
        { 
            get
            { 
                return _keyValueRepo
                    .GetItem<List<JsonBudget>>("tbl_Budget")
                    .Where(b => b.BudgetType.Equals("lifestyle"))
                    .ToList();
            }
            set
            {
                _keyValueRepo.SetItem("tbl_Budget", value);
            }
        }
        
        private readonly IKeyValueRepo _keyValueRepo;

        public LifestyleRepo(IKeyValueRepo keyValueRepo)
        {
            _keyValueRepo = keyValueRepo;
        }

        public IEnumerable<ILifestyle> Find(Func<ILifestyle, bool>? predicate = null)
        {
            return _jsonLifestyles.Select(l => new LifestyleMap(l));
        }

        public IEnumerable<ILifestyle> Upsert(IEnumerable<ILifestyle> lifestyles)
        {
            var lifestylesMerged = lifestyles
                .Except(
                    _jsonLifestyles.Select(l => new LifestyleMap(l)),
                    new IdentifiedComparer<ILifestyle>());

            _jsonLifestyles = lifestylesMerged.Select(l => new JsonBudget(l)).ToList();
            
            return lifestylesMerged;
        }

        public IEnumerable<ILifestyle> Remove(IEnumerable<ILifestyle> lifestyles)
        {
            var lifestylesFiltered = _jsonLifestyles
                .Select(l => new LifestyleMap(l))
                .Except(lifestyles, new IdentifiedComparer<ILifestyle>());

            _jsonLifestyles = lifestylesFiltered.Select(l => new JsonBudget(l)).ToList();
            
            return lifestylesFiltered;
        }
    }
}