using System;
using System.Collections.Generic;
using System.Linq;
using LearnersLanguage.InternalTypes;

namespace LearnersLanguage
{
    /**
     * <summary>
     * A lexer converts statements into a computer parsable list of tokens. The lexer doesn't do any error checking
     * These tokens are then given to the parser. Syntax which cannot be converted into tokens are given the
     * TOKEN_UNKNOWN type but no exception is thrown. This is because the parser is able to create better informative
     * error messages
     * 
     * </summary>
     */
    public class Lexer
    { 
        List<Token> tokens = new List<Token>();
        
        /**
         * <summary>
         * Adds a statement to the end of the token list and finishes it with a TOKEN_END
         * </summary>
         */
        public void AddStatement(string statement)
        {
            // TODO: Improve detection from just spaces
            // I think nodes is the wrong name
            var nodes = statement.Split(" ").ToList();

            foreach (var node in nodes)
            {
                var token = Token.StrToToken(node);
                var value = "null";
                
                if (node == " ")
                    continue;
                
                if (token == Token.TokenType.TOKEN_INT || token == Token.TokenType.TOKEN_SYMBOL 
                                                       || token == Token.TokenType.TOKEN_KEYWORD)
                    value = node;

                tokens.Add(new Token(token, value));
            }
            
            tokens.Add(new Token(Token.TokenType.TOKEN_END, "null"));
        }

        public IEnumerable<Token> GetTokens()
        {
            return tokens;
        }
    }
}