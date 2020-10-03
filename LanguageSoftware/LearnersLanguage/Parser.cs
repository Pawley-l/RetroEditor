﻿using System;
using System.Collections.Generic;
using LearnersLanguage.Exceptions;
using LearnersLanguage.Nodes;
using LearnersLanguage.Nodes.Data;
using LearnersLanguage.Nodes.Func;

namespace LearnersLanguage
{
    /**
     * <summary>
     * Parser takes the tokens and uses it to generate a AST. The Abstract Syntax Tree is a tree made of nodes which
     * are generated in a way based on the languages rules. 
     * 
     * </summary>
     */
    public class Parser
    {
        private List<INode> _abstractSyntaxTree = new List<INode>();
        private List<Token> _currentStatement = new List<Token>();
        
        /**
         * <summary>
         * Method loads the lex and generates the Abstract Syntax Tree which can be gotten by GetAst()
         * </summary>
         */
        public List<INode> LoadTokens(Lexer lexer)
        {
            foreach (var token in lexer.GetTokens())
            {
                if (token.Type == Token.TokenType.TOKEN_END)
                {
                    _abstractSyntaxTree.Add(ParseStatement(null));
                    _currentStatement.Clear();
                }
                else 
                    _currentStatement.Add(token);
            }

            return _abstractSyntaxTree;
        }
        
        /**
         * <summary>
         * Gets the Abstract Syntax Tree - LoadTokens() should be called first
         * </summary>
         */
        public List<INode> GetAst()
        {
            return _abstractSyntaxTree;
        }
        
        /**
         * Recursive statement which converts each token into a node and then correctly orders them
         * TODO: Make this more readable
         */
        private INode ParseStatement(INode previous)
        {
            if (previous != null)
                Next();

            if (_currentStatement.Count == 0)
                return null;
            
            switch (_currentStatement[0].Type)
            {
                case Token.TokenType.TOKEN_ADD:
                    if (previous is IntNode || previous is IdentifierNode)
                        return TokenOpNode(OpNode.Type.Add, previous);
                    break;
                case Token.TokenType.TOKEN_SUB:
                    if (previous is IntNode || previous is IdentifierNode)
                        return TokenOpNode(OpNode.Type.Sub, previous);
                    break;
                case Token.TokenType.TOKEN_DIV:
                    if (previous is IntNode || previous is IdentifierNode)
                        return TokenOpNode(OpNode.Type.Div, previous);
                    break;
                case Token.TokenType.TOKEN_MUL:
                    if (previous is IntNode || previous is IdentifierNode)
                        return TokenOpNode(OpNode.Type.Mul, previous);
                    break;
                case Token.TokenType.TOKEN_EQU:
                    if (previous is IntNode || previous is IdentifierNode)
                        return DeclareSymbol(previous);
                    break;
                case Token.TokenType.TOKEN_LPAR:
                    if (previous is IdentifierNode)
                        return CallFunction(previous);
                    break;
                case Token.TokenType.TOKEN_COMMA:
                    Next();
                    return null;
                case Token.TokenType.TOKEN_KEYWORD: // TODO: Make node for this when needed
                    break;
                case Token.TokenType.TOKEN_INT:
                    return TokenInt();
                case Token.TokenType.TOKEN_SYMBOL:
                    return TokenSymbol();
                default:
                    return null;;
            }
            return null;
        }
        
        /**
         * If this is called, it's rather the end of a statement or it is a part of a OpNode
         */
        private INode TokenInt()
        {
            var value = Convert.ToInt32(_currentStatement[0].Value);
            var next = ParseStatement(new IntNode(value));

            return next switch
            {
                OpNode _ => next,
                null => new IntNode(value),
                _ => throw new SyntaxErrorException("Unexpected value after integer")
            };
        }
        
        /**
         * OpNode needs to find the right value and then assign it to its right
         */
        private INode TokenOpNode(OpNode.Type type, INode previous)
        {
            var currentNode = new OpNode(type, previous);
            var next = ParseStatement(currentNode);
            currentNode.Right = next;
            return currentNode;
        }
        
        /**
         * If this is called, it rather needs to get a value or declare a value
         */
        private INode TokenSymbol()
        {
            var currentNode = new IdentifierNode(_currentStatement[0].Value);
            var next = ParseStatement(currentNode);

            if (next is DeclarerNode)
                return next;
            if (next is FuncCallNode func)
                return func;
            
            if (!(next is OpNode opnext)) return currentNode;
            
            opnext.Left = currentNode;
            return opnext;

        }
        
        /**
         * When calling a function the parameters need to be mapped first before being added to the FuncCallNode
         */
        private INode CallFunction(INode previous)
        {
            var rindex = 0;
            for (var i = 0; i < _currentStatement.Count; i++)
                if (_currentStatement[i].Type == Token.TokenType.TOKEN_RPAR)
                    rindex = i;
            
            var parameters = new List<INode>();
            
            var parameterTokens = _currentStatement.GetRange(1, rindex);
            var holder = _currentStatement;
            _currentStatement = parameterTokens;
            
            for (var i = 0; i < rindex; i++)
            {
                var node = ParseStatement(null);
                if (node == null)
                    continue;
                    
                parameters.Add(node);
            }
            
            _currentStatement = holder;
            return new FuncCallNode(previous, parameters.ToArray());
        }
        
        /**
         * When declaring a variable, the node it equals to needs to be resolved first before the DeclarerNode can be added
         */
        private INode DeclareSymbol(INode previous)
        {
            var currentNode = new DeclarerNode(previous);
            var next = ParseStatement(currentNode);
            currentNode.Right = next;
            return currentNode;
        }
        
        /**
         * Moves onto the next value in the current statement.
         */
        private void Next()
        {
            _currentStatement.RemoveAt(0);
        }
    }
}