using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryCache
{
    public class DictionaryBasedStorage<TKey, TValue> : IStorage<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _dictionary;

        private int _capacity;

        public DictionaryBasedStorage(int capacity)
        {
            _dictionary = new Dictionary<TKey, TValue>();
            _capacity = capacity;
        }

        public TValue Get(TKey key)
        {
            try
            {
                return _dictionary[key];
            }
            catch (System.Collections.Generic.KeyNotFoundException)
            {
                throw new KeyNotFoundException();
            }
        }

        public void Remove(TKey key)
        {
            if (_dictionary.ContainsKey(key))
            {
                _dictionary.Remove(key);
            }
        }

        public void Set(TKey key, TValue value)
        {
            if(_dictionary.Count >= _capacity)
            {
                throw new CacheOutOfMemoryException();
            }
            _dictionary[key] = value;
        }
    }
}
