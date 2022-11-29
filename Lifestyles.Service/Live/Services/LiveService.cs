using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Domain.Tree.Entities;
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
                lifestyleNode.AddNodesAsChildren(dfCategories.Select(dfCategory =>
                {
                    var category = new CategoryMap(dfCategory);
                    var categoryNode = new Node<IBudget>(new BudgetMap(lifestyle, category));

                    // Map budgets to leaves.
                    var budgetsByCategory = _dfBudgetRepo.FindBy(dfLifestyle, dfCategory).Select(e => new BudgetMap(e));
                    categoryNode.AddNodesAsChildren(budgetsByCategory.Select(e => new Node<IBudget>(e)).ToArray());
                    categoryNode.Entity.Valuate(category.GetValue(lifestyle, budgetsByCategory));
                    budgetsByLifestyle.AddRange(budgetsByCategory);

                    return categoryNode;
                }).ToArray());
                lifestyleNode.Entity.Valuate(lifestyle.GetValue(budgetsByLifestyle));

                return lifestyleNode;
            });
        }

        public IEnumerable<Node<IBudget>> FindSavedLifeTrees(Guid[]? lifestyleIds = null)
        {
            // Map lifestyles to roots.
            var lifestyles = lifestyleIds == null || !lifestyleIds.Any()
                ? _lifestyleRepo.Find()
                : _lifestyleRepo.Find(l => lifestyleIds.Contains(l.Id));
            return lifestyles.Select(lifestyle =>
            {
                var lifestyleNode = new Node<IBudget>(new BudgetMap(lifestyle));

                // Map categories to children.
                var budgetsByLifestyle = new List<IBudget>();
                var categories = _categoryRepo.FindCategorizedAs(lifestyleNode.Entity.Id);
                lifestyleNode.AddNodesAsChildren(categories.Select(category =>
                {
                    var categoryNode = new Node<IBudget>(new BudgetMap(lifestyle, category));

                    // Map budgets to leaves.
                    var budgetsByCategory = _budgetRepo.FindCategorizedAs(categoryNode.Entity.Id);
                    categoryNode.AddNodesAsChildren(budgetsByCategory.Select(e => new Node<IBudget>(new BudgetMap(e))).ToArray());
                    categoryNode.Entity.Valuate(new CategoryMap(categoryNode.Entity).GetValue(lifestyle, budgetsByCategory));
                    budgetsByLifestyle.AddRange(budgetsByCategory);

                    return categoryNode;
                }).ToArray());
                lifestyleNode.Entity.Valuate(lifestyle.GetValue(budgetsByLifestyle));

                return lifestyleNode;
            });
        }

        /// <summary>
        /// Given a flat list of { lifestyles, categories, budgets },
        ///   - deassociate them with their existing parents
        ///   - associate   them with their new      parents
        ///   - insert them if they do not exist
        ///   - update them if they do     exist
        /// </summary>
        /// <param name="lifeTrees">flat list of { lifestyles, categories, budgets } to upsert</param>
        /// <returns>flat list of upserted { lifestyles, categories, budgets }</returns>
        public IEnumerable<Node<IBudget>> UpsertSavedLifeTrees(IEnumerable<Node<IBudget>> lifeTrees)
        {
            // Map roots to lifestyles; then upsert.
            var lifestyles = lifeTrees.ToList().Select(node => new LifestyleMap(node.Entity));
            _lifestyleRepo.Upsert(lifestyles);
            lifeTrees.ToList().ForEach(root =>
            {
                // Map children to categories; then upsert & categorize as lifestyles.
                var categories = root.Children.ToList().Select(node => new CategoryMap(node.Entity));
                _categoryRepo.Upsert(categories);
                _categoryRepo.Categorize(root.Entity.Id, categories);
                root.Children.ToList().ForEach(child =>
                {
                    // Map leaves to budgets; then upsert & categorize as categories.
                    var budgets = child.Children.ToList().Select(node => new BudgetMap(node.Entity));
                    _budgetRepo.Upsert(budgets);
                    _budgetRepo.Categorize(child.Entity.Id, budgets);
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