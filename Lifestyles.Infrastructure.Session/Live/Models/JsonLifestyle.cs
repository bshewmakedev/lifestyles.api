using Lifestyles.Domain.Live.Entities;
using Lifestyles.Infrastructure.Session.Categorize.Models;

namespace Lifestyles.Infrastructure.Session.Live.Models
{
    public class JsonLifestyle : JsonCategory
    {
        public int? Lifetime { get; set; }
        public string Recurrence { get; set; }
        public string Existence { get; set; }

        public JsonLifestyle() { }

        public JsonLifestyle(ILifestyle lifestyle) : base(lifestyle)
        {
            Lifetime = lifestyle.Lifetime;
            Recurrence = Lifestyles.Infrastructure.Session.Live.Models.JsonRecurrence.Map(lifestyle.Recurrence);
            Existence = Lifestyles.Infrastructure.Session.Live.Models.JsonExistence.Map(lifestyle.Existence);
        }
    }
}