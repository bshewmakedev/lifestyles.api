using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Service.Budget.Repositories;
using Lifestyles.Service.Categorize.Repositories;
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
            // Map lifestyles to roots.
            var dfLifestyles = _dfLifestyleRepo.Find();
            return dfLifestyles.Select(dfLifestyle =>
            {
                var lifestyle = new LifestyleMap(dfLifestyle);
                var lifestyleNode = new Node<IBudget>(new BudgetMap(lifestyle));

                // Map categories to children.
                var budgetsByLifestyle = new List<IBudget>();
                var dfCategories = _dfCategoryRepo.FindBy(dfLifestyle);
                lifestyleNode.AddNodesAsChild(dfCategories.Select(dfCategory =>
                {
                    var category = new CategoryMap(dfCategory);
                    var categoryNode = new Node<IBudget>(new BudgetMap(lifestyle, category));

                    // Map budgets to leaves.
                    var budgetsByCategory = _dfBudgetRepo.FindBy(dfLifestyle, dfCategory).Select(e => new BudgetMap(e));
                    categoryNode.AddNodesAsChild(budgetsByCategory.Select(e => new Node<IBudget>(e)));
                    categoryNode.Value.Value(category.GetSignedAmount(lifestyle, budgetsByCategory));
                    budgetsByLifestyle.AddRange(budgetsByCategory);

                    return categoryNode;
                }));
                lifestyleNode.Value.Value(lifestyle.GetSignedAmount(budgetsByLifestyle));

                return lifestyleNode;
            });
        }

        public IEnumerable<Node<IBudget>> FindSavedLifeTrees()
        {
            // Map lifestyles to roots.
            var lifestyles = _lifestyleRepo.Find();
            return lifestyles.Select(lifestyle =>
            {
                var lifestyleNode = new Node<IBudget>(new BudgetMap(lifestyle));

                // Map categories to children.
                var budgetsByLifestyle = new List<IBudget>();
                var categories = _categoryRepo.FindCategorizedAs(lifestyleNode.Value.Id);
                lifestyleNode.AddNodesAsChild(categories.Select(category =>
                {
                    var categoryNode = new Node<IBudget>(new BudgetMap(lifestyle, category));

                    // Map budgets to leaves.
                    var budgetsByCategory = _budgetRepo.FindCategorizedAs(categoryNode.Value.Id);
                    categoryNode.AddNodesAsChild(budgetsByCategory.Select(e => new Node<IBudget>(new BudgetMap(e))));
                    categoryNode.Value.Value(new CategoryMap(categoryNode.Value).GetSignedAmount(lifestyle, budgetsByCategory));
                    budgetsByLifestyle.AddRange(budgetsByCategory);

                    return categoryNode;
                }));
                lifestyleNode.Value.Value(lifestyle.GetSignedAmount(budgetsByLifestyle));

                return lifestyleNode;
            });
        }

        public IEnumerable<Node<IBudget>> UpsertSavedLifeTrees(IEnumerable<Node<IBudget>> lifeTrees)
        {
            // Map roots to lifestyles; then upsert.
            var lifestyles = lifeTrees.ToList().Select(node => new LifestyleMap(node.Value));
            _lifestyleRepo.Upsert(lifestyles);
            lifeTrees.ToList().ForEach(root =>
            {
                // Map children to categories; then upsert & categorize as lifestyles.
                var categories = root.Children.ToList().Select(node => new CategoryMap(node.Value));
                _categoryRepo.Upsert(categories);
                _categoryRepo.Categorize(root.Value.Id, categories);
                root.Children.ToList().ForEach(child =>
                {
                    // Map leaves to budgets; then upsert & categorize as categories.
                    var budgets = child.Children.ToList().Select(node => new BudgetMap(node.Value));
                    _budgetRepo.Upsert(budgets);
                    _budgetRepo.Categorize(child.Value.Id, budgets);
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