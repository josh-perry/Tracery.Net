using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class SaveSymbolTests
    {
        [TestMethod]
        public void SaveSymbol_NoExpansionSymbol_Saves()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#[hero:Alfred]story#'," +
                       "    'story': 'His name was #hero#.'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.IsTrue(output == "His name was Alfred.");
        }

        [TestMethod]
        public void SaveSymbol_OneExpansionSymbol_Saves()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#[hero:#name#]story#'," +
                       "    'name': 'Alfred'," +
                       "    'story': 'His name was #hero#.'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.IsTrue(output == "His name was Alfred.");
        }

        [TestMethod]
        public void SaveSymbol_NoExpansionSymbolWithModifier_Saves()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#[hero:alfred]story#'," +
                       "    'story': 'His name was #hero.capitalize#.'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.IsTrue(output == "His name was Alfred.");
        }

        [TestMethod]
        [Description("hello")]
        public void SaveSymbol_OneLevelDeep_Saves()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#[setName]name#'," +
                       "	'setName': '[name:Luigi]'," +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.IsTrue(output == "Luigi");
        }

        [TestMethod]
        public void SaveSymbol_TwoLevelsDeep_Saves()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#[setName]name#'," +
                       "	'setName': '[name:#maleNames#]'," +
                       "	'maleNames': 'Mario'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.IsTrue(output == "Mario");
        }
    }
}