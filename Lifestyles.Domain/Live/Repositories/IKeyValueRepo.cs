namespace Lifestyles.Domain.Live.Repositories
{
    public interface IKeyValueRepo
    {
        T GetItem<T>(string key);
        void SetItem<T>(string key, T value);
        void RemoveItem(string key);
    }
}