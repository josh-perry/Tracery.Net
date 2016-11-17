using System;
using System.IO;

namespace TraceryNetExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var source = File.ReadAllText(@"C:\TraceryExamples\test.json");
            var grammar = new TraceryNet.Grammar(source);

            for (var i = 0; i < 100; i++)
            {
                Console.WriteLine(grammar.Flatten("#sentence#"));
            }

            Console.ReadKey();
        }
    }
}
