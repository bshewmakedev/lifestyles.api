using Xunit;
using Lifestyles.Domain.Measure.Constants;
using BudgetMap = Lifestyles.Infrastructure.Database.Budget.Map.Budget;
using System.Data;

namespace Test.Integration.Lifestyles.Infrastructure.Database.Budget.Map
{
    public class BudgetTests
    {
        [Theory]
        [InlineData(10, null, "never", "excluded")]
        [InlineData(10, "", "daily", "expected")]
        [InlineData(-10, null, "never", "suggested")]
        [InlineData(-10, "", "weekly", "excluded")]
        public void Budget_ShouldConstruct(
            decimal amount,
            string label,
            string recurrenceAlias,
            string existenceAlias)
        {
            var row = GetTestDataRow(amount, label, recurrenceAlias, existenceAlias);
            var budget = new BudgetMap(row);

            Assert.NotNull(budget.Categories);
            Assert.True(budget.Amount > 0);
            Assert.True(budget.Direction.Equals(amount > 0 ? Direction.In : Direction.Out));
            Assert.False(string.IsNullOrWhiteSpace(budget.Id.ToString()));
            Assert.True(string.IsNullOrWhiteSpace(budget.Label));

            if (recurrenceAlias.Equals("never"))
            {
                Assert.Null(budget.Lifetime);
            }
            else
            {
                Assert.NotNull(budget.Lifetime);
            }

            Assert.True(budget.Recurrence.Equals(BudgetMap.GetRecurrence((object)recurrenceAlias)));
        }

        private static DataRow GetTestDataRow(
            decimal amount,
            string label,
            string recurrenceAlias,
            string existenceAlias)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Columns.Add("Amount", typeof(decimal));
            table.Columns.Add("Label", typeof(string));
            table.Columns.Add("RecurrenceAlias", typeof(string));
            table.Columns.Add("ExistenceAlias", typeof(string));

            DataRow row = table.NewRow();
            row["Id"] = Guid.NewGuid();
            row["Amount"] = amount;
            row["Label"] = label;
            row["RecurrenceAlias"] = recurrenceAlias;
            row["ExistenceAlias"] = existenceAlias;

            return row;
        }
    }
}