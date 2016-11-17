using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Tests
{
    [TestClass]
    public class FlattenTests
    {
        [TestMethod]
        public void Flatten_HelloWorld_Success()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'hello world'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);
            
            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "hello world");
        }


        [TestMethod]
        public void Flatten_ExpandSymbol_Animal()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'hello #animal#'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "hello cat");
        }


        [TestMethod]
        public void Flatten_Capitalize_FirstLetterCapitalized()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'hello #animal.capitalize#'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "hello Cat");
        }


        [TestMethod]
        public void Flatten_BeeSpeak_Beezz()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#animal.beeSpeak# are very important'," +
                       "    'animal': 'bees'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "beezzz are very important");
        }

        [TestMethod]
        public void Flatten_Comma_HelloCommaWorld()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#greeting.comma# #place#'," +
                       "    'greeting': 'Hello'," +
                       "    'place': 'world'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "Hello, world");
        }

        [TestMethod]
        public void Flatten_InQuotes_HelloQuoteWorldQuote()
        {
            // Arrange
            var json = "{" +
                       "    'origin': '#greeting# #place.inQuotes#'," +
                       "    'greeting': 'Hello'," +
                       "    'place': 'world'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "Hello \"world\"");
        }
        
        [TestMethod]
        public void Flatten_A_ACat()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are #animal.a#'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you are a cat");
        }

        [TestMethod]
        public void Flatten_A_AnElephant()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are #animal.a#'," +
                       "    'animal': 'elephant'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you are an elephant");
        }

        [TestMethod]
        public void Flatten_CaptitalizeA_ACat()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are #animal.capitalize.a#'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you are a Cat");
        }

        [TestMethod]
        public void Flatten_ACaptitalize_ACat()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are #animal.a.capitalize#'," +
                       "    'animal': 'cat'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you are A cat");
        }

        [TestMethod]
        public void Flatten_CaptitalizeAllCuteCat_CuteCat()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you are a #animal.capitalizeAll#'," +
                       "    'animal': 'cute cat'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you are a Cute Cat");
        }

        [TestMethod]
        public void Flatten_PastTensifyBully_Bullied()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you #verb.ed#'," +
                       "    'verb': 'bully'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you bullied");
        }

        [TestMethod]
        public void Flatten_PastTensifyQuack_Quacked()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you #verb.ed#'," +
                       "    'verb': 'quack'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you quacked");
        }

        [TestMethod]
        public void Flatten_PastTensifyCall_Called()
        {
            // Arrange
            var json = "{" +
                       "    'origin': 'you #verb.ed#?'," +
                       "    'verb': 'call'" +
                       "}";

            var grammar = new TraceryNet.Grammar(json);

            // Act
            var output = grammar.Flatten("#origin#");

            // Assert
            Assert.AreEqual(output, "you called?");
        }
    }
}