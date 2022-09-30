using Newtonsoft.Json;
using Lifestyles.Infrastructure.Database.Live.Models;

namespace Lifestyles.Api.Live.Repositories
{
    public class SessionStorage : IKeyValueStorage
    {
        private readonly IHttpContextAccessor _httpContextAcc;

        public SessionStorage(IHttpContextAccessor httpContextAcc)
        {
            _httpContextAcc = httpContextAcc;

            AppalachianTrail.Create(this);
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