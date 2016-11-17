using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var source = File.ReadAllText(@"C:\TraceryExamples\test.json");
            var grammar = new TraceryNet.Grammar(source);
            
            grammar.Flatten("#origin#");
        }
    }
}