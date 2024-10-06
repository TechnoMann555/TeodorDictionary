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
            TeodorDictionary<string, int> dict = new TeodorDictionary<string, int>(1);
            dict.PrintList();

            Console.WriteLine("=== Insertion ===");
            dict["Beograd"] = 100;
            dict.PrintList();

            Console.WriteLine("=== Updating ===");
            dict["Beograd"] = 200;
            dict.PrintList();

            Console.WriteLine("=== Fetching ===");
            Console.WriteLine($"Beograd - {dict["Beograd"]}");
            dict.PrintList();

            Console.WriteLine(dict["AAAAAAAAAAAAAAAAAAA"].ToString());

            Console.ReadLine();
        }
    }
}
