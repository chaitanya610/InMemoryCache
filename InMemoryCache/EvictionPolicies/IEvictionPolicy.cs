using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryCache
{
    public interface IEvictionPolicy<TKey>
    {
        void KeyUsed(TKey key);
        TKey Evict();
    }
}
