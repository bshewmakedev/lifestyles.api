using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Service.Live.Models;
using Newtonsoft.Json;

namespace Lifestyles.Service.Live.Repositories
{
    public class DefaultLifestyleRepo : IRepository<DefaultLifestyle>
    {
        public IEnumerable<DefaultLifestyle> Find(Func<DefaultLifestyle, bool>? predicate = null)
        {
            using (StreamReader reader = File.OpenText(Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), $"Live/Defaults/lifestyles.json"))))
            {
                var defaultLifestyles = (JsonConvert.DeserializeObject<List<DefaultLifestyle>>(reader.ReadToEnd()) ?? new List<DefaultLifestyle>());

                return defaultLifestyles;
            }
        }

        public IEnumerable<DefaultLifestyle> Upsert(IEnumerable<DefaultLifestyle> entities)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<DefaultLifestyle> Remove(IEnumerable<DefaultLifestyle> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}