using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryCache
{
    public class DoublyLinkedListNode<TKey>
    {
        public TKey data;
        public DoublyLinkedListNode<TKey> prev;
        public DoublyLinkedListNode<TKey> next;

        public DoublyLinkedListNode(TKey data)
        {
            this.data = data;
            this.prev = null;
            this.next = null;
        }
    }
}
