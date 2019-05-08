using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using Utils;
namespace Algorithms {
  public static class Numeric {
    public static int[] GetPrimes (int n) {
      bool[] prime = new bool[n+1];
      SieveOfEratosthenes(prime);
      return prime
        .Select((val, index) => new { val, index})
        .Where(el => el.val == true)
        .Select(el => el.index).ToArray();
    }
    public static void SieveOfEratosthenes (bool[] prime) {
      int n = prime.Length - 1;
      Array.Fill(prime, true);
      prime[0] = prime[1] = false;
      for (var p = 2; p*p <= n; p++) {
        if(prime[p]) {
          for (var i = p * p; i <= n; i+=p) {
            prime[i] = false;
          }
        }
      }
    }
  }
}