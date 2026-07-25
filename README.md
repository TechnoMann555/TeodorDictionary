# TDictionary

TDictionary is a small C# console project that implements a generic dictionary from scratch. It was created as an educational data-structure exercise: the code demonstrates how a hash table can store key-value pairs, resolve collisions with separate chaining, expose an indexer, and support iteration.

The solution contains one .NET Framework console application. `Program.cs` runs a manual demo of the dictionary operations.

## What the Demo Shows

`Program.cs` creates a `TeodorDictionary<string, int>` and executes the main operations:

- inserting key-value pairs
- rejecting duplicate keys
- fetching values by key
- checking whether keys exist
- updating existing values
- removing entries
- using the indexer syntax
- iterating through stored pairs with `foreach`
- triggering a rehash when a bucket grows beyond the configured threshold
- clearing the table

The demo uses `PrintList()` to print the internal bucket structure so the hash table layout is visible while operations run.

## Requirements

- Windows with .NET Framework 4.7.2 support.
- Visual Studio 2019 or newer, Visual Studio Build Tools, or another MSBuild-compatible environment that can build .NET Framework projects.
- No external NuGet packages are required.

## Core Type

The main implementation lives in `TDictionary/TeodorDictionary.cs`:

```csharp
public class TeodorDictionary<TKey, TValue> : IEnumerable
```

Internally, the dictionary stores data as an array of linked lists:

```csharp
private LinkedList<KeyValuePair<TKey, TValue>>[] table;
```

Each array slot is a hash bucket. When multiple keys hash to the same bucket, their pairs are stored in that bucket's linked list.

## Public API

### Constructors

```csharp
var dictionary = new TeodorDictionary<string, int>();
var dictionaryWithSize = new TeodorDictionary<string, int>(5);
```

- The parameterless constructor creates a table with the default initial size of `10`.
- The sized constructor creates a table with the given bucket count.

### Count

```csharp
int count = dictionary.Count;
```

Returns the number of inserted key-value pairs.

### Insert

```csharp
dictionary.Insert("one", 1);
```

Adds a new key-value pair.

- Throws `ArgumentException` if the key already exists.
- Rehashes into a larger table when a bucket grows past the maximum bucket item count.

### FetchValue

```csharp
int value = dictionary.FetchValue("one");
```

Returns the value associated with the key.

- Throws `ArgumentException` if the key does not exist.

### CheckIfExists

```csharp
bool exists = dictionary.CheckIfExists("one");
```

Returns `true` when the key exists, otherwise `false`.

### Update

```csharp
dictionary.Update("one", 100);
```

Replaces the value associated with an existing key.

- Throws `ArgumentException` if the key does not exist.

### Remove

```csharp
bool removed = dictionary.Remove("one");
```

Removes the key-value pair for the given key.

- Returns `true` when an item was removed.
- Returns `false` when the key was not found.

### Clear

```csharp
dictionary.Clear();
```

Removes all bucket lists from the table.

### Indexer

```csharp
dictionary["one"] = 1;      // inserts when the key is new
dictionary["one"] = 100;    // updates when the key already exists
int value = dictionary["one"];
```

The getter uses `FetchValue`. The setter inserts or updates depending on whether the key already exists.

### Iteration

```csharp
foreach (KeyValuePair<string, int> pair in dictionary)
{
    Console.WriteLine($"{pair.Key}: {pair.Value}");
}
```

Iteration is implemented by `TeodorDictionaryEnumerator<TKey, TValue>` in `TDictionary/TeodorDictionaryEnumerator.cs`. It walks through the bucket array and yields each linked-list node as a `KeyValuePair<TKey, TValue>`.

### PrintList

```csharp
dictionary.PrintList();
```

Prints the internal hash table layout. This method is intended for demonstration and debugging.

## Implementation Notes

- Hashing uses each key's `GetHashCode()` result and maps it into the bucket array with modulo arithmetic.
- Collision handling uses separate chaining with `LinkedList<KeyValuePair<TKey, TValue>>`.
- Rehashing doubles the table size when any bucket contains more than `10` items.
- `Insert` does not allow duplicate keys.
- The indexer setter inserts missing keys and updates existing keys.
