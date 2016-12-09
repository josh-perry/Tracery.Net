using System;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace TraceryNet
{
    /// <summary>
    /// A set of rules that can be "flattened" to produce a single random string.
    /// Often a single json object.
    /// </summary>
    public class Grammar
    {
        /// <summary>
        /// Object containing all of the deserialized json rules.
        /// </summary>
        public JObject Rules;

        /// <summary>
        /// RNG to pick from multiple rules.
        /// </summary>
        private Random Random = new Random();

        /// <summary>
        /// Modifier function table.
        /// </summary>
        public Dictionary<string, Func<string, string>> ModifierLookup;

        /// <summary>
        /// Read all text from a file and pass it to the other constructor.
        /// </summary>
        /// <param name="source"></param>
        public Grammar(FileInfo source) : this(File.ReadAllText(source.FullName))
        {
        }

        /// <summary>
        /// Load the rules list by deserializing the source as a json object.
        /// </summary>
        /// <param name="source"></param>
        public Grammar(string source)
        {
            // Populate the rules list
            Rules = JsonConvert.DeserializeObject<dynamic>(source);

            // Set up the function table
            ModifierLookup = new Dictionary<string, Func<string, string>>
            {
                { "a",             Modifiers.A },
                { "beeSpeak",      Modifiers.BeeSpeak },
                { "capitalize",    Modifiers.Capitalize },
                { "comma",         Modifiers.Comma },
                { "inQuotes",      Modifiers.InQuotes },
                { "s",             Modifiers.S },
                { "ed",            Modifiers.Ed },
                { "capitalizeAll", Modifiers.CapitalizeAll }
            };
        }

        /// <summary>
        /// Resolve the list of rules into a single sentence.
        /// If the rule contains an expansion symbol, follow it and resolve those recursively.
        /// The result should be a single string.
        /// </summary>
        /// <param name="rule">The rule to start on. Often #origin#.</param>
        /// <returns>The resultant string, flattened from the rules.</returns>
        public string Flatten(string rule)
        {
            // Get all expansion symbols
            var regex = new Regex(@"(?<!\[|:)(?!\])#.+?(?<!\[|:)#(?!\])");
            
            // Iterate expansion symbols
            foreach (Match match in regex.Matches(rule))
            {
                // Remove the # surrounding the symbol name
                var matchName = match.Value.Replace("#", "");

                if (matchName.Contains("."))
                {
                    matchName = matchName.Substring(0, matchName.IndexOf("."));
                }

                // Get modifiers
                var modifiers = new List<string>();
                modifiers = GetModifiers(match.Value);

                // If there's no modifier with that name then skip
                if(modifiers == null)
                {
                    continue;
                }

                // Get the selected rule
                var selectedRule = Rules[matchName];

                // If the rule has any children then pick one at random
                if (selectedRule.Type == JTokenType.Array)
                {
                    var index = Random.Next(0, ((JArray)selectedRule).Count);
                    var chosen = selectedRule[index].ToString();
                    var resolved = Flatten(chosen);

                    resolved = ApplyModifiers(resolved, modifiers);

                    rule = rule.Replace(match.Value, resolved);
                }
                else 
                {
                    // Otherwise flatten it
                    var resolved = Flatten(selectedRule.ToString());

                    resolved = ApplyModifiers(resolved, modifiers);

                    rule = rule.Replace(match.Value, resolved);
                }
            }

            return rule;
        }

        /// <summary>
        /// Return a list of modifier names from the provided expansion symbol
        /// Modifiers are extra operations to perform on an expansion symbol.
        ///
        /// For instance:
        ///      #animal.capitalize#
        /// will flatten into a single animal and capitalize the first character of it's name.
        ///
        /// Multiple modifiers can be applied, separated by a .
        ///      #animal.capitalize.inQuotes#
        /// ...for example
        /// </summary>
        /// <param name="symbol">The symbol to take modifiers from:
        /// e.g: #animal#, #animal.s#, #animal.capitalize.s#
        /// </param>
        /// <returns></returns>
        private List<string> GetModifiers(string symbol)
        {
            var modifiers = symbol.Replace("#", "").Split('.').ToList();
            modifiers.RemoveAt(0);

            return modifiers;
        }

        /// <summary>
        /// Iterate over the list of modifiers on the expansion symbol and resolve each individually.
        /// </summary>
        /// <param name="resolved">The string to perform the modifications to</param>
        /// <param name="modifiers">The list of modifier strings</param>
        /// <returns>The resolved string with modifiers applied to it</returns>
        private string ApplyModifiers(string resolved, List<string> modifiers)
        {
            // Iterate over each modifier
            foreach (var modifier in modifiers)
            {
                // If there's no modifier by this name in the list, skip it
                if(!ModifierLookup.ContainsKey(modifier))
                    continue;

                // Otherwise execute the function and take the output
                resolved = ModifierLookup[modifier](resolved);
            }

            // Give back the string
            return resolved;
        }
    }
}
 