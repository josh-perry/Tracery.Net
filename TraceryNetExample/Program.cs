using System;
using System.IO;

namespace TraceryNetExample
{
    class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // TODO: Move this into a resource or something.
            var grammar = new TraceryNet.Grammar(new FileInfo(@"C:\TraceryExamples\test.json"));

            // Just generate 100 strings.
            for (var i = 0; i < 100; i++)
            {
                Console.WriteLine(grammar.Flatten("#origin#"));
                Console.ReadKey();
            }

            Console.ReadKey();
        }
    }
}
