﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LLanguage
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
        private readonly List<Token> _tokens = new List<Token>();
        
        /**
         * <summary>
         * Adds a statement to the end of the token list and finishes it with a TOKEN_END
         * </summary>
         */
        public void AddStatement(string statement)
        {
            // TODO: Improve detection from just spaces
            // I think nodes is the wrong name
            var tmpnodes = SplitInput(statement);

            var added = false;
            var skip = false;
            
            var nodes = new List<string>();
            
            /*
             * TODO: TEMP solution, need more elegant fix
             */
            for (var i = 0; i < tmpnodes.Count; i++)
            {
                if (skip)
                {
                    skip = false;
                    continue;
                }

                if (tmpnodes.ElementAt(i) == "=" && tmpnodes.ElementAt(i + 1) == "=")
                {
                    nodes.Add("==");
                    skip = true;
                    continue;
                }
                nodes.Add(tmpnodes.ElementAt(i));
            }
            
            foreach (var node in nodes)
            {
                var token = Token.StrToToken(node);
                var value = "null";
                
                if (string.IsNullOrEmpty(node) || string.IsNullOrWhiteSpace(node))
                    continue;
                
                if (token == Token.TokenType.TOKEN_INT || token == Token.TokenType.TOKEN_SYMBOL 
                                                       || token == Token.TokenType.TOKEN_KEYWORD)
                    value = node;
                
                added = true;
                _tokens.Add(new Token(token, value));
            }
            if (added)
                _tokens.Add(new Token(Token.TokenType.TOKEN_END, "null"));
        }
        
        /**
         * Converts the string into a string list based on: @"(\d+)|(\w+)|\+|\-|\*|\/|\,|\(|\)|\=")
         */
        public static List<string> SplitInput(string input)
        {
            var matchList = Regex.Matches(input, @"(\d+)|(\w+)|\+|\-|\*|\/|\,|\(|\)|\=");
            var all = matchList.Cast<Match>().Select(match => match.Value).ToList();

            return all;
        }
        
        /**
         * <summary> Gets the tokens - should be run after finished adding statements with AddStatement() </summary>
         */
        public IEnumerable<Token> GetTokens()
        {
            return _tokens;
        }
    }
}