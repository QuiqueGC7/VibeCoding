using System.Collections.Concurrent;

namespace WeatherAPI.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private readonly ConcurrentDictionary<string, CacheItem> _cache = new();
        private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(10);

        public T? Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out var item))
            {
                if (DateTime.UtcNow - item.Timestamp < item.Expiration)
                {
                    return (T?)item.Value;
                }
                
                _cache.TryRemove(key, out _);
            }
            
            return default;
        }

        public void Set<T>(string key, T value, TimeSpan? expiration = null)
        {
            var cacheItem = new CacheItem
            {
                Value = value,
                Timestamp = DateTime.UtcNow,
                Expiration = expiration ?? _defaultExpiration
            };
            
            _cache[key] = cacheItem;
        }

        public void Remove(string key)
        {
            _cache.TryRemove(key, out _);
        }

        public void Clear()
        {
            _cache.Clear();
        }

        public int GetCacheSize()
        {
            return _cache.Count;
        }

        private class CacheItem
        {
            public object? Value { get; set; }
            public DateTime Timestamp { get; set; }
            public TimeSpan Expiration { get; set; }
        }
    }
}
