using System;
using Xunit;
using DotNetDsa.DataStructures;

namespace DotNetDsa.Tests.DataStructures
{
  public class HashTableTests
  {
    HashTable<Guid, int> hashTable;
    public HashTableTests() {
      hashTable = new HashTable<Guid, int>();
    }

    [Fact]
    public void AfterConstr_ShouldHaveNoKeys()
    {
      Assert.True(hashTable.Keys.Count == 0);
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
  }
}
