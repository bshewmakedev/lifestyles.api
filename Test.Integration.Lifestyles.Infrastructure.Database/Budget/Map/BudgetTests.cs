using Xunit;
using Lifestyles.Domain.Measure.Constants;
using BudgetMap = Lifestyles.Infrastructure.Database.Budget.Map.Budget;
using System.Data;

namespace Test.Integration.Lifestyles.Infrastructure.Database.Budget.Map
{
    public class BudgetTests
    {
        [Theory]
        [InlineData(10, null, "never")]
        [InlineData(10, "", "daily")]
        [InlineData(-10, null, "never")]
        [InlineData(-10, "", "weekly")]
        public void Budget_ShouldConstruct(
            decimal amount,
            string label,
            string recurrenceAlias)
        {
            var row = GetTestDataRow(amount, label, recurrenceAlias);
            var budget = new BudgetMap(row);

            Assert.NotNull(budget.Categories);
            Assert.True(budget.Amount > 0);
            Assert.True(budget.Direction.Equals(amount > 0 ? Direction.In : Direction.Out));
            Assert.True(budget.Existence.Equals(Existence.Excluded));
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
            string recurrenceAlias)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(Guid));
            table.Columns.Add("Amount", typeof(decimal));
            table.Columns.Add("Label", typeof(string));
            table.Columns.Add("RecurrenceAlias", typeof(string));

            DataRow row = table.NewRow();
            row["Id"] = Guid.NewGuid();
            row["Amount"] = amount;
            row["Label"] = label;
            row["RecurrenceAlias"] = recurrenceAlias;

            return row;
        }
    }
}