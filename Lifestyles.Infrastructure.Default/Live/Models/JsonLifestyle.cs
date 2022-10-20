using Lifestyles.Domain.Live.Entities;
using Lifestyles.Infrastructure.Default.Categorize.Models;

namespace Lifestyles.Infrastructure.Default.Live.Models
{
    public class JsonLifestyle : JsonCategory
    {
        public int? Lifetime { get; set; }
        public string RecurrenceAlias { get; set; } = string.Empty;
        public string ExistenceAlias { get; set; } = string.Empty;
    }
}