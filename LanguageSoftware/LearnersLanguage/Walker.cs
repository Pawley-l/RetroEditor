using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LearnersLanguage.Exceptions;
using LearnersLanguage.Nodes;
using LearnersLanguage.Nodes.Data;
using LearnersLanguage.Nodes.Func;

namespace LearnersLanguage
{
    /**
     * <summary>
     * The evaluator is walks up every "branch" in the Abstract Syntax Tree and then executes everything in order.
     * In order to resolve a branch below, the evaluator has to execute the branch which comes from it first, to
     * get it's return.
     * </summary>
     *
     * TODO: Move execution from this onto the individual node classes. This will make the code more readable and easier to maintain.
     */
    public class Walker
    {
        private Dictionary<string, IntNode> _backVariables = new Dictionary<string, IntNode>();
        private Dictionary<string, IntNode> _variables = new Dictionary<string, IntNode>();
        private Dictionary<string, Func<List<IntNode>, IntNode>> _func = new Dictionary<string, Func<List<IntNode>, IntNode>>();
        private List<MethodNode> _method = new List<MethodNode>();
        
        /**
         * <summary> Executes a AST </summary>
         */
        public void Execute(List<INode> ast)
        {
            foreach (var node in ast)
            {
                Execute(node);
            }
        }
        
        /**
         * Recursive method which executes a node but the node method might execute its own nodes first 
         */
        private INode Execute(INode node)
        {
            try
            {
                return node switch
                {
                    DeclareVarNode declarerNode => ExecuteDeclareNode(declarerNode),
                    OpNode opNode => ExecuteOpNode(opNode),
                    IdentifierNode identity => GetVariable(identity),
                    FuncCallNode call => ExecuteFunctionCall(call),
                    IntNode intNode => intNode,
                    MethodNode method => ExecuteDeclareMethod(method),
                    LoopNode loop => ExecuteLoopNode(loop),
                    ConditionalNode conditional => ExecuteConditional(conditional),
                    _ => new IntNode(-1)
                };
            }
            catch (UndeclaredSymbolException)
            {
                throw;
            }
        }
        
        /**
         * Resolves left and right values then executes itself turning into a IntNode
         */
        private INode ExecuteOpNode(OpNode node)
        {
            var right = Execute(node.Right);
            var left = Execute(node.Left);

            return node.OpType switch
            {
                OpNode.Type.Add => new IntNode(((IntNode) left).Number + ((IntNode) right).Number),
                OpNode.Type.Sub => new IntNode(((IntNode) left).Number - ((IntNode) right).Number),
                OpNode.Type.Mul => new IntNode(((IntNode) left).Number * ((IntNode) right).Number),
                OpNode.Type.Div => new IntNode(((IntNode) left).Number / ((IntNode) right).Number),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        private INode ExecuteConditional(ConditionalNode node)
        {
            var right = Execute(node.Right) as IntNode;
            var left = Execute(node.Left) as IntNode;

            Debug.Assert(right != null, nameof(right) + " != null");
            Debug.Assert(left != null, nameof(left) + " != null");
            
            if (right.Number == left.Number)
            {
                Execute(node.Body);
            }

            return null;
        }
        
        /**
         * Executes its equal first then sets the variable to it's value
         */
        private INode ExecuteDeclareNode(DeclareVarNode node)
        {
            // Dont execute because it hasnt been declared yet
            var identifier = node.Left as IdentifierNode;
            var right = Execute(node.Right);

            if (identifier != null)
                SetVariable(identifier, right as IntNode);
            return right;
        }
        
        /**
         * Function calls are mapped to functions in _func. The identifier node identifies the method and then executes
         * the nodes as parameters before invoking the method.
         */
        private INode ExecuteFunctionCall(FuncCallNode node)
        {
            if (!(node.Identifier is IdentifierNode identity)) return null;
            var parameters = node.Parameters.Select(parameter => Execute(parameter) as IntNode).ToList();

            if (_func.ContainsKey(identity.Identifier))
            {
                _func[identity.Identifier].DynamicInvoke(parameters);
            }
                
            ExecuteMethod(identity, parameters);

            return null;
        }
        
        /**
         * Executes user defined method if it exists
         */
        private INode ExecuteMethod(IdentifierNode identifier, List<IntNode> parameters)
        {
            foreach (var method in _method)
            {
                if (!(method.Identifier is IdentifierNode nodeid)) continue;
                if (nodeid.Identifier != identifier.Identifier) continue;
                _backVariables = _variables;
                for (var i = 0; i < parameters.Count; i++)
                {
                    if (method.Parameters[i] is IdentifierNode id)
                        SetVariable(id, parameters[i]);
                }

                Execute(method.Body);
                _variables = _backVariables;
            }

            return null;
        }
        
        private INode ExecuteDeclareMethod(MethodNode method)
        {
            _method.Add(method);
            return null;
        }

        private INode ExecuteLoopNode(LoopNode loop)
        {
            if (loop.ForValue is IdentifierNode id)
            {
                var count = GetVariable(id).Number;
                
                for (var i = 0; i < count; i++)
                {
                    count = GetVariable(id).Number;
                    
                    Execute(loop.Body);
                }
            }
            else if (loop.ForValue is IntNode value)
            {
                var count = value.Number;
                
                for (var i = 0; i < count; i++)
                {
                    Execute(loop.Body);
                }
            }

            return null;
        }
        
        /**
         * <summary>
         * Adds a definition of the method into the eval's state, so it can be executed in code.
         * </summary>
         *
         * TODO: This needs testing
         */
        public void MapMethod(string identity, Func<List<IntNode>, IntNode> func)
        {
            _func.Add(identity, func);
        }

        /**
         * <summary> Resets everything but the mapped methods. </summary>
         */
        public void Reset()
        {
            _method.Clear();
            _variables.Clear();
        }
        
        /*
         * Sets and gets a variable from the dictionary. Added to methods for future refactoring 
         */
        private void SetVariable(IdentifierNode identifier, IntNode value)
        {
            _variables[identifier.Identifier] = value;
        }

        private IntNode GetVariable(IdentifierNode identity)
        {
            try
            {
                return _variables[identity.Identifier];
            }
            catch (Exception)
            {
                throw new UndeclaredSymbolException(identity.Identifier + " has not been declared. ", identity.Identifier);
            }
        }
    }
}