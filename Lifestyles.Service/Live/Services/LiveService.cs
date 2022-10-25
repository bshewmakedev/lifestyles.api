using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Service.Budget.Repositories;
using Lifestyles.Service.Categorize.Repositories;
using Lifestyles.Service.Live.Map;
using Lifestyles.Service.Live.Repositories;
using BudgetMap = Lifestyles.Service.Budget.Map.Budget;
using CategoryMap = Lifestyles.Service.Categorize.Map.Category;
using LifestyleMap = Lifestyles.Service.Live.Map.Lifestyle;

namespace Lifestyles.Service.Live.Services
{
    public class LiveService : ILiveService
    {
        private readonly IBudgetRepo _budgetRepo;
        private readonly DefaultBudgetRepo _dfBudgetRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly DefaultCategoryRepo _dfCategoryRepo;
        private readonly ILifestyleRepo _lifestyleRepo;
        private readonly DefaultLifestyleRepo _dfLifestyleRepo;

        public LiveService(
            IBudgetRepo budgetRepo,
            DefaultBudgetRepo dfBudgetRepo,
            ICategoryRepo categoryRepo,
            DefaultCategoryRepo dfCategoryRepo,
            ILifestyleRepo lifestyleRepo,
            DefaultLifestyleRepo dfLifestyleRepo)
        {
            _budgetRepo = budgetRepo;
            _dfBudgetRepo = dfBudgetRepo;
            _categoryRepo = categoryRepo;
            _dfCategoryRepo = dfCategoryRepo;
            _lifestyleRepo = lifestyleRepo;
            _dfLifestyleRepo = dfLifestyleRepo;
        }

        public IEnumerable<Lifestyles.Domain.Live.Entities.Recurrence> FindRecurrences()
        {
            return Enum.GetValues(typeof(Lifestyles.Domain.Live.Entities.Recurrence)).Cast<Lifestyles.Domain.Live.Entities.Recurrence>();
        }

        public IEnumerable<Lifestyles.Domain.Live.Entities.Existence> FindExistences()
        {
            return Enum.GetValues(typeof(Lifestyles.Domain.Live.Entities.Existence)).Cast<Lifestyles.Domain.Live.Entities.Existence>();
        }

        public IEnumerable<Node<IBudget>> FindDefaultLifeTrees()
        {
            var lifeTrees = new List<Node<IBudget>>();
            var dfLifestyles = _dfLifestyleRepo.Find();
            dfLifestyles.ToList().ForEach(dfLifestyle =>
            {
                var nodeLifestyle = new Node<IBudget>(new BudgetMap(dfLifestyle));

                var dfCategories = _dfCategoryRepo.FindBy(dfLifestyle);
                var budgetsByLifestyle = new List<IBudget>();
                dfCategories.ToList().ForEach(dfCategory =>
                {
                    var nodeCategory = new Node<IBudget>(new BudgetMap(dfLifestyle, dfCategory));

                    var dfBudgets = _dfBudgetRepo.FindBy(dfLifestyle, dfCategory);
                    budgetsByLifestyle.AddRange(dfBudgets.Select(d => new BudgetMap(d)));
                    dfBudgets.ToList().ForEach(dfBudget =>
                    {
                        var nodeBudget = new Node<IBudget>(new BudgetMap(dfBudget));

                        // Add node as leaf.
                        nodeCategory.AddNodeAsChild(nodeBudget);
                    });

                    // Calculate & map signed amount.
                    nodeCategory.Value.Value(new CategoryMap(dfCategory).GetSignedAmount(new LifestyleMap(dfLifestyle), dfBudgets.Select(d => new BudgetMap(d))));

                    // Add node.
                    nodeLifestyle.AddNodeAsChild(nodeCategory);
                });

                // Calculate & map signed amount. 
                nodeLifestyle.Value.Value(new LifestyleMap(dfLifestyle).GetSignedAmount(budgetsByLifestyle));

                // Add node as root.
                lifeTrees.Add(nodeLifestyle);
            });

            return lifeTrees;
        }

        public IEnumerable<Node<IBudget>> FindSavedLifeTrees()
        {
            var lifeTrees = new List<Node<IBudget>>();
            var lifestyles = _lifestyleRepo.Find();
            lifestyles.ToList().ForEach(lifestyle =>
            {
                var nodeLifestyle = new Node<IBudget>(new BudgetMap(lifestyle));

                var categories = _categoryRepo.FindCategorizedAs(lifestyle.Id);
                var budgetsByLifestyle = new List<IBudget>();
                categories.ToList().ForEach(category =>
                {
                    var nodeCategory = new Node<IBudget>(new BudgetMap(lifestyle, category));

                    var budgets = _budgetRepo.FindCategorizedAs(category.Id);
                    budgetsByLifestyle.AddRange(budgets);
                    budgets.ToList().ForEach(budget =>
                    {
                        var nodeBudget = new Node<IBudget>(budget);

                        // Add node as leaf.
                        nodeCategory.AddNodeAsChild(nodeBudget);
                    });

                    // Calculate & map signed amount.
                    nodeCategory.Value.Value(category.GetSignedAmount(lifestyle, budgets));

                    // Add node.
                    nodeLifestyle.AddNodeAsChild(nodeCategory);
                });

                // Calculate & map signed amount. 
                nodeLifestyle.Value.Value(lifestyle.GetSignedAmount(budgetsByLifestyle));

                // Add node as root.
                lifeTrees.Add(nodeLifestyle);
            });

            return lifeTrees;
        }

        public IEnumerable<Node<IBudget>> UpsertSavedLifeTrees(IEnumerable<Node<IBudget>> lifeTrees)
        {
            lifeTrees.ToList().ForEach(root =>
            {
                var lifestyle = new LifestyleMap(
                    root.Value.Id,
                    root.Value.Label,
                    root.Value.Lifetime,
                    root.Value.Recurrence,
                    root.Value.Existence
                );
                _lifestyleRepo.Upsert(new[] { lifestyle });

                root.Children.ToList().ForEach(child =>
                {
                    var category = new CategoryMap(
                        child.Value.Id,
                        child.Value.Label
                    );
                    _categoryRepo.Upsert(new[] { category });
                    _categoryRepo.Categorize(lifestyle.Id, new[] { category });

                    child.Children.ToList().ForEach(leaf =>
                    {
                        var budget = new BudgetMap(
                            leaf.Value.Amount,
                            leaf.Value.Id,
                            leaf.Value.Label,
                            leaf.Value.Lifetime,
                            leaf.Value.Recurrence,
                            leaf.Value.Existence
                        );
                        _budgetRepo.Upsert(new[] { budget });
                        _budgetRepo.Categorize(category.Id, new[] { budget });
                    });
                });
            });

            return FindSavedLifeTrees();
        }

        public IEnumerable<IComparison<ILifestyle>> CompareLifestyles(IEnumerable<ILifestyle> lifestyles)
        {
            throw new System.NotImplementedException();
        }
    }
}