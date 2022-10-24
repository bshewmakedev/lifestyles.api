using Lifestyles.Api.Categorize.Models;
using Lifestyles.Domain.Live.Entities;

namespace Lifestyles.Api.Live.Models
{
    public class VmLifestyle : VmCategory
    {
        public int? Lifetime { get; set; }
        public string Recurrence { get; set; } = "";
        public string Existence { get; set; } = "";

        public VmLifestyle() { }

        public VmLifestyle(ILifestyle lifestyle) : base(lifestyle)
        {
            Lifetime = lifestyle.Lifetime;
            Recurrence = Lifestyles.Api.Live.Models.VmRecurrence.Map(lifestyle.Recurrence);
            Existence = Lifestyles.Api.Live.Models.VmExistence.Map(lifestyle.Existence);
        }
    }
}