using System;

namespace InMemoryCache
{
    class Program
    {
        static void Main(string[] args)
        {
            var cache = new Cache<string, int>(new DictionaryBasedStorage<string, int>(3), new LRUEvictionPolicy<string>());

            cache.Set("a", 1);
            cache.Set("b", 2);
            cache.Set("c", 3);
            cache.Set("d", 4);

            Console.WriteLine(cache.Get("a"));

            cache.Get("b");

            cache.Set("e", 5);

            Console.WriteLine(cache.Get("b"));
            Console.WriteLine(cache.Get("c"));
            Console.ReadKey();
        }
    }
}
