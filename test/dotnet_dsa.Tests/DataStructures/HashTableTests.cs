using System;
using Xunit;
using DotNetDsa.DataStructures;
using System.Collections.Generic;

namespace DotNetDsa.Tests.DataStructures
{
  public class StubHashTable<TKey, TValue>: HashTable<TKey,TValue> where TKey: IComparable<TKey> {
    public List<KeyValuePair<TKey, TValue>>[] Store {
      get {
        return arr;
      }
    }
    public StubHashTable(int capacity, float loadFactor): base(capacity, loadFactor){
    }
  }
  public class HashTableTests
  {
    StubHashTable<Guid, int> hashTable;
    float loadFactor = 1.0f;
    int capacity = 10;
    public HashTableTests() {
      hashTable = new StubHashTable<Guid, int>(capacity, loadFactor);
    }

    [Fact]
    public void AfterConstr_ShouldHaveNoKeys()
    {
      Assert.True(hashTable.Keys.Count == 0);
    }
    public void AfterConstr_ShouldSetBucketSizeToCapacity()
    {
      Assert.True(hashTable.Store.Length == 10);
    }
    public void GettingValueWithExistingKeyEntry_ShouldReturnValue() {
      var guidKey = Guid.NewGuid();
      hashTable[guidKey] = 100;
      Assert.Equal(100, hashTable[guidKey]);
    }
    [Fact]
    public void GettingValueWithNonExistentKeyEntry_ShouldThrowException()
    {
      Exception expected = null;
      try {
        var value = hashTable[Guid.NewGuid()];
      } catch(Exception e){
        expected = e;
      }
      Assert.True(expected != null);
    }
    [Fact]
    public void SettingNewKeyEntry_ShouldAddKeyEntry() {
      var guidKey = Guid.NewGuid();
      hashTable[guidKey] = 100;
      Assert.True(hashTable.Keys.Contains(guidKey));
    }
    [Fact]
    public void CheckingExistingKeyEntry_ShouldReturnTrue() {
      var guidKey = Guid.NewGuid();
      hashTable[guidKey] = 100;
      Assert.True(hashTable.ContainsKey(guidKey));
    }
    [Fact]
    public void CheckingNonExistentKeyEntry_ShouldReturnFalse() {
      var guidKey = Guid.NewGuid();
      var unknownGuid = Guid.NewGuid();
      hashTable[guidKey] = 100;
      Assert.False(hashTable.ContainsKey(unknownGuid));
    }
    [Fact]
    public void WhenAttemptingToGetValueFromExistingKeyEntry_ShouldReturnTrue() {
      var guidKey = Guid.NewGuid();
      hashTable[guidKey] = 100;
      int value;
      Assert.True(hashTable.TryGetValue(guidKey, out value));
    }
    [Fact]
    public void WhenAttemptingToGetValueFromExistingKeyEntry_ShouldSetCorrespondingValue() {
      var guidKey = Guid.NewGuid();
      hashTable[guidKey] = 100;
      int value;
      hashTable.TryGetValue(guidKey, out value);
      Assert.Equal(value, 100);
    }
    [Fact]
    public void WhenAttemptingToGetValueFromNonExistentKeyEntry_ShouldReturnFalse() {
      var guidKey = Guid.NewGuid();
      var unknownGuid = Guid.NewGuid();
      hashTable[guidKey] = 100;
      int value;
      Assert.False(hashTable.TryGetValue(unknownGuid, out value));
    }
    [Fact]
    public void WhenAttemptingToGetValueFromNonExistentKeyEntry_ShouldSetDefaultValue() {
      var guidKey = Guid.NewGuid();
      var unknownGuid = Guid.NewGuid();
      hashTable[guidKey] = 100;
      int value;
      hashTable.TryGetValue(unknownGuid, out value);
      Assert.Equal(value, default(int));
    }
    [Fact]
    public void WhenEntriesDoNotExceedLoadFactor_ShouldNotResizeDataStore() {
      int expected = hashTable.Store.Length;
      var rand = new Random();
      for (var i = 0; i < expected; i++) {
        var guidKey = Guid.NewGuid();
        var value = rand.Next() % 1000 + 1; 
        hashTable[guidKey] = value;
      }
      int actual = hashTable.Store.Length;
      Assert.Equal(expected, actual);
    }
    [Fact]
    public void WhenEntriesExceedLoadFactor_ShouldResizeDataStore() {
      int expected = hashTable.Store.Length * 2;
      var rand = new Random();
      for (var i = 0; i < 19; i++) {
        var guidKey = Guid.NewGuid();
        var value = rand.Next() % 1000 + 1; 
        hashTable[guidKey] = value;
      }
      int actual = hashTable.Store.Length;
      Assert.Equal(expected, actual);
    }
    [Fact]
    public void WhenEntriesDoNotExceedLoadFactor_ShouldKeepKeyValueMappings() {
      int n = hashTable.Store.Length;
      List<KeyValuePair<Guid, int>> kvps = new List<KeyValuePair<Guid, int>>();
      var rand = new Random();
      for (var i = 0; i < n; i++) {
        var guidKey = Guid.NewGuid();
        var value = rand.Next() % 1000 + 1; 
        hashTable[guidKey] = value;
        kvps.Add(KeyValuePair.Create(guidKey, value));
      }
      for (var i = 0; i < kvps.Count; i++) {
        Assert.Equal(hashTable[kvps[i].Key], kvps[i].Value);
      }
    }
    [Fact]
    public void WhenEntriesExceedLoadFactor_ShouldKeepKeyValueMappings() {
      int n = hashTable.Store.Length * 2;
      List<KeyValuePair<Guid, int>> kvps = new List<KeyValuePair<Guid, int>>();
      var rand = new Random();
      for (var i = 0; i < 19; i++) {
        var guidKey = Guid.NewGuid();
        var value = rand.Next() % 1000 + 1; 
        hashTable[guidKey] = value;
        kvps.Add(KeyValuePair.Create(guidKey, value));
      }
      for (var i = 0; i < kvps.Count; i++) {
        Assert.Equal(hashTable[kvps[i].Key], kvps[i].Value);
      }
    }
  }
}
