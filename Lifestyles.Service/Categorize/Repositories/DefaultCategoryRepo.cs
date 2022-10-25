using Lifestyles.Service.Categorize.Models;
using Lifestyles.Service.Live.Models;
using Newtonsoft.Json;

namespace Lifestyles.Service.Categorize.Repositories
{
    public class DefaultCategoryRepo
    {
        public IEnumerable<DefaultCategory> FindBy(DefaultLifestyle defaultLifestyle)
        {
            using (StreamReader reader = File.OpenText(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"Categorize/Defaults/categories.{defaultLifestyle.Alias}.json"))))
            {
                var defaultCategories = (JsonConvert.DeserializeObject<List<DefaultCategory>>(reader.ReadToEnd()) ?? new List<DefaultCategory>());

                return defaultCategories;
            }
        }
    }
}