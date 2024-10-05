using System;
using System.Text;
using System.Collections.Generic;

namespace TDictionary
{
	public class TeodorDictionary<TKey, TValue>
	{
		private int itemCount = 0;
		private int usedBuckets = 0;

		private const double maxLoadFactor = 0.7;
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

		private double LoadFactor
        { get { return (double)usedBuckets / TotalBucketCount; } }

		
		// Gets the hash code of a given object
		private int HashFunction(object val)
        {
			// The System.Object.GetHashCode() method allows us
			// to get the hash code of an object of any type.
			int hashingResult = val.GetHashCode();

			// In order to properly calculate the array index from the hash code
			// using the modulo operator, the resulting hash value must be a positive integer.
			if(hashingResult < 0)
				return hashingResult * (-1);
			else
				return hashingResult;
        }

		// Calculates the table array index from a given key value
		private int HashKey(TKey key)
        {
			return this.HashFunction(key) % TotalBucketCount;
        }

		// Inserts a new key-value pair into the hash table
		public void Insert(TKey key, TValue value)
        {
			int arrayIndex = HashKey(key);
			KeyValuePair<TKey, TValue> newPair = new KeyValuePair<TKey, TValue>(key, value);

			// If the bucket is empty, create a new linked list
			if(table[arrayIndex] == null)
				table[arrayIndex] = new LinkedList<KeyValuePair<TKey, TValue>>();

			LinkedList<KeyValuePair<TKey, TValue>> bucketList = table[arrayIndex];

			// Bucket list is empty - add the first node
			if(bucketList.Count == 0)
			{
				bucketList.AddFirst(newPair);
				usedBuckets++;
			}

			else
			{
				// Check if a key-value pair with the passed key value already exists in the bucket
				foreach(KeyValuePair<TKey, TValue> pair in bucketList)
				{
					if(pair.Key.Equals(newPair.Key))
						throw new ArgumentException("An item with the given key already exists!");
				}
				// Add the new pair to the end of the list
				bucketList.AddLast(newPair);
			}

			itemCount++;
        }

		public TValue GetValue(TKey key)
        {
			int arrayIndex = HashKey(key);
			LinkedList<KeyValuePair<TKey, TValue>> bucketList = table[arrayIndex];

			// The bucket is empty
			if(bucketList == null)
				throw new ArgumentException("An item with the given key does not exist!");

			// Only one item is in the bucket - therefore, no collision had occured 
			if(bucketList.Count == 1)
            {
				return bucketList.First.Value.Value;
            }
			
			// Multiple items are in the same bucket - therefore,
			// collisions had occured, so search for the matching key-value pair
			else
            {
				foreach(KeyValuePair<TKey, TValue> pair in bucketList)
                {
					if(pair.Key.Equals(key))
						return pair.Value;
				}

				// No matching pair has been found
				throw new ArgumentException("An item with the given key does not exist!");
            }
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