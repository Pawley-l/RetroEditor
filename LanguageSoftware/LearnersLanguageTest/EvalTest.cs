using LearnersLanguage;
using NUnit.Framework;

namespace LearnersLanguageTest
{
    [TestFixture]
    public class EvalTest
    {
        private Eval _eval;
        private Parser _parser;
        private Lexer _lexer;
        
        [SetUp]
        public void ResetLexer()
        {
            _lexer = new Lexer();
            _parser = new Parser();
            _eval = new Eval();
        }
        
        [Test]
        public void ExecuteMath()
        {
            
        }
        
        [Test]
        public void ExecuteAssignValue()
        {
            
        }
        
        [Test]
        public void ExecuteCallFunction()
        {
            
        }
    }
}