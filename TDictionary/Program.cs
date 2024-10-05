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
            TeodorDictionary<string, int> dict = new TeodorDictionary<string, int>(20);

            Console.WriteLine(dict.HashKey("aaa"));
            Console.WriteLine(dict.HashKey("aaa"));
            Console.WriteLine(dict.HashKey("bb35g33 d"));
            Console.WriteLine(dict.HashKey("feasf"));
            Console.WriteLine(dict.HashKey("aahdthdt"));

            Console.ReadLine();
        }
    }
}
