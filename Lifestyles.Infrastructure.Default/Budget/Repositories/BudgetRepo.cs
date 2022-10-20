using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Default.Budget.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Lifestyles.Infrastructure.Default.Budget.Repositories
{
    public class BudgetRepo
    {
        // private readonly ILifestyleRepo _lifestyleRepo;

        public BudgetRepo()
        {
            // _lifestyleRepo = lifestyleRepo;
        }

        // public IEnumerable<JsonBudget> Find()
        // {
        //     var budgets = new List<JsonBudget>();
        //     foreach (var lifestyle in _lifestyleRepo.Find())
        //     {
        //         using (StreamReader reader = File.OpenText($"../Lifestyles.Infrastructure.Default/Budget/Data/Budgets.{new Regex("[^a-zA-Z]").Replace(lifestyle.Label, "")}.json"))
        //         {
        //             budgets = budgets
        //                 .Concat(JsonConvert
        //                 .DeserializeObject<List<JsonBudget>>(reader.ReadToEnd()) ?? new List<JsonBudget>())
        //                 .ToList();
        //         }
        //     }

        //     return budgets;
        // }

        public IEnumerable<JsonBudget> FindBy(params ICategory[] categories)
        {
            var pathCategories = string.Join(".", categories.Select(c => new Regex("[^a-zA-Z]").Replace(c.Label, "")));
            var path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"Budget/Data/Budgets.{pathCategories}.json"));
            using (StreamReader reader = File.OpenText(path))
            {
                return JsonConvert.DeserializeObject<List<JsonBudget>>(reader.ReadToEnd()) ?? new List<JsonBudget>();
            }
        }
    }
}