using System;
using Xunit;
using Moq;

namespace InMemoryCache.Tests
{
    public class CacheTests
    {
        private ICache<string, string> _cache;

        private readonly Mock<IStorage<string, string>> _mockStorage;
        private readonly Mock<IEvictionPolicy<string>> _mockEvictionPolicy;
        public CacheTests()
        {
            _mockStorage = new Mock<IStorage<string, string>>();
            _mockEvictionPolicy = new Mock<IEvictionPolicy<string>>();
        }

        [Fact]
        public void Cache_Should_Get_Value_From_Storage()
        {
            _mockStorage.Setup(storage => storage.Get(It.IsAny<string>())).Returns("111");
            _mockEvictionPolicy.Setup(policy => policy.KeyUsed(It.IsAny<string>())).Verifiable();

            _cache = new Cache<string, string>(_mockStorage.Object, _mockEvictionPolicy.Object);

            Assert.Equal("111", _cache.Get(""));
        }

        [Fact]
        public void Cache_Should_Return_Default_Value_When_Storage_Throws_Exception()
        {
            _mockStorage.Setup(storage => storage.Get(It.IsAny<string>())).Throws(new KeyNotFoundException());
            _mockEvictionPolicy.Setup(policy => policy.KeyUsed(It.IsAny<string>())).Verifiable();

            _cache = new Cache<string, string>(_mockStorage.Object, _mockEvictionPolicy.Object);

            Assert.Null(_cache.Get(""));
        }

        [Fact]
        public void Cache_Should_Mark_Key_Used_When_Get_Key()
        {
            _mockStorage.Setup(storage => storage.Get(It.IsAny<string>())).Returns("111");
            _mockEvictionPolicy.Setup(policy => policy.KeyUsed(It.IsAny<string>())).Verifiable();

            _cache = new Cache<string, string>(_mockStorage.Object, _mockEvictionPolicy.Object);
            Assert.Equal("111", _cache.Get(""));

            _mockEvictionPolicy.Verify(policy => policy.KeyUsed(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Cache_Should_Set_Value_In_Storage_When_Set_Key_Value()
        {
            _mockStorage.Setup(storage => storage.Set(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            _mockEvictionPolicy.Setup(policy => policy.KeyUsed(It.IsAny<string>())).Verifiable();

            _cache = new Cache<string, string>(_mockStorage.Object, _mockEvictionPolicy.Object);

            _cache.Set("a", "1");

            _mockStorage.Verify(storage => storage.Set(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Cache_Should_Mark_Key_Used_When_Set_Key_Value()
        {
            _mockStorage.Setup(storage => storage.Set(It.IsAny<string>(), It.IsAny<string>())).Verifiable();
            _mockEvictionPolicy.Setup(policy => policy.KeyUsed(It.IsAny<string>())).Verifiable();

            _cache = new Cache<string, string>(_mockStorage.Object, _mockEvictionPolicy.Object);

            _cache.Set("a", "1");

            _mockEvictionPolicy.Verify(policy => policy.KeyUsed(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Cache_Should_Evict_Key_When_Storage_Full()
        {
            _mockStorage.Setup(storage => storage.Set(It.IsAny<string>(), It.IsAny<string>())).Throws(new CacheOutOfMemoryException());
            _mockEvictionPolicy.Setup(policy => policy.Evict()).Returns<string>(null);

            _cache = new Cache<string, string>(_mockStorage.Object, _mockEvictionPolicy.Object);

            _cache.Set("a", "1");

            _mockEvictionPolicy.Verify(policy => policy.Evict(), Times.Once);
        }

        [Fact]
        public void Cache_Should_Remove_Evicted_Key_And_Call_Set_Again_When_Storage_Full()
        {
            _mockStorage.SetupSequence(storage => storage.Set(It.IsAny<string>(), It.IsAny<string>())).Throws<CacheOutOfMemoryException>().Pass();
            _mockEvictionPolicy.Setup(policy => policy.Evict()).Returns("1");
            _mockEvictionPolicy.Setup(policy => policy.KeyUsed(It.IsAny<string>())).Verifiable();

            _mockStorage.Setup(storage => storage.Remove(It.IsAny<string>())).Verifiable();

            _cache = new Cache<string, string>(_mockStorage.Object, _mockEvictionPolicy.Object);

            _cache.Set("a", "1");

            _mockEvictionPolicy.Verify(policy => policy.Evict(), Times.Once);
            _mockStorage.Verify(storage => storage.Remove("1"), Times.Once);

            _mockStorage.Verify(storage => storage.Set("a", "1"), Times.Exactly(2));
            _mockEvictionPolicy.Verify(policy => policy.KeyUsed("a"), Times.Once);
        }


    }
}
