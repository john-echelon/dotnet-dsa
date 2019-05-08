using System;
using Xunit;

namespace dotnet_dsa.Tests
{
  public class UnitTest1
  {
    int Add (int x, int y) {
      return x + y;
    }
    [Fact]
    public void Test1()
    {
      Assert.Equal(4, Add(2, 2));
    }
  }
}
