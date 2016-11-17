using System;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

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
            if(String.IsNullOrWhiteSpace(rule))
            {
                return "";
            }
            
            return Resolve(rule);
        }

        public string Resolve(string rule)
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
                    var resolved = Resolve(chosen);

                    resolved = ApplyModifiers(resolved, modifiers);

                    rule = rule.Replace(match.Value, resolved);
                }
                else 
                {
                    // Otherwise resolve it
                    var resolved = Resolve(selectedRule.ToString());

                    resolved = ApplyModifiers(resolved, modifiers);

                    rule = rule.Replace(match.Value, resolved);
                }
            }

            return rule;
        }

        private string ApplyModifiers(string resolved, List<string> modifiers)
        {
            var sentencePunctuation = new List<char> { ',', '.', '!', '?' };
            char lastChar;

            foreach (var modifier in modifiers)
            {
                switch(modifier)
                {
                    case "capitalize":
                        resolved = char.ToUpper(resolved[0]) + resolved.Substring(1);
                        break;
                    case "comma":
                        lastChar = resolved[resolved.Length - 1];
                        
                        if(sentencePunctuation.Contains(lastChar))
                            break;

                        resolved += ",";
                        break;
                    case "inQuotes":
                        resolved = "\"" + resolved + "\"";
                        break;
                    case "beeSpeak":
                        resolved = resolved.Replace("s", "zzz");
                        break;
                    default:
                        continue;
                }
            }

            return resolved;
        }
    }
}