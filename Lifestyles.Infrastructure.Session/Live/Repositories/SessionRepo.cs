using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Lifestyles.Domain.Live.Repositories;

namespace Lifestyles.Infrastructure.Session.Live.Repositories
{
    public class SessionRepo : IKeyValueRepo
    {
        private readonly IHttpContextAccessor _httpContextAcc;

        public SessionRepo(IHttpContextAccessor httpContextAcc)
        {
            _httpContextAcc = httpContextAcc;
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