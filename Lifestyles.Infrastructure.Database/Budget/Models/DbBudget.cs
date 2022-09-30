namespace Lifestyles.Infrastructure.Database.Budget.Models
{
    public class DbBudget
    {
        public Guid Id { get; set; }
        public decimal? Amount { get; set; }
        public string Label { get; set; }
        public decimal? Lifetime { get; set; }
        public Guid? RecurrenceId { get; set; }
        public Guid? ExistenceId { get; set; }
    }
}