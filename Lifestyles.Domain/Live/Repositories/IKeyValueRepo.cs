namespace Lifestyles.Domain.Live.Repositories
{
    public interface IKeyValueRepo
    {
        T GetItem<T>(string key) where T : new();
        void SetItem<T>(string key, T value) where T : new();
        void RemoveItem(string key);
    }
}