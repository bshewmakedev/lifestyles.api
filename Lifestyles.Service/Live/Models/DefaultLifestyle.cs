using Lifestyles.Service.Categorize.Models;

namespace Lifestyles.Service.Live.Models
{
    public class DefaultLifestyle : DefaultCategory
    {        
        public int? Lifetime { get; set; }
        public string Recurrence { get; set; } = string.Empty;
        public string Existence { get; set; } = string.Empty;

        public DefaultLifestyle() { }
    }
}