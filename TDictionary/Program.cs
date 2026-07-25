using System;
using System.Collections.Generic;

namespace TDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            TeodorDictionary<string, int> dict = new TeodorDictionary<string, int>(5);
            dict.PrintList();

            Console.WriteLine("=== INSERTION ===");
            dict.Insert("WXMAX", 0);
            dict.Insert("ZNPKP", 1);
            dict.PrintList();

            dict.Insert("OVBDF", 1);
            dict.PrintList();

            try
            {
                dict.Insert("OVBDF", 1);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            dict.PrintList();

            Console.WriteLine("=== FETCHING === ");
            Console.WriteLine(dict.FetchValue("WXMAX"));
            Console.WriteLine(dict.FetchValue("ZNPKP"));
            Console.WriteLine(dict.FetchValue("OVBDF"));

            try
            {
                dict.FetchValue("AAAAAAAAA");
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("=== CHECKING EXISTANCE === ");
            Console.WriteLine(dict.CheckIfExists("WXMAX"));
            Console.WriteLine(dict.CheckIfExists("ZNPKP"));
            Console.WriteLine(dict.CheckIfExists("OVBDF"));
            Console.WriteLine(dict.CheckIfExists("AAAAAAAA"));

            Console.WriteLine("=== UPDATING ===");
            dict.Update("WXMAX", 100);
            dict.Update("ZNPKP", 200);
            dict.Update("OVBDF", 200);
            dict.PrintList();

            try
            {
                dict.Update("AAAAAAAAA", 400);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            dict.PrintList();

            Console.WriteLine("=== REMOVING ===");
            Console.WriteLine(dict.Remove("WXMAX"));
            Console.WriteLine(dict.Remove("ZNPKP"));
            Console.WriteLine(dict.Remove("OVBDF"));
            Console.WriteLine(dict.Remove("AAAAAAAAA"));

            dict.PrintList();

            Console.WriteLine("=== INDEXER ===");
            Console.WriteLine("=== INSERTION ===");
            dict["WXMAX"] = 0;
            dict["ZNPKP"] = 1;
            dict.PrintList();

            dict["OVBDF"] = 1;
            dict.PrintList();

            Console.WriteLine("=== FETCHING ===");
            Console.WriteLine(dict["WXMAX"]);
            Console.WriteLine(dict["ZNPKP"]);
            Console.WriteLine(dict["OVBDF"]);
            
            Console.WriteLine("=== UPDATING ===");
            dict["WXMAX"] = 100;
            dict["ZNPKP"] = 200;
            dict["OVBDF"] = 200;
            dict.PrintList();

            Console.WriteLine("=== ITERATING ===");
            foreach(KeyValuePair<string, int> pair in dict)
            {
                Console.WriteLine(pair);
            }

            // Only works when the max threshold is 3
            Console.WriteLine("=== REHASHING ===");
            dict = new TeodorDictionary<string, int>(1);
            dict["WXMAX"] = 100;
            dict["ZNPKP"] = 200;
            dict["OVBDF"] = 200;

            dict.PrintList();

            dict["NJXAU"] = 300;

            dict.PrintList();

            Console.WriteLine("=== CLEARING ===");
            dict = new TeodorDictionary<string, int>(10);
            dict["WXMAX"] = 100;
            dict["ZNPKP"] = 200;
            dict["OVBDF"] = 200;
            dict.PrintList();
            dict.Clear();
            dict.PrintList();

            Console.ReadLine();
        }
    }
}
