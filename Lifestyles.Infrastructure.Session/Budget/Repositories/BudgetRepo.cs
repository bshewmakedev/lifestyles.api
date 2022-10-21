using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Session.Live.Repositories;
using BudgetMap = Lifestyles.Infrastructure.Session.Budget.Map.Budget;

namespace Lifestyles.Infrastructure.Session.Budget.Repositories
{
    public class BudgetRepo : EntityRepo<IBudget, BudgetMap>, IBudgetRepo
    {
        public BudgetRepo(IKeyValueRepo keyValueRepo) : base(keyValueRepo, "budget") { }
    }
}