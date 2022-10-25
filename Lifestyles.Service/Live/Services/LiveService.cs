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

        public IEnumerable<INode<IBudget>> FindDefaultLifeTrees()
        {
            var lifeTrees = new List<INode<IBudget>>();
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
                        nodeCategory.Children.Add(nodeBudget);
                    });

                    // Calculate & map signed amount.
                    nodeCategory.Value.Value(new CategoryMap(dfCategory).GetSignedAmount(new LifestyleMap(dfLifestyle), dfBudgets.Select(d => new BudgetMap(d))));

                    // Add node.
                    nodeLifestyle.Children.Add(nodeCategory);
                });

                // Calculate & map signed amount. 
                nodeLifestyle.Value.Value(new LifestyleMap(dfLifestyle).GetSignedAmount(budgetsByLifestyle));

                // Add node as root.
                lifeTrees.Add(nodeLifestyle);
            });

            return lifeTrees;
        }

        // var jsonLifestyles = FindDefaultLifestyles();
        // foreach (var jsonLifestyle in jsonLifestyles)
        // {
        //     var lifestyle = new LifestyleMap(
        //         null,
        //         jsonLifestyle.Label,
        //         jsonLifestyle.Lifetime,
        //         RecurrenceMap.Map(jsonLifestyle.Recurrence),
        //         ExistenceMap.Map(jsonLifestyle.Existence));

        //     _lifestyleRepo.Upsert(new[] { lifestyle });

        //     var jsonCategories = FindDefaultCategoriesBy(jsonLifestyle);
        //     foreach (var jsonCategory in jsonCategories)
        //     {
        //         var category = new CategoryMap(
        //             null,
        //             jsonCategory.Label);

        //         _categoryRepo.Upsert(new[] { category });
        //         _categoryRepo.Categorize(lifestyle.Id, new[] { category });

        //         var jsonBudgets = FindDefaultBudgetsBy(jsonLifestyle, jsonCategory);
        //         foreach (var jsonBudget in jsonBudgets)
        //         {
        //             var budget = new BudgetMap(
        //                 jsonBudget.Amount,
        //                 null,
        //                 jsonBudget.Label,
        //                 jsonBudget.Lifetime,
        //                 RecurrenceMap.Map(jsonBudget.Recurrence),
        //                 ExistenceMap.Map(jsonBudget.Existence));

        //             _budgetRepo.Upsert(new[] { budget });
        //             _budgetRepo.Categorize(category.Id, new[] { budget });
        //         }
        //     }
        // }

        public IEnumerable<INode<IBudget>> FindSavedLifeTrees()
        {
            var lifeTrees = new List<INode<IBudget>>();
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
                        nodeCategory.Children.Add(nodeBudget);
                    });

                    // Calculate & map signed amount.
                    nodeCategory.Value.Value(category.GetSignedAmount(lifestyle, budgets));

                    // Add node.
                    nodeLifestyle.Children.Add(nodeCategory);
                });

                // Calculate & map signed amount. 
                nodeLifestyle.Value.Value(lifestyle.GetSignedAmount(budgetsByLifestyle));

                // Add node as root.
                lifeTrees.Add(nodeLifestyle);
            });

            return lifeTrees;
        }

        public IEnumerable<INode<IBudget>> UpsertSavedLifeTrees(IEnumerable<INode<IBudget>> lifeTrees)
        {
            lifeTrees.ToList().ForEach(root => {
                var lifestyle = new BudgetMap(root.Value);
                _lifestyleRepo.Upsert(new[] { lifestyle });

                root.Children.ToList().ForEach(child => {
                    var category = new BudgetMap(root.Value);
                    _categoryRepo.Upsert(new[] { category });

                    child.Children.ToList().ForEach(leaf => {
                        var budget = new BudgetMap(root.Value);
                        _budgetRepo.Upsert(new[] { budget });
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