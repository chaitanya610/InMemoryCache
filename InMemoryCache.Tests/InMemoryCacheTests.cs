using System;
using Xunit;
using Moq;

namespace InMemoryCache.Tests
{
    public class InMemoryCacheTests
    {
        private readonly ICache<string, string> _cache;

        private readonly Mock<IStorage<string, string>> _mockStorage;
        private readonly Mock<IEvictionPolicy<string>> _mockEvictionPolicy;
        public InMemoryCacheTests()
        {
            _mockStorage = new Mock<IStorage<string, string>>();
            _mockEvictionPolicy = new Mock<IEvictionPolicy<string>>();
        }
        [Fact]
        public void Test1()
        {

        }
    }
}
