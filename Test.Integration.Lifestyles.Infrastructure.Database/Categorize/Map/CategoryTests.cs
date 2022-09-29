using Xunit;
using CategoryMap = Lifestyles.Infrastructure.Database.Categorize.Map.Category;
using System.Data;

namespace Test.Integration.Lifestyles.Infrastructure.Categorize.Map
{
    public class CategoryTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Category_ShouldConstruct(string label)
        {
            var row = GetTestDataRow(label);
            var category = new CategoryMap(row);
            
            Assert.NotNull(category.Budgets);
            Assert.False(string.IsNullOrWhiteSpace(category.Id.ToString()));
        }

        private static DataRow GetTestDataRow(string label)
        {
            DataTable table = new DataTable();            
            table.Columns.Add("Id", typeof(Guid));
            table.Columns.Add("Label", typeof(string));

            DataRow row = table.NewRow();
            row["Id"] = Guid.NewGuid();
            row["Label"] = label;

            return row;
        }
    }
}