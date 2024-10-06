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
            TeodorDictionary<string, int> dict = new TeodorDictionary<string, int>(2);
            dict.PrintList();

            dict["Belgrade"] = 100;
            dict["BElgrade"] = 100;
            dict["Novi Sad"] = 200;
            dict["Kragujevac"] = 300;
            dict.PrintList();

            foreach(KeyValuePair<string, int> pair in dict)
            {
                Console.WriteLine(pair);
            }

            Console.ReadLine();
        }
    }
}
