using System;
using System.Collections.Generic;
using System.Text;

namespace InMemoryCache
{
    public class DoublyLinkedList<TKey>
    {
        private DoublyLinkedListNode<TKey> _head = null;
        private DoublyLinkedListNode<TKey> _tail = null;
        
        public TKey RemoveEnd()
        {
            if(_tail != null)
            {
                var key = _tail.data;
                if (_head == _tail)
                {
                    _head = null;
                    _tail = null;
                }
                else
                {
                    _tail = _tail.prev;
                    _tail.next = null;
                }
                return key;
            }
            return default;
        }

        public void AddBegining(DoublyLinkedListNode<TKey> node)
        {
            if(_head == null)
            {
                _head = node;
                _tail = node;
            }
            else
            {
                node.next = _head;
                _head.prev = node;
                _head = node;
            }
        }

        public void MoveToBegining(DoublyLinkedListNode<TKey> node)
        {
            Remove(node);
            AddBegining(node);
        }

        void Remove(DoublyLinkedListNode<TKey> node)
        {
            if(node == null) { return; }

            if(node == _tail)
            {
                RemoveEnd();
            }
            else if(node == _head)
            {
                _head = _head.next;
                _head.prev = null;
            }
            else
            {
                node.prev.next = node.next;
                node.next.prev = node.prev;
            }
        }
    }
}
