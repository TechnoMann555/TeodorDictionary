using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            TeodorDictionary<string, int> dict = new TeodorDictionary<string, int>(4);
            dict.PrintList();

            dict["Belgrade"] = 100;
            dict.PrintList();
            Console.WriteLine($"Item count: {dict.Count}; Bucket count: {dict.UsedBuckets}\n");

            dict["Novi Sad"] = 200;
            dict.PrintList();
            Console.WriteLine($"Item count: {dict.Count}; Bucket count: {dict.UsedBuckets}\n");

            dict["BElgrade"] = 200;
            dict.PrintList();
            Console.WriteLine($"Item count: {dict.Count}; Bucket count: {dict.UsedBuckets}\n");

            dict.Insert("Belgrade", 500);

            Console.ReadLine();
        }
    }
}
