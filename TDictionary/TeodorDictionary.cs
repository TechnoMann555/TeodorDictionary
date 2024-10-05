using System;
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

		private int ItemCount
        { 
			get { return itemCount; }
			set { itemCount = value; }
		}

		private int TotalBucketCount
        { get { return table.Length; } }

		private int UsedBucketCount
        { get { return usedBuckets; } }

		
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


	}
}