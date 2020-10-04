using System;
using System.Collections.Generic;
using NUnit.Framework;
using LearnersLanguage;
using LearnersLanguage.Nodes.Data;
using LearnersLanguage.Nodes.Func;

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
            var ast = _parser.GetAst();

            if (ast[0] is OpNode node)
            {
                if (node.Left is IntNode left)
                    Assert.AreEqual(left.Number, 1);
                if (node.Left is IntNode right)
                    Assert.AreEqual(right.Number, 1);
                Assert.AreEqual(node.OpType, OpNode.Type.Add);
            }
            else
                Assert.Fail();
        }
        
        [Test]
        public void GenFuncCallNode()
        {
            // TODO: Randomize names, better test
            
            _lexer.AddStatement("function()");
            _parser.LoadTokens(_lexer);
            var ast = _parser.GetAst();

            if (ast[0] is FuncCallNode node)
            {
                if (node.Identifier is IdentifierNode identity)
                {
                    Assert.AreEqual(identity.Identifier, "function");
                }
                else 
                    Assert.Fail();
            }
            
        }
        
        [Test]
        public void GenDeclarerNode()
        {
            _lexer.AddStatement("value=100");
            _parser.LoadTokens(_lexer);
            var ast = _parser.GetAst();

            Console.WriteLine(ast[0]);

            if (ast[0] is DeclareVarNode node)
            {
                if (node.Left is IdentifierNode identity)
                    Assert.AreEqual(identity.Identifier, "value");
                else
                    Assert.Fail();
                
                if (node.Right is IntNode value)
                    Assert.AreEqual(value.Number, 100);
            }
        }
    }
}