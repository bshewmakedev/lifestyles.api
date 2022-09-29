using Lifestyles.Domain.Categorize.Entities;
using CategoryEntity = Lifestyles.Service.Categorize.Map.Category;
using System.Data;

namespace Lifestyles.Infrastructure.Database.Categorize.Map
{
    public class Category : CategoryEntity
    {
        public Category(DataRow row) : base(
            GetId(row["Id"]),
            GetLabel(row["Label"]))
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