using System;

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
            // A grammar json object to load from
            var json = "{" +
                       "    'origin': 'The #person# was feeling #mood#.'," +
                       "    'person': ['girl', 'dwarf', 'cat', 'dragon']," +
                       "    'mood': ['bashful', 'dopey', 'happy', 'sleepy', 'sneezy', 'grumpy']" +
                       "}";

            // The grammar constructor can take a string or a FileInfo
            var grammar = new TraceryNet.Grammar(json);

            // Alternatively:
            //      var grammar = new TraceryNet.Grammar(new FileInfo("grammar.json"));
            //
            // .. which would read all text from grammar.json and use that.

            // Generate 100 strings.
            for (var i = 0; i < 100; i++)
            {
                // Start generating from #origin#
                Console.WriteLine(grammar.Flatten("#origin#"));

                // Tracery will then run through the only string in #origin#
                // and find #person# and look up the 'person' rule.
                //
                // Seeing that person is an array and not a singular string,
                // it will pick one at random: say, 'girl'.
                //
                // Then, going back to origin, it'll continue until it finds
                // #mood#, doing the same thing, picking a mood at random,
                // say, happy.
                //
                // This results in the output: the girl was feeling happy.
                //
                // Sample outputs:
                //      The dwarf was feeling grumpy.
                //      The girl was feeling sneezy.
                //      The girl was feeling sleepy.
                //      The dwarf was feeling grumpy.
                //      The dragon was feeling dopey.
            }

            Console.ReadKey();
        }
    }
}
