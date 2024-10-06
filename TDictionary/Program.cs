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

            dict.Insert("Beograd", 1000000);
            dict.Insert("Novi Sad", 1500000);
            dict.Insert("Novi SAd", 1500000);
            dict.Insert("Jovanovac", 2000000);

            dict.PrintList();
                
            dict.Update("Beograd", 3000000);
            dict.Update("Novi Sad", 3000000);
            dict.Update("Novi SAd", 3000000);
            dict.Update("Jovanovac", 3000000);

            dict.PrintList();
                
            dict.Update("Dobrovac", 3000000);

            Console.ReadLine();
        }
    }
}
