using Lifestyles.Domain.Live.Constants;
using LifestyleMap = Lifestyles.Service.Live.Map.Lifestyle;
using Lifestyles.Infrastructure.Database.Live.Models;
using Lifestyles.Infrastructure.Database.Live.Extensions;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Live.Map
{
    public class Lifestyle : LifestyleMap
    {
        public Lifestyle(
            IKeyValueStorage context,
            DbLifestyle dbLifestyle) : base(
            dbLifestyle.Id,
            dbLifestyle.Label,
            dbLifestyle.Lifetime,
            GetRecurrence(context, dbLifestyle.RecurrenceId),
            GetExistence(context, dbLifestyle.ExistenceId))
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

        public static Recurrence GetRecurrence(
            IKeyValueStorage context,
            Guid? recurrenceId)
        {
            var dbRecurrences = context.GetItem<DataTable>("tbl_Recurrence").GetRows().Select(r => new DbRecurrence(r));

            return Lifestyles.Domain.Live.Map.Recurrence.Map(dbRecurrences.FirstOrDefault(r => r.Id.Equals(recurrenceId))?.Alias);
        }

        public static Existence GetExistence(
            IKeyValueStorage context,
            Guid? existenceId)
        {
            var dbExistences = context.GetItem<DataTable>("tbl_Existence").GetRows().Select(r => new DbExistence(r));

            return Lifestyles.Domain.Live.Map.Existence.Map(dbExistences.FirstOrDefault(r => r.Id.Equals(existenceId))?.Alias);
        }
    }
}