using System;
using System.IO;

namespace TraceryNetExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var grammar = new TraceryNet.Grammar(new FileInfo(@"C:\TraceryExamples\test.json"));

            for (var i = 0; i < 100; i++)
            {
                Console.WriteLine(grammar.Flatten("#sentence#"));
            }

            Console.ReadKey();
        }
    }
}
