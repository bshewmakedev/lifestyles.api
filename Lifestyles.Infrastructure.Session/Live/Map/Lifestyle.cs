using Lifestyles.Domain.Live.Entities;
using Lifestyles.Domain.Live.Repositories;
using Lifestyles.Infrastructure.Session.Live.Models;
using Lifestyles.Infrastructure.Session.Live.Extensions;
using System.Data;
using LifestyleMap = Lifestyles.Service.Live.Map.Lifestyle;

namespace Lifestyles.Infrastructure.Session.Live.Map
{
    public class Lifestyle : LifestyleMap
    {
        public Lifestyle(
            IKeyValueRepo context,
            DbLifestyle dbLifestyle) : base(
            dbLifestyle.Id,
            dbLifestyle.Label,
            dbLifestyle.Lifetime,
            Lifestyles.Infrastructure.Session.Live.Models.DbRecurrence.GetRecurrence(context, dbLifestyle.RecurrenceId),
            Lifestyles.Infrastructure.Session.Live.Models.DbExistence.GetExistence(context, dbLifestyle.ExistenceId))
        { }

        private static Guid GetId(object id)
        {
            var idStr = id.ToString() ?? "";

            return string.IsNullOrWhiteSpace(idStr)
                ? Guid.NewGuid()
                : Guid.Parse(idStr);
        }

        private static string GetLabel(object label)
        {
            return label.ToString() ?? "";
        }
    }
}