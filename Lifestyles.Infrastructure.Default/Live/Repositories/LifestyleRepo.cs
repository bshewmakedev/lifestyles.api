using Lifestyles.Infrastructure.Default.Live.Models;
using Newtonsoft.Json;

namespace Lifestyles.Infrastructure.Default.Live.Repositories
{
    public class LifestyleRepo
    {
        public IEnumerable<JsonLifestyle> Find()
        {
            var path = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Live/Data/Lifestyles.json"));
            var lifestyles = new List<JsonLifestyle>();
            using (StreamReader reader = File.OpenText(path))
            {
                lifestyles = lifestyles
                    .Concat(JsonConvert
                    .DeserializeObject<List<JsonLifestyle>>(reader.ReadToEnd()) ?? new List<JsonLifestyle>())
                    .ToList();
            }

            return lifestyles;
        }
    }
}