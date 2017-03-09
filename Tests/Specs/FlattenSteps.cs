using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using TraceryNet;

namespace Tests
{
    [Binding]
    public class FlattenSteps
    {
        private Grammar _grammar;
        private string _output;

        [Given(@"I have a string to be flattened: ""(.*)""\.")]
        public void GivenIHaveAStringToBeFlattened_(string input)
        {
            var json = "{" +
                       "    'origin': '" + input + "'" +
                       "}";

            _grammar = new Grammar(json);
        }
        
        [When(@"I flatten it\.")]
        public void WhenIFlattenIt_()
        {
            _output = _grammar.Flatten("#origin#");
        }
        
        [Then(@"It should return ""(.*)""\.")]
        public void ThenItShouldReturn_(string correctOutput)
        {
            Assert.AreEqual(_output, correctOutput);
        }
    }
}
