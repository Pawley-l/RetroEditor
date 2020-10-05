using System;
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
     * </summary>
     *
     * TODO: Implement template method pattern for better error handling
     * TODO: Clean and simplify
     * TODO: Class too big
     * TODO: Make conditionals, methods and loops recursive so multiple can be defined at once
     */
    public class Parser
    {
        /*
         * When defining a method it has to generate it's own AST for the lines within the method. When this happens,
         * main abstract syntax tree is moved onto the back abstract syntax tree. And then the parsing continues until
         * the parser hits the ENDMETHOD keyword which it then swaps them back and adds the method to the main ast. 
         */
        private List<INode> _backAbstractSyntaxTree = new List<INode>(); 
        private bool _inMethod, _inLoop, _inConditional;
        private MethodNode _backDeclareMethod;
        private LoopNode _backDefineLoop;
        private ConditionalNode _backConditionalNode;
        
        private List<INode> _abstractSyntaxTree = new List<INode>();
        private List<Token> _currentStatement = new List<Token>();

        /**
         * <summary>
         * Method loads the lex and generates the Abstract Syntax Tree which can be gotten by GetAst()
         * </summary>
         */
        public List<INode> LoadTokens(Lexer lexer)
        {
            try
            {
                foreach (var token in lexer.GetTokens())
                {
                    if (token.Type == Token.TokenType.TOKEN_END)
                    {
                        var node = ParseStatement(null);
                        if (node != null)
                            _abstractSyntaxTree.Add(node);
                        _currentStatement.Clear();
                    }
                    else
                        _currentStatement.Add(token);
                }
            }
            catch (SyntaxErrorException)
            {
                throw;
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

            try
            {
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
                            return DeclareVar(previous);
                        break;
                    case Token.TokenType.TOKEN_LPAR:
                        if (previous is IdentifierNode)
                            return CallFunction(previous);
                        break;
                    case Token.TokenType.TOKEN_COMMA:
                        Next();
                        return null;
                    case Token.TokenType.TOKEN_KEYWORD:
                        TokenKeyword(previous);
                        break;
                    case Token.TokenType.TOKEN_INT:
                        return TokenInt();
                    case Token.TokenType.TOKEN_SYMBOL:
                        return TokenSymbol();
                    default:
                        return null;;
                }
            }
            catch (SyntaxErrorException)
            {
                throw;
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

            switch (next)
            {
                case DeclareVarNode _:
                    return next;
                case FuncCallNode func:
                    return func;
            }

            if (!(next is OpNode opnext)) return currentNode;
            
            opnext.Left = currentNode;
            return opnext;
        }

        private INode TokenKeyword(INode previous)
        {
            // Split the AST at the end for each keyword
            switch (_currentStatement[0].Value)
            {
                    case "IF":
                        TokenConditional();
                        return null;
                    case "ENDIF":
                        if (_inConditional)
                        {
                            _backConditionalNode.Body = _abstractSyntaxTree;
                            _abstractSyntaxTree = _backAbstractSyntaxTree;
                            _inConditional = false;
                            _abstractSyntaxTree.Add(_backConditionalNode);

                            return null;
                        }
                        break;
                    case "==":
                        if (_inConditional)
                        {
                            _backConditionalNode.Left = previous;
                            var next = ParseStatement(_backConditionalNode);
                            _backConditionalNode.Right = next;
                            
                                                        
                            _backAbstractSyntaxTree = _abstractSyntaxTree;
                            _abstractSyntaxTree = new List<INode>();
                            _abstractSyntaxTree.Clear();
                            return null;
                        }
                        break;
                    
                    case "METHOD":
                        return DeclareMethod();
                    case "ENDMETHOD":
                        if (_inMethod)
                        {
                            _backDeclareMethod.Body = _abstractSyntaxTree;
                            _abstractSyntaxTree = _backAbstractSyntaxTree;
                            _inMethod = false;
                            _abstractSyntaxTree.Add(_backDeclareMethod);
                            return null;
                        }
                        break;
                    case "LOOP":
                        TokenLoop();
                        return null;
                    case "ENDLOOP":
                        if (_inLoop)
                        {
                            _backDefineLoop.Body = _abstractSyntaxTree;
                            _abstractSyntaxTree = _backAbstractSyntaxTree;
                            _inLoop = false;
                            _abstractSyntaxTree.Add(_backDefineLoop);
                            return null;
                        }
                        break;
            }
            // Should never reach here
            throw new SyntaxErrorException("Undefined keyword caused by unknown reason.");
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
        private INode DeclareVar(INode previous)
        {
            var currentNode = new DeclareVarNode(previous);
            var next = ParseStatement(currentNode);
            currentNode.Right = next;
            return currentNode;
        }

        /**
         * Declare Method
         * TODO: This whole method needs cleaning up
         */
        private INode DeclareMethod()
        {
            Next();
            _backDeclareMethod = new MethodNode();
            _backDeclareMethod.Identifier = new IdentifierNode(_currentStatement[0].Value);
            Next();
            
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
            
            _backDeclareMethod.Parameters = parameters.ToArray();
            _inMethod = true;
            _backAbstractSyntaxTree = _abstractSyntaxTree;
            _abstractSyntaxTree = new List<INode>();
            _currentStatement.Clear();
            return null;
        }

        private INode TokenConditional()
        {
            Next();
            _backConditionalNode = new ConditionalNode();
            _inConditional = true;
            ParseStatement(null);
            return null;
        }
        
        private INode TokenLoop()
        {
            Next();
            // Checking for the correct syntax
            if (!(_currentStatement[0].Type is Token.TokenType.TOKEN_KEYWORD && _currentStatement[0].Value is "FOR"))
                throw new SyntaxErrorException("Syntax Error: LOOP must have a FOR");
            Next();
            
            if (_currentStatement.Count == 0)
                throw new SyntaxErrorException("Syntax Error: Must Specify a value to loop by");
            
            _backDefineLoop = new LoopNode(ParseStatement(null));
            
            _inLoop = true;
            _backAbstractSyntaxTree = _abstractSyntaxTree;
            _abstractSyntaxTree = new List<INode>();
            _currentStatement.Clear();
            return null;
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