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

            dict.Insert("Beograd", 1000000);
            dict.Insert("Novi Sad", 1500000);
            dict.Insert("Novi SAd", 1500000);
            dict.Insert("Jovanovac", 2000000);

            dict.PrintList();
            Console.WriteLine(dict.Remove("Dobrovac"));

            dict.PrintList();
            Console.WriteLine(dict.Remove("Jovanovac"));

            dict.PrintList();
            Console.WriteLine(dict.Remove("Novi SAD"));

            dict.PrintList();
            Console.WriteLine(dict.Remove("BEograd"));

            dict.PrintList();
            Console.WriteLine(dict.Remove("Novi Sad"));

            dict.PrintList();
            Console.ReadLine();
        }
    }
}
