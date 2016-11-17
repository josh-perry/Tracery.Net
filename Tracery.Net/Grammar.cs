using System;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

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
            var regex = new Regex(@"#\w+#");

            // Iterate expansion symbols
            foreach (Match match in regex.Matches(rule))
            {
                // Remove the # surrounding the symbol name
                var matchName = match.Value.Replace("#", "");

                // Get the selected rule
                var selectedRule = Rules[matchName];
                
                if (selectedRule.Type == JTokenType.Array)
                {
                    // If the rule has any children then pick one at random
                    var index = Random.Next(0, ((JArray)selectedRule).Count);
                    var chosen = selectedRule[index].ToString();
                    
                    rule = rule.Replace(match.Value, Resolve(chosen));
                }
                else 
                {
                    // Otherwise resolve it
                    var resolved = Resolve(selectedRule.ToString());

                    rule = rule.Replace(match.Value, resolved);
                }
            }

            return rule;
        }
    }
}