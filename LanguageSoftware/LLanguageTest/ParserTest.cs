using System;
using System.Collections.Generic;
using NUnit.Framework;
using LLanguage;
using LLanguage.Nodes.Operations;
using LLanguage.Nodes.Static;
using LLanguage.Nodes.Types;

namespace LearnersLanguageTest
{
    [TestFixture]
    public class ParserTest
    {
        private Parser _parser;
        private Lexer _lexer;

        [SetUp]
        public void Reset()
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

            if (ast[0] is IntOpNode node)
            {
                if (node.Left is IntNode left)
                    Assert.AreEqual(left.Value, 1);
                if (node.Left is IntNode right)
                    Assert.AreEqual(right.Value, 1);
                Assert.AreEqual(node.OpType, IntOpNode.Type.Add);
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
                if (node.Identifier is ReferenceNode identity)
                {
                    Assert.AreEqual(identity.Value, "function");
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

            if (ast[0] is DeclareIntVarNode node)
            {
                if (node.Left is ReferenceNode identity)
                    Assert.AreEqual(identity.Value, "value");
                else
                    Assert.Fail();
                
                if (node.Right is IntNode value)
                    Assert.AreEqual(value.Value, 100);
            }
        }
    }
}