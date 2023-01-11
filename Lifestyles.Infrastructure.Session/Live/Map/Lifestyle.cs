using Lifestyles.Infrastructure.Session.Live.Models;

namespace Lifestyles.Infrastructure.Session.Live.Map
{
    public class Lifestyle : Lifestyles.Service.Live.Map.Lifestyle
    {
        public Lifestyle() { }

        public Lifestyle(JsonLifestyle jsonLifestyle) : base(
            jsonLifestyle.Id,
            jsonLifestyle.Alias,
            jsonLifestyle.Label,
            jsonLifestyle.Lifetime,
            Lifestyles.Infrastructure.Session.Live.Map.Recurrence.Map(jsonLifestyle.Recurrence),
            Lifestyles.Infrastructure.Session.Live.Map.Existence.Map(jsonLifestyle.Existence))
        { }
    }
}