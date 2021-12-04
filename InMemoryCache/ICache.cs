using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryCache
{
    public interface ICache<TKey, TValue>
    {
        void Set(TKey key, TValue value);

        TValue Get(TKey key);
    }
}
