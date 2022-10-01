using Lifestyles.Api.Categorize.Models;

namespace Lifestyles.Api.Live.Models
{
    public class VmLifestyle : VmCategory
    {
        public decimal Lifetime { get; set; }
        public string Recurrence { get; set; }
        public string Existence { get; set; }
    }
}