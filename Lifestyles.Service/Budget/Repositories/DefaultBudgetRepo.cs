using Lifestyles.Service.Budget.Models;
using Lifestyles.Service.Categorize.Models;
using Lifestyles.Service.Live.Models;
using Newtonsoft.Json;

namespace Lifestyles.Service.Budget.Repositories
{
    public class DefaultBudgetRepo
    {
        public IEnumerable<DefaultBudget> FindBy(DefaultLifestyle defaultLifestyle, DefaultCategory defaultCategory)
        {
            using (StreamReader reader = File.OpenText(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"Budget/Defaults/budgets.{defaultLifestyle.Alias}.{defaultCategory.Alias}.json"))))
            {
                var defaultBudgets = (JsonConvert.DeserializeObject<List<DefaultBudget>>(reader.ReadToEnd()) ?? new List<DefaultBudget>());

                return defaultBudgets;
            }
        }
    }
}