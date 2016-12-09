using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text.RegularExpressions;
using TraceryNet;

namespace Tests
{
    [TestClass]
    public class ExpansionRegexTests
    {
        [TestMethod]
        public void ExpansionRegex_OneMatchNoModifiers_OneMatch()
        {
            // Arrange
            var rule = "#colour#";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.IsTrue(matches.Count == 1);
            Assert.IsTrue(matches[0].Value == "#colour#");
        }

        [TestMethod]
        public void ExpansionRegex_TwoMatchesNoModifiers_TwoMatches()
        {
            // Arrange
            var rule = "#colour# #animal#";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.IsTrue(matches.Count == 2);
            Assert.IsTrue(matches[0].Value == "#colour#");
            Assert.IsTrue(matches[1].Value == "#animal#");
        }

        [TestMethod]
        public void ExpansionRegex_OneMatchOneModifier_OneMatch()
        {
            // Arrange
            var rule = "#animal.capitalize#";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.IsTrue(matches.Count == 1);
            Assert.IsTrue(matches[0].Value == "#animal.capitalize#");
        }

        [TestMethod]
        public void ExpansionRegex_FourMatchesSentence_FourMatches()
        {
            // Arrange
            var rule = "The #animal# was #adjective.comma# #adjective.comma# and #adjective#.";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.IsTrue(matches.Count == 4);
            Assert.IsTrue(matches[0].Value == "#animal#");
            Assert.IsTrue(matches[1].Value == "#adjective.comma#");
            Assert.IsTrue(matches[2].Value == "#adjective.comma#");
            Assert.IsTrue(matches[3].Value == "#adjective#");
        }

        [TestMethod]
        public void ExpansionRegex_OneMatchSaveSymbol_OneMatch()
        {
            // Arrange
            var rule = "#[hero:#name#][heroPet:#animal#]story#";

            // Act
            var matches = Grammar.ExpansionRegex.Matches(rule);

            // Assert
            Assert.IsTrue(matches.Count == 1);

            // Even though there's sub-expansion symbols, it should only match once around
            // the whole thing.
            Assert.IsTrue(matches[0].Value == "#[hero:#name#][heroPet:#animal#]story#");
        }
    }
}