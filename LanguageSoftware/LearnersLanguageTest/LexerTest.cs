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
        private Lexer lexer;

        [SetUp]
        public void ResetLexer()
        {
            lexer = new Lexer();
        }
        
        // Arithmetic
        [Test]
        public void GenArithmetic()
        {
            string[] comparison = new string[12] {
                "TOKEN_NAME", 
                "TOKEN_EQU", 
                "TOKEN_INT",
                "TOKEN_ADD",
                "TOKEN_INT",
                "TOKEN_SUB",
                "TOKEN_INT",
                "TOKEN_DIV",
                "TOKEN_INT",
                "TOKEN_MUL",
                "TOKEN_INT",
                "TOKEN_END"
            };
            
            lexer.AddStatement("variable = 1 + 1 - 1 / 1 * 1");

            for (int i = 0; i < 12; i++)
            {
                string compare = comparison[i];
                var token = lexer.GetTokens().ElementAt(i);
                
                Assert.AreEqual(token.Type, compare);
                
                Console.WriteLine(i + "| " + token.Type + " == " + compare);
            }
        }
        // Variables
        
        [Test]
        public void GenVariable()
        {
            string[] comparison = new string[4] {
                "TOKEN_NAME", 
                "TOKEN_EQU", 
                "TOKEN_INT",
                "TOKEN_END"
            };
            
            lexer.AddStatement("variable = 10");
            
            for (int i = 0; i < 3; i++)
            {
                string compare = comparison[i];
                var token = lexer.GetTokens().ElementAt(i);
                
                Assert.AreEqual(token.Type, compare);
                
                Console.WriteLine(i + "| " + token.Type + " == " + compare);
            }
        }

        [Test]
        public void IncorrectVariable()
        {
            string[] comparison = new string[2] {
                "TOKEN_UNKNOWN",
                "TOKEN_END"
            };
            
            lexer.AddStatement("11BadVariable");
            
            for (int i = 0; i < 2; i++)
            {
                string compare = comparison[i];
                var token = lexer.GetTokens().ElementAt(i);
                
                Assert.AreEqual(token.Type, compare);
                
                Console.WriteLine(i + "| " + token.Type + " == " + compare);
            }
        }
        
        // Keyword match
        
        [Test]
        public void GenKeyword()
        {
            string[] comparison = new string[2] {
                "TOKEN_KEYWORD",
                "TOKEN_END"
            };
            
            lexer.AddStatement("METHOD");
            
            for (int i = 0; i < 2; i++)
            {
                string compare = comparison[i];
                var token = lexer.GetTokens().ElementAt(i);
                
                Assert.AreEqual(token.Type, compare);
                
                Console.WriteLine(i + "| " + token.Type + " == " + compare);
            }
        }

        // Conditions
        
        [Test]
        public void GenConditions()
        {
            string[] comparison = new string[5] {
                "TOKEN_KEYWORD",
                "TOKEN_INT",
                "TOKEN_EQL",
                "TOKEN_NAME",
                "TOKEN_END"
            };
            
            lexer.AddStatement("if 10 = hello");
            
            for (var i = 0; i < 5; i++)
            {
                var compare = comparison[i];
                var token = lexer.GetTokens().ElementAt(i);
                
                Assert.AreEqual(token.Type, compare);
                
                Console.WriteLine(i + "| " + token.Type + " == " + compare);
            }
        }

    }
}