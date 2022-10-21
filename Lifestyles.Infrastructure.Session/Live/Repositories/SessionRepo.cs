using Lifestyles.Domain.Live.Repositories;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Lifestyles.Infrastructure.Session.Live.Repositories
{
    public class SessionRepo : IKeyValueRepo
    {
        private readonly IHttpContextAccessor _httpContextAcc;

        public SessionRepo(IHttpContextAccessor httpContextAcc)
        {
            _httpContextAcc = httpContextAcc;
        }

        public T GetItem<T>(string key) where T : new()
        {
            var item = JsonConvert.DeserializeObject<T>(
                _httpContextAcc.HttpContext?.Session.GetString(key) ?? "");

            if (item == null)
            {
                item = new T();

                SetItem(key, item);
            }

            return item;
        }

        public void SetItem<T>(string key, T item) where T : new()
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