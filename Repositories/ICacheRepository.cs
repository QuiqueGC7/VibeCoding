namespace WeatherAPI.Repositories
{
    public interface ICacheRepository
    {
        T? Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan? expiration = null);
        void Remove(string key);
        void Clear();
        int GetCacheSize();
    }
}
