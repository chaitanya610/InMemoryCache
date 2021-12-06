using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InMemoryCache.Tests
{
    public class LRUEvictionPolicyTests
    {
        private LRUEvictionPolicy<string> _evictionPolicy = new LRUEvictionPolicy<string>();

        [Fact]
        public void Policy_Should_Evict_Least_Recently_Used_Key()
        {
            _evictionPolicy.KeyUsed("a");
            _evictionPolicy.KeyUsed("b");
            _evictionPolicy.KeyUsed("c");
            _evictionPolicy.KeyUsed("a");

            Assert.Equal("b", _evictionPolicy.Evict());
        }

        [Fact]
        public void Policy_Should_Evict_Keys_In_Used_Order()
        {
            _evictionPolicy.KeyUsed("a");
            _evictionPolicy.KeyUsed("b");
            _evictionPolicy.KeyUsed("c");

            Assert.Equal("a", _evictionPolicy.Evict());
            Assert.Equal("b", _evictionPolicy.Evict());
            Assert.Equal("c", _evictionPolicy.Evict());
        }

        [Fact]
        public void Policy_Should_Return_Null_When_No_Key_To_Evict()
        {
            Assert.Null(_evictionPolicy.Evict());
        }
    }
}
