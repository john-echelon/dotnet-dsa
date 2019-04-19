using System;
using utils;
using sorting;
namespace dotnet_dsa
{
    class Program
    {
        static void Main(string[] args)
        {
            var arr = new int[20];
            Helper.FillRandom(arr, 100);
            var output = Helper.Display(arr);
            Console.WriteLine("{0}", output);
            Console.WriteLine("Sorting...");

            Sort.Bubble(arr);
            output = Helper.Display(arr);
            Console.WriteLine("{0}", output);
        }
    }
}
