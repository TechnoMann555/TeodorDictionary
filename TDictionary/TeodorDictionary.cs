using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace TDictionary
{
	public class TeodorDictionary<TKey, TValue> : IEnumerable
	{
		private int itemCount = 0;
		private int usedBuckets = 0;

		private const int maxBucketItemCount = 7;
		private const int defaultInitialSize = 10;

		// The Seperate Chaining collision resolution policy requires us
		// to organize the data as an array of linked lists of key-value pairs
		private LinkedList<KeyValuePair<TKey, TValue>>[] table;

		public TeodorDictionary()
		{
			table = new LinkedList<KeyValuePair<TKey, TValue>>[defaultInitialSize];
		}
		public TeodorDictionary(int initialSize)
		{
			table = new LinkedList<KeyValuePair<TKey, TValue>>[initialSize];
		}

		public int Count
		{ get { return itemCount; } }
		private int TotalBucketCount
		{ get { return table.Length; } }


		// Gets the hash code of a given object
		private int HashFunction(object val)
		{
			// The System.Object.GetHashCode() method allows us
			// to get the hash code of an object of any type.
			int hashingResult = val.GetHashCode();

			// In order to properly calculate the array index from the hash code
			// using the modulo operator, the resulting hash value must be a positive integer.
			return Math.Abs(hashingResult);
		}

		// Gets a hash table's index from a passed hash code and bucket count
		private int GetTableIndex(object hashCode, int bucketCount)
        {
			return this.HashFunction(hashCode) % bucketCount;
        }

		// Calculates the dictionary array index from the passed key value
		private int HashKey(TKey key)
		{
			return this.GetTableIndex(key, this.TotalBucketCount);
		}

		// Rehashes the hash-table when any bucket's item count passes the defined threshold
		private void Rehash()
		{
			LinkedList<KeyValuePair<TKey, TValue>>[] newTable = new LinkedList<KeyValuePair<TKey, TValue>>[table.Length*2];
			int newUsedBucketCount = 0;

			// Iterate through the original hash-table's key-value pairs
			foreach(KeyValuePair<TKey, TValue> pair in this)
            {
				int bucketItemCount;
				bool listCreated;
				
				/*
					No need to handle the returned insert status code,
					since we're just copying the key-value pairs from
					one hash table to another
				*/
				this.InsertIntoHashTable(
					pair.Key,
					pair.Value,
					newTable,
					out listCreated,
					out bucketItemCount
				);

				if(listCreated)
                {
					newUsedBucketCount++;
                }
            }

			this.table = newTable;
			this.usedBuckets = newUsedBucketCount;
        }

		// INSERTION

		private enum InsertStatusCode
		{
			OK, // Key-value pair successfully inserted
			DuplicateKey // Key already exists in the hash table
		}

		// Inserts a key-value pair into the passed hash-table
		private InsertStatusCode InsertIntoHashTable(
			TKey key,
			TValue value,
			LinkedList<KeyValuePair<TKey, TValue>>[] insertionTable,
			out bool listCreated, // Indicates whether a new linked list was created in a bucket
			out int bucketListItemCount
		)
		{
			int arrayIndex = this.GetTableIndex(key, insertionTable.Length);
			KeyValuePair<TKey, TValue> newPair = new KeyValuePair<TKey, TValue>(key, value);
			
			listCreated = false;
			bucketListItemCount = 0;

			// If the bucket is empty, create a new linked list
			if(insertionTable[arrayIndex] == null)
			{
				insertionTable[arrayIndex] = new LinkedList<KeyValuePair<TKey, TValue>>();
				listCreated = true;
			}

			LinkedList<KeyValuePair<TKey, TValue>> bucketList = insertionTable[arrayIndex];

			// Bucket list is empty - add the first node
			if(bucketList.Count == 0)
			{
				bucketList.AddFirst(newPair);
			}
			else
			{
				// Check if a key-value pair with the passed key value already exists in the bucket
				foreach(KeyValuePair<TKey, TValue> pair in bucketList)
				{
					if(pair.Key.Equals(newPair.Key))
					{
						return InsertStatusCode.DuplicateKey;
					}
				}
				// Add the new pair to the end of the list
				bucketList.AddLast(newPair);
			}

			bucketListItemCount = bucketList.Count;
			return InsertStatusCode.OK;
		}

		// Inserts a new key-value pair into the dictionary
		public void Insert(TKey key, TValue value)
		{
			int bucketListItemCount;
			bool listCreated;
			InsertStatusCode returnStatus = this.InsertIntoHashTable(
												key,
												value,
												this.table,
												out listCreated,
												out bucketListItemCount
											);

			switch(returnStatus)
			{
				case InsertStatusCode.OK:
				itemCount++;
				break;
				case InsertStatusCode.DuplicateKey:
				throw new ArgumentException("An item with the given key already exists!");
				break;
			}

			if(listCreated)
			{
				usedBuckets++;
			}

			if(bucketListItemCount > maxBucketItemCount)
            {
				this.Rehash();
            }
		}

		// FETCHING
		// Returns a key-value pair node from the hash-table linked list whose key matches the passed key
		private LinkedListNode<KeyValuePair<TKey, TValue>> GetNodeFromKey(TKey key)
        {
			int arrayIndex = this.HashKey(key);
			LinkedList<KeyValuePair<TKey, TValue>> bucketList = table[arrayIndex];

			// The bucket is empty - the node doesn't exist
			if(bucketList == null)
			{
				return null;
			}

			LinkedListNode<KeyValuePair<TKey, TValue>> node = bucketList.First;
			for(int i = 0; i < bucketList.Count; i++, node = node.Next)
            {
				// A node with the matching key value was found
				if(node.Value.Key.Equals(key))
                {
					return node;
				}
            }

			return null;
		}

		// Gets the value from the key-value pair that holds the passed key value
		public TValue FetchValue(TKey key)
		{
			LinkedListNode<KeyValuePair<TKey, TValue>> node = this.GetNodeFromKey(key);

			// No matching key-value pair was found
			if(node == null)
            {
				throw new ArgumentException("An item with the given key does not exist!");
			}

			// A matching key-value pair was found
			return node.Value.Value;
		}

		// Checks if a key-value pair exists with the passed key value
		public bool CheckIfExists(TKey key)
		{
			// If the result is null - no pair was found, otherwise, a pair exists
			return !(GetNodeFromKey(key) == null);
		}

		// UPDATING
		// Updates the key-value pair's value that has the passed key value
		public void Update(TKey key, TValue value)
		{
			int arrayIndex = this.HashKey(key);
			LinkedList<KeyValuePair<TKey, TValue>> bucketList = table[arrayIndex];

			// The bucket is empty
			if(bucketList == null)
			{
				throw new ArgumentException("An item with the given key does not exist!");
			}

			KeyValuePair<TKey, TValue> updatedPair = new KeyValuePair<TKey, TValue>(key, value);

			// The bucket contains one pair - check if the key matches
			if(bucketList.Count == 1)
			{
				// LinkedList<T>.Remove(T) is an O(n) operation, since it performs a linear search first,
				// while LinkedList<T>.Remove<LinkedListNode<T>> is an O(1) operation,
				// which is why this method works with LinkedListNode-s instead of KeyValuePair-s
				LinkedListNode<KeyValuePair<TKey, TValue>> onlyPair = bucketList.First;

				// The pair's key doesn't match the passed key value
				if(!onlyPair.Value.Key.Equals(key))
				{
					throw new ArgumentException("An item with the given key does not exist!");
				}

				// The key matches - insert a new pair and remove the original
				bucketList.AddLast(updatedPair);
				bucketList.Remove(onlyPair);
			}

			// The bucket contains multiple pairs - check if there's one that matches
			else
			{
				LinkedListNode<KeyValuePair<TKey, TValue>> node = bucketList.First;

				// The for-each loop iterates through a linked list's node values (key-value pairs),
				// which is why this iteration uses the for loop to iterate through nodes themselves
				for(
					int i = 0;
					i < bucketList.Count;
					i++, node = node.Next
				)
				{
					// The key matches - insert a new pair and remove the original
					if(node.Value.Key.Equals(key))
					{
						bucketList.AddAfter(node, updatedPair);
						bucketList.Remove(node);
						return;
					}
				}

				// No matching pair has been found
				throw new ArgumentException("An item with the given key does not exist!");
			}
		}

		// DELETION
		// Deletes the key-value pair that has the passed key value
		public bool Remove(TKey key)
		{
			int arrayIndex = this.HashKey(key);
			LinkedList<KeyValuePair<TKey, TValue>> bucketList = table[arrayIndex];

			// The bucket is empty - there's no pair to delete
			if(bucketList == null)
			{
				return false;
			}

			// The bucket contains one pair - check if the key matches
			else if(bucketList.Count == 1)
			{
				if(bucketList.First.Value.Key.Equals(key))
				{
					// There's no need for a bucket to store an empty linked list
					table[arrayIndex] = null;
					itemCount--;
					usedBuckets--;

					return true;
				}
			}

			// The bucket contains multiple pairs - check if there's one that matches
			else
			{
				LinkedListNode<KeyValuePair<TKey, TValue>> node = bucketList.First;

				// Iterate through linked list nodes
				for(
					int i = 0;
					i < bucketList.Count;
					i++, node = node.Next
				)
				{
					// The key matches - remove the pair
					if(node.Value.Key.Equals(key))
					{
						bucketList.Remove(node);
						itemCount--;

						return true;
					}
				}
			}

			// No matching pair has been found
			return false;
		}

		// Clears the entire dictionary of all linked lists and key-value pairs
		public void Clear()
        {
			for(int i = 0; i < this.table.Length; i++)
			{
				this.table[i] = null;
			}
		}

		public TValue this[TKey key]
		{
			get
			{
				return this.FetchValue(key);
			}

			// Whether we insert or update a key-value pair depends
			// on if the key-value pair exists in the table or not
			set
			{
				// If the key-value pair exists - update it
				if(this.CheckIfExists(key))
				{
					this.Update(key, value);
				}

				// If it doesn't exist - insert it
				else
				{
					this.Insert(key, value);
				}
			}
		}

		// IEnumerable implementation
		public TeodorDictionaryEnumerator<TKey, TValue> GetEnumerator()
		{
			return new TeodorDictionaryEnumerator<TKey, TValue>(table);
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return (IEnumerator)GetEnumerator();
		}

		// METHOD MADE FOR TESTING - prints the entire hash table structure
		public void PrintList()
		{
			StringBuilder output = new StringBuilder();
			int index = 0;

			foreach(LinkedList<KeyValuePair<TKey, TValue>> bucket in table)
			{
				output.Append($"[{index}]-> ");

				if(bucket != null)
				{
					foreach(KeyValuePair<TKey, TValue> pair in bucket)
					{
						output.Append($"<{pair.Key.ToString()}, {pair.Value.ToString()}>-");
					}
				}
				else
				{
					output.Append("Empty");
				}

				output.AppendLine();
				index++;
			}

			Console.WriteLine(output.ToString());
		}
	}
}