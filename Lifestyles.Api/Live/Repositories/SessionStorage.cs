using Newtonsoft.Json;
using Lifestyles.Infrastructure.Database.Budget.Models;
using Lifestyles.Infrastructure.Database.Categorize.Models;
using Lifestyles.Infrastructure.Database.Live.Models;
using Lifestyles.Infrastructure.Database.Measure.Models;

namespace Lifestyles.Api.Live.Repositories
{
    public class SessionStorage : IKeyValueStorage
    {
        private readonly IHttpContextAccessor _httpContextAcc;

        public SessionStorage(IHttpContextAccessor httpContextAcc)
        {
            _httpContextAcc = httpContextAcc;

            var recurrenceIds = DbRecurrence.Default(this);
            var existenceIds = DbExistence.Default(this);
            var budgetTypeIds = DbBudgetType.Default(this);
            var lifestyleIds = DbLifestyle.Default(this, budgetTypeIds);
            var categoryIds = DbCategory.Default(this, budgetTypeIds, lifestyleIds);
            DbBudget.Default(this, budgetTypeIds, lifestyleIds, categoryIds, recurrenceIds, existenceIds);
        }

        public T GetItem<T>(string key)
        {
            var item = JsonConvert.DeserializeObject<T>(
                _httpContextAcc.HttpContext?.Session.GetString(key) ?? "");
            
            return item;
        }

        public void SetItem<T>(string key, T item)
        {
            var value = JsonConvert.SerializeObject(item); 

            _httpContextAcc.HttpContext?.Session.SetString(key, value);
        }

        public void RemoveItem(string key)
        {
            _httpContextAcc.HttpContext?.Session.Remove(key);
        }
    }
}