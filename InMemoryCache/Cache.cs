using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryCache
{
    public class Cache<TKey, TValue> : ICache<TKey, TValue>
    {
        private readonly IStorage<TKey, TValue> _storage;
        private readonly IEvictionPolicy<TKey> _evictionPolicy;

        public Cache(IStorage<TKey, TValue> storage, IEvictionPolicy<TKey> evictionPolicy)
        {
            _storage = storage;
            _evictionPolicy = evictionPolicy;
        }

        public TValue Get(TKey key)
        {
            try
            {
                var value = _storage.Get(key);
                _evictionPolicy.KeyUsed(key);
                return value;
            }
            catch (KeyNotFoundException)
            {
                return default(TValue);
            }
        }

        public void Set(TKey key, TValue value)
        {
            try
            {
                _storage.Set(key, value);
                _evictionPolicy.KeyUsed(key);
            }
            catch (CacheOutOfMemoryException)
            {
                var keyToEvict = _evictionPolicy.Evict();
                if(keyToEvict != null)
                {
                    _storage.Remove(keyToEvict);
                    Set(key, value);
                }         
            }
        }
    }
}
