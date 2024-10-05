using System;
using System.Collections.Generic;

namespace TDictionary
{
	public class TeodorDictionary<TKey, TValue>
	{
		// The Seperate Chaining collision resolution policy requires us
		// to organize the data as an array of linked lists of key-value pairs
		private LinkedList<KeyValuePair<TKey, TValue>>[] table;

		private const double maxLoadFactor = 0.7;
		private const int defaultInitialSize = 10;

		public TeodorDictionary()
		{
			table = new LinkedList<KeyValuePair<TKey, TValue>>[defaultInitialSize];
		}

		public TeodorDictionary(int initialSize)
        {
			table = new LinkedList<KeyValuePair<TKey, TValue>>[initialSize];
		}
	}
}