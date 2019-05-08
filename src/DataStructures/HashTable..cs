
using System;
using System.Buffers;
using System.Collections.Generic;
using Utils;
using Algorithms;
namespace DataStructures {

  // TODO: Look into unit test projects, show out distribution. See also poisson distribution
  // TODO: Implement Rehash based on loadFactor
  public class HashTable<TKey,TValue> where TKey: IComparable<TKey>{
    private int bucketSize;
    private float loadFactor;
    public List<KeyValuePair<TKey, TValue>>[] arr;
    public List<TKey> keys;
    Func<uint, uint> hashFunction;
    public HashTable(int capacity = 10, float loadFactor = 1.0f) {
      bucketSize = capacity;
      this.loadFactor = loadFactor;
      arr = new List<KeyValuePair<TKey, TValue>>[bucketSize];
      keys = new List<TKey>();
      hashFunction = Hashing.UniversalHashingFamily();
    }
    protected void Rehash() {
      var currentLoadFactor = keys.Count / bucketSize;
      if (currentLoadFactor > loadFactor) {
        var nextHashTable = new HashTable<TKey, TValue>(bucketSize * 2, loadFactor);
        keys.ForEach(k => nextHashTable[k] = this[k]);
        this.arr = nextHashTable.arr;
        this.bucketSize = nextHashTable.bucketSize;
      }
    }
    public int GetHash(TKey key) {
      return (int) (hashFunction((uint)key.GetHashCode()) % bucketSize);
    }
    public bool TryGetValue(TKey key, out TValue value) {
      var hash = GetHash(key);
      var chain = arr[hash];
      if (chain?.Count > 0) {
        for (var i = 0; i < chain.Count; i++) {
          if (chain[i].Key.CompareTo(key) == 0) {
            value = chain[i].Value;
            return true;
          }
        }
      } 
      value = default(TValue);
      return false;
    }
    public bool ContainsKey(TKey key) {
      TValue value;
      return TryGetValue(key, out value);
    }
    public TValue this[TKey key] {
      get {
        TValue value;
        var hasValue = TryGetValue(key, out value);
        if (!hasValue) {
          throw new Exception("Item was not found.");
        }
        return value;
      }
      set {
        var hash = GetHash(key);
        var chain = arr[hash];
        var entry = KeyValuePair.Create(key, value);
        if (chain == null) {
          var chainToAdd = new List<KeyValuePair<TKey, TValue>>(){ entry };
          arr[hash] = chainToAdd;
          keys.Add(key);
          return;
        }
        for (var i = 0; i < chain.Count; i++) {
          if (chain[i].Key.CompareTo(key) == 0) {
            chain[i] = entry;
            return;
          }
        }
        chain.Add(entry);
        keys.Add(key);
      }
    }
  }
}