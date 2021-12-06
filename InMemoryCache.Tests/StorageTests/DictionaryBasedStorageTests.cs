using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InMemoryCache.Tests
{
    public class DictionaryBasedStorageTests
    {
        private DictionaryBasedStorage<string, string> _storage;

        [Fact]
        public void Storage_Should_Set_And_Get_Key_In_Dictionary()
        {
            _storage = new DictionaryBasedStorage<string, string>(3);

            _storage.Set("a", "1");

            Assert.Equal("1", _storage.Get("a"));
        }

        [Fact]
        public void Storage_Should_Throw_Exception_When_Get_Key_Is_Not_Present()
        {
            _storage = new DictionaryBasedStorage<string, string>(3);

            Assert.Throws<KeyNotFoundException>(() => _storage.Get("a"));
        }

        [Fact]
        public void Storage_Should_Remove_Key_From_Dictionary()
        {
            _storage = new DictionaryBasedStorage<string, string>(3);

            _storage.Set("a", "1");
            _storage.Remove("a");

            Assert.Throws<KeyNotFoundException>(() => _storage.Get("a"));
        }

        [Fact]
        public void Storage_Should_Throw_Exception_When_Dictionary_Is_Full()
        {
            _storage = new DictionaryBasedStorage<string, string>(3);

            _storage.Set("a", "1");
            _storage.Set("b", "2");
            _storage.Set("c", "3");

            Assert.Throws<CacheOutOfMemoryException>(() => _storage.Set("d", "4"));
        }
    }
}
