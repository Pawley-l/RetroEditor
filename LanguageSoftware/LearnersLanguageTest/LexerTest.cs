using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using LearnersLanguage;

namespace LearnersLanguageTest
{
    [TestFixture]
    public class LexerTest
    {
        private Lexer _lexer;

        [SetUp]
        public void ResetLexer()
        {
            // Reset every test
            _lexer = new Lexer();
        }
        
        // Arithmetic
        [Test]
        public void GenArithmetic()
        {
            var comparison = new Token.TokenType[12] {
                Token.TokenType.TOKEN_SYMBOL, 
                Token.TokenType.TOKEN_EQU, 
                Token.TokenType.TOKEN_INT,
                Token.TokenType.TOKEN_ADD,
                Token.TokenType.TOKEN_INT,
                Token.TokenType.TOKEN_SUB,
                Token.TokenType.TOKEN_INT,
                Token.TokenType.TOKEN_DIV,
                Token.TokenType.TOKEN_INT,
                Token.TokenType.TOKEN_MUL,
                Token.TokenType.TOKEN_INT,
                Token.TokenType.TOKEN_END
            };
            
            _lexer.AddStatement("variable= 1 + 1 - 1 / 1 * 1");

            for (int i = 0; i < 12; i++)
            {
                var compare = comparison[i];
                var token = _lexer.GetTokens().ElementAt(i);
                
                Assert.AreEqual(token.Type, compare);
                
                Console.WriteLine(i + "| " + token.Type + " == " + compare);
            }
        }
        // Variables
        
        [Test]
        public void GenVariable()
        {
            var comparison = new Token.TokenType[4] {
                Token.TokenType.TOKEN_SYMBOL, 
                Token.TokenType.TOKEN_EQU, 
                Token.TokenType.TOKEN_INT,
                Token.TokenType.TOKEN_END
            };
            
            _lexer.AddStatement("variable = 10");
            
            for (int i = 0; i < 3; i++)
            {
                var compare = comparison[i];
                var token = _lexer.GetTokens().ElementAt(i);
                
                Assert.AreEqual(token.Type, compare);
                
                Console.WriteLine(i + "| " + token.Type + " == " + compare);
            }
        }

        // Keyword match
        
        [Test]
        public void GenKeyword()
        {
            var comparison = new Token.TokenType[2] {
                Token.TokenType.TOKEN_KEYWORD,
                Token.TokenType.TOKEN_END
            };
            
            _lexer.AddStatement("METHOD");
            
            for (int i = 0; i < 2; i++)
            {
                var compare = comparison[i];
                var token = _lexer.GetTokens().ElementAt(i);
                
                Assert.AreEqual(token.Type, compare);
                
                Console.WriteLine(i + "| " + token.Type + " == " + compare);
            }
        }

        // Conditions
        
        [Test]
        public void GenConditions()
        {
            var comparison = new Token.TokenType[5] {
                Token.TokenType.TOKEN_KEYWORD,
                Token.TokenType.TOKEN_INT,
                Token.TokenType.TOKEN_EQU,
                Token.TokenType.TOKEN_SYMBOL,
                Token.TokenType.TOKEN_END
            };
            
            _lexer.AddStatement("if 10 = hello");
            
            for (var i = 0; i < 5; i++)
            {
                var compare = comparison[i];
                var token = _lexer.GetTokens().ElementAt(i);
                
                Assert.AreEqual(token.Type, compare);
                
                Console.WriteLine(i + "| " + token.Type + " == " + compare);
            }
        }

    }
}