using System;
using NUnit.Framework;
using LearnersLanguage;

namespace LearnersLanguageTest
{
    [TestFixture]
    public class ParserTest
    {
        private Parser _parser;
        private Lexer _lexer;

        [SetUp]
        public void ResetLexer()
        {
            // Reset both every test
            _lexer = new Lexer();
            _parser = new Parser();
        }

        [Test]
        public void GenOpNode()
        {
            _lexer.AddStatement("1+1");
            _parser.LoadTokens(_lexer);
            
            Console.WriteLine(_parser.GetAst());
        }
    }
}