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

            try
            {
                dict.Insert("Beograd", 1000000);
                dict.Insert("Novi Sad", 2000000);
                dict.Insert("aAFAEF", 3000000);

                Console.WriteLine(dict.GetValue("Beograd"));
                Console.WriteLine(dict.GetValue("Novi Sad"));
                Console.WriteLine(dict.GetValue("aAFAEF"));

                dict.PrintList();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
