using System;
using System.Collections;
using System.Collections.Generic;

namespace TDictionary
{
    public class TeodorDictionaryEnumerator<TKey, TValue> : IEnumerator
    {
        // The dictionary (hash-table) itself
        private LinkedList<KeyValuePair<TKey, TValue>>[] _dictionary;

        // The current bucket
        private LinkedList<KeyValuePair<TKey, TValue>> curBucket;

        // The current key-value pair of the current bucket
        private LinkedListNode<KeyValuePair<TKey, TValue>> curPair;

        // The current index of the dictionary (linked list array)
        private int curIndex;

        public TeodorDictionaryEnumerator(LinkedList<KeyValuePair<TKey, TValue>>[] dict)
        {
            this._dictionary = dict;
            curIndex = -1;
            curBucket = null;
        }

        /* 
            This method works by first iterating through the hash-table array
            until it finds a bucket that contains key-value pairs (a linked list).

            Then, when a bucket is found, the method iterates through its linked list
            until it reaches the end of the list, at which point it looks for the next
            bucket with key-value pairs.

            The two steps are repeated until the end of the dictionary collection is reached.

            Essentialy, it iterates through all the key-value pairs in the dictionary
            as if all of them were connected in a single linked list.
        */
        public bool MoveNext()
        {
            /*
                If there is no current bucket, find a bucket in the hash-table
                to set as the current bucket and set the current pair to the
                first pair of the bucket, or reach the end of the collection.
            */
            if(curBucket == null)
            {
                // Iterates through the dictionary until it finds a non-empty bucket
                // or reaches the end of the collection.
                do
                {
                    if(++curIndex >= _dictionary.Length)
                    {
                        return false;
                    }
                } while(_dictionary[curIndex] == null);
                
                curBucket = _dictionary[curIndex];
                curPair = curBucket.First;
            }
            /*
                If there is no next key-value pair in the linked list
                to go to, find the next bucket in the hash-table to set as the
                current bucket and set the current pair to the first pair of
                the bucket, or reach the end of the collection.
            */
            else if(curPair.Next == null)
            {
                curBucket = null;
                return this.MoveNext();
            }
            // If the end of the current bucket's linked list hasn't been
            // reached, go to the next key-value pair node
            else
            {
                curPair = curPair.Next;
            }

            return true;
        }

        public void Reset()
        {
            curIndex = -1;
        }

        public KeyValuePair<TKey, TValue> Current
        {
            get
            {
                try
                {
                    // Since curPair is of type LinkedListNode<T>, we must return
                    // its .Value property, which is of type KeyValuePair<TKey, TValue>
                    return curPair.Value;
                }
                catch(IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
    }
}
