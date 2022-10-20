using System.Data;

namespace Lifestyles.Infrastructure.Session.Live.Extensions
{
    public static class DataTableExtensions
    {
        public static IEnumerable<DataRow> GetRows(this DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                yield return row;
            }
        }
    }
}