using System;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using Tracery.Net;

namespace TraceryNet
{
    public class Grammar
    {
        public JObject Rules;
        private Random Random = new Random();

        public Grammar(FileInfo source) : this(File.ReadAllText(source.FullName))
        {
        }

        public Grammar(string source)
        {
            Rules = JsonConvert.DeserializeObject<dynamic>(source);    
        }

        public string Flatten(string rule)
        {
            // Get all expansion symbols
            var regex = new Regex(@"#.+?#");

            // Iterate expansion symbols
            foreach (Match match in regex.Matches(rule))
            {
                // Remove the # surrounding the symbol name
                var matchName = match.Value.Replace("#", "");

                // Get modifiers
                var modifiers = new List<string>();

                if (match.Value.Contains("."))
                {
                    matchName = matchName.Substring(0, matchName.IndexOf("."));

                    modifiers = match.Value.Replace("#", "").Split('.').ToList();
                    modifiers.RemoveAt(0);
                }

                // Get the selected rule
                var selectedRule = Rules[matchName];
                
                if (selectedRule.Type == JTokenType.Array)
                {
                    // If the rule has any children then pick one at random
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

        private string ApplyModifiers(string resolved, List<string> modifiers)
        {
            foreach (var modifier in modifiers)
            {
                switch(modifier)
                {
                    case "capitalize":
                        resolved = Modifiers.Capitalize(resolved);
                        break;
                    case "comma":
                        resolved = Modifiers.Comma(resolved);
                        break;
                    case "inQuotes":
                        resolved = Modifiers.InQuotes(resolved);
                        break;
                    case "beeSpeak":
                        resolved = Modifiers.BeeSpeak(resolved);
                        break;
                    case "s":
                        resolved = Modifiers.S(resolved);
                        break;
                    default:
                        continue;
                }
            }

            return resolved;
        }
    }
}