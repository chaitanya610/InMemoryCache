using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryCache
{
    public class LRUEvictionPolicy<TKey> : IEvictionPolicy<TKey>
    {
        private DoublyLinkedList<TKey> _list;
        private Dictionary<TKey, DoublyLinkedListNode<TKey>> _dictionary;

        public LRUEvictionPolicy()
        {
            _list = new DoublyLinkedList<TKey>();
            _dictionary = new Dictionary<TKey, DoublyLinkedListNode<TKey>>();
        }
        public TKey Evict()
        {
            var key = _list.RemoveEnd();
            _dictionary.Remove(key);
            return key;
        }

        public void KeyUsed(TKey key)
        {
            if (!_dictionary.ContainsKey(key))
            {
                var node = new DoublyLinkedListNode<TKey>(key);
                _list.AddBegining(node);
                _dictionary.Add(key, node);
            }
            else
            {
                _list.MoveToBegining(_dictionary[key]);
            }
        }
    }
}
