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
    }
}