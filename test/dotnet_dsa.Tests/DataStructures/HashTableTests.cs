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
    [Fact]
    public void AttemptToGetValueFromUnknownKey_ShouldThrowException()
    {
      Exception expected = null;
      try {
        var value = hashTable[Guid.NewGuid()];
      } catch(Exception e){
        expected = e;
      }
      Assert.True(expected != null);
    }
  }
}
