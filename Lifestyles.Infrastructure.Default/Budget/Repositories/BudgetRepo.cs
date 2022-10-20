using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Default.Budget.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Lifestyles.Infrastructure.Default.Budget.Repositories
{
    public class BudgetRepo
    {
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