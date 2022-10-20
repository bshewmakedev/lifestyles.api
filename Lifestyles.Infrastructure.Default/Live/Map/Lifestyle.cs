using Lifestyles.Domain.Live.Repositories;
using LifestyleMap = Lifestyles.Service.Live.Map.Lifestyle;
using Lifestyles.Infrastructure.Default.Live.Models;

namespace Lifestyles.Infrastructure.Default.Live.Map
{
    public class Lifestyle : LifestyleMap
    {
        public Lifestyle(
            IKeyValueRepo context,
            JsonLifestyle jsonLifestyle) : base(
            null,
            jsonLifestyle.Label,
            jsonLifestyle.Lifetime,
            Lifestyles.Service.Live.Map.Recurrence.Map(jsonLifestyle.RecurrenceAlias),
            Lifestyles.Service.Live.Map.Existence.Map(jsonLifestyle.ExistenceAlias))
        { }
    }
}