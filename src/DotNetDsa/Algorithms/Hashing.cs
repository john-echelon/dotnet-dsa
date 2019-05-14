using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using DotNetDsa.Utils;
namespace DotNetDsa.Algorithms {
  public class Hashing {
    public static Func<uint, uint> UniversalHashingFamily(uint a, uint b, uint p) {
      return x => (a * x + b) % p;
    }
    public static Func<uint, uint> UniversalHashingFamily () {
      uint a = 536869483, b = 715826819, p = 1073741789;
      return UniversalHashingFamily(a, b, p);
    }
  }
}