using Lifestyles.Domain.Categorize.Entities;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Default.Categorize.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Lifestyles.Infrastructure.Default.Categorize.Repositories
{
    public class CategoryRepo
    {
        // private readonly ILifestyleRepo _lifestyleRepo;

        public CategoryRepo()
        {
            // _lifestyleRepo = lifestyleRepo;
        }

        // public IEnumerable<JsonCategory> Find()
        // {
        //     var categories = new List<JsonCategory>();
        //     foreach (var lifestyle in _lifestyleRepo.Find())
        //     {
        //         var path = $"../Lifestyles.Infrastructure.Default/Categorize/Data/Categories.{new Regex("[^a-zA-Z]").Replace(lifestyle.Label, "")}.json";
        //         using (StreamReader reader = File.OpenText(path))
        //         {
        //             categories = categories
        //                 .Concat(JsonConvert
        //                 .DeserializeObject<List<JsonCategory>>(reader.ReadToEnd()) ?? new List<JsonCategory>())
        //                 .ToList();
        //         }
        //     }

        //     return categories;
        // }

        public IEnumerable<JsonCategory> FindBy(ICategory category)
        {
            var path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"Categorize/Data/Categories.{new Regex("[^a-zA-Z]").Replace(category.Label, "")}.json"));
            using (StreamReader reader = File.OpenText(path))
            {
                return JsonConvert.DeserializeObject<List<JsonCategory>>(reader.ReadToEnd()) ?? new List<JsonCategory>();
            }
        }
    }
}