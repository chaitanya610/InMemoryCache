using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryCache
{
    public interface IStorage<TKey, TValue>
    {
        void Set(TKey key, TValue value);

        TValue Get(TKey key);

        void Remove(TKey key);
    }
}
