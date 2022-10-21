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
                _keyValueRepo.SetItem("tbl_Budget", _keyValueRepo
                    .GetItem<List<JsonBudget>>("tbl_Budget")
                    .Where(b => !b.BudgetType.Equals("lifestyle"))
                    .Union(value)
                    .ToList());
            }
        }
        
        private readonly IKeyValueRepo _keyValueRepo;

        public LifestyleRepo(IKeyValueRepo keyValueRepo)
        {
            _keyValueRepo = keyValueRepo;
        }

        public IEnumerable<ILifestyle> Find(Func<ILifestyle, bool>? predicate = null)
        {
            return _jsonLifestyles
                .Select(l => new LifestyleMap(l))
                .Where(l => predicate == null ? true : predicate(l));
        }

        public IEnumerable<ILifestyle> Upsert(IEnumerable<ILifestyle> lifestyles)
        {
            var lifestylesMerged = lifestyles
                .Union(Find(), new IdentifiedComparer<ILifestyle>());

            _jsonLifestyles = lifestylesMerged.Select(l => new JsonBudget(l)).ToList();
            
            return Find();
        }

        public IEnumerable<ILifestyle> Remove(IEnumerable<ILifestyle> lifestyles)
        {
            var lifestylesFiltered = Find().Except(lifestyles, new IdentifiedComparer<ILifestyle>());

            _jsonLifestyles = lifestylesFiltered.Select(l => new JsonBudget(l)).ToList();
            
            return Find();
        }
    }
}