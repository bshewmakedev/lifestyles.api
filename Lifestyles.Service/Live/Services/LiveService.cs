using Lifestyles.Domain.Budget.Entities;
using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Budget.Repositories;
using Lifestyles.Domain.Categorize.Repositories;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Domain.Live.Services;
using Lifestyles.Service.Live.Map;
using Lifestyles.Service.Budget.Models;
using Lifestyles.Service.Categorize.Models;
using Lifestyles.Service.Live.Models;
using Newtonsoft.Json;
using BudgetMap = Lifestyles.Service.Budget.Map.Budget;
using CategoryMap = Lifestyles.Service.Categorize.Map.Category;
using RecurrenceMap = Lifestyles.Service.Live.Map.Recurrence;
using ExistenceMap = Lifestyles.Service.Live.Map.Existence;
using LifestyleMap = Lifestyles.Service.Live.Map.Lifestyle;

namespace Lifestyles.Service.Live.Services
{
    public class LiveService : ILiveService
    {
        private readonly IBudgetRepo _budgetRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly ILifestyleRepo _lifestyleRepo;

        public LiveService(
            IBudgetRepo budgetRepo,
            ICategoryRepo categoryRepo,
            ILifestyleRepo lifestyleRepo)
        {
            _budgetRepo = budgetRepo;
            _categoryRepo = categoryRepo;
            _lifestyleRepo = lifestyleRepo;
        }

        private void Default()
        {
            using (StreamReader reader1 = File.OpenText(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"Live/Defaults/lifestyles.json"))))
            {
                var jsonLifestyles = JsonConvert.DeserializeObject<List<DefaultLifestyle>>(reader1.ReadToEnd()) ?? new List<DefaultLifestyle>();
                foreach (var jsonLifestyle in jsonLifestyles)
                {
                    var lifestyle = new LifestyleMap(
                        null,
                        jsonLifestyle.Label,
                        jsonLifestyle.Lifetime,
                        RecurrenceMap.Map(jsonLifestyle.Recurrence),
                        ExistenceMap.Map(jsonLifestyle.Existence));

                    _lifestyleRepo.Upsert(new[] { lifestyle });

                    using (StreamReader reader2 = File.OpenText(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"Categorize/Defaults/categories.{jsonLifestyle.Alias}.json"))))
                    {
                        var jsonCategories = (JsonConvert.DeserializeObject<List<DefaultCategory>>(reader2.ReadToEnd()) ?? new List<DefaultCategory>());
                        foreach (var jsonCategory in jsonCategories)
                        {
                            var category = new CategoryMap(
                                null,
                                jsonCategory.Label);

                            _categoryRepo.Upsert(new[] { category });
                            _categoryRepo.Categorize(lifestyle.Id, new[] { category });

                            using (StreamReader reader3 = File.OpenText(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"Budget/Defaults/budgets.{jsonLifestyle.Alias}.{jsonCategory.Alias}.json"))))
                            {
                                var jsonBudgets = (JsonConvert.DeserializeObject<List<DefaultBudget>>(reader3.ReadToEnd()) ?? new List<DefaultBudget>());
                                foreach (var jsonBudget in jsonBudgets)
                                {
                                    var budget = new BudgetMap(
                                        jsonBudget.Amount,
                                        null,
                                        jsonBudget.Label,
                                        jsonBudget.Lifetime,
                                        RecurrenceMap.Map(jsonBudget.Recurrence),
                                        ExistenceMap.Map(jsonBudget.Existence));

                                    _budgetRepo.Upsert(new[] { budget });
                                    _budgetRepo.Categorize(category.Id, new[] { budget });
                                }
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<INode<IBudget>> GetLifeTree()
        {
            var lifeTree = new List<INode<IBudget>>();
            var lifestyles = FindLifestyles();

            // If user has no lifestyles, then load defaults.
            if (!lifestyles.Any())
            {
                Default();
                lifestyles = FindLifestyles();
            }

            foreach (var lifestyle in lifestyles)
            {
                var lifestyleAsBudget = new BudgetMap(
                    GetSignedAmount(lifestyle.Id),
                    lifestyle.Id,
                    lifestyle.Label,
                    lifestyle.Lifetime,
                    lifestyle.Recurrence,
                    lifestyle.Existence
                );
                var nodeLifestyle = new Node<IBudget>(lifestyleAsBudget);
                foreach (var category in _categoryRepo.FindCategorizedAs(lifestyle.Id))
                {
                    var categoryAsBudget = new BudgetMap(
                        0, // TODO
                        category.Id,
                        category.Label,
                        lifestyle.Lifetime,
                        lifestyle.Recurrence,
                        lifestyle.Existence
                    );
                    var nodeCategory = new Node<IBudget>(categoryAsBudget);
                    foreach (var budget in _budgetRepo.FindCategorizedAs(category.Id))
                    {
                        var nodeBudget = new Node<IBudget>(budget);
                        nodeCategory.Children.Add(nodeBudget);
                    }
                    nodeLifestyle.Children.Add(nodeCategory);
                }
                lifeTree.Add(nodeLifestyle);
            }

            return lifeTree;
        }

        public IEnumerable<Direction> FindDirections()
        {
            return Enum.GetValues(typeof(Direction)).Cast<Direction>();
        }

        public IEnumerable<Lifestyles.Domain.Live.Entities.Recurrence> FindRecurrences()
        {
            return Enum.GetValues(typeof(Lifestyles.Domain.Live.Entities.Recurrence)).Cast<Lifestyles.Domain.Live.Entities.Recurrence>();
        }

        public IEnumerable<Lifestyles.Domain.Live.Entities.Existence> FindExistences()
        {
            return Enum.GetValues(typeof(Lifestyles.Domain.Live.Entities.Existence)).Cast<Lifestyles.Domain.Live.Entities.Existence>();
        }

        public IEnumerable<ILifestyle> FindLifestyles()
        {
            return _lifestyleRepo.Find();
        }

        public IEnumerable<ILifestyle> UpsertLifestyles(IEnumerable<ILifestyle> lifestyles)
        {
            return _lifestyleRepo.Upsert(lifestyles);
        }

        public IEnumerable<ILifestyle> RemoveLifestyles(IEnumerable<ILifestyle> lifestyles)
        {
            return _lifestyleRepo.Remove(lifestyles);
        }

        public decimal GetSignedAmount(Guid lifestyleId)
        {
            var lifestyle = FindLifestyles().FirstOrDefault(l => l.Id.Equals(lifestyleId));

            if (lifestyle == null)
            {
                // TODO : Log a message.
                throw new NullReferenceException();
            }

            var budgets = _budgetRepo.FindCategorizedAs(lifestyleId);

            return lifestyle.GetSignedAmount(budgets);
        }

        public IEnumerable<IComparison<ILifestyle>> CompareLifestyles(IEnumerable<ILifestyle> lifestyles)
        {
            throw new System.NotImplementedException();
        }
    }
}