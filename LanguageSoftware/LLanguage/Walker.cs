using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LLanguage.Exceptions;
using LLanguage.Nodes;
using LLanguage.Nodes.Features;
using LLanguage.Nodes.Operations;
using LLanguage.Nodes.Static;
using LLanguage.Nodes.Types;

namespace LLanguage
{
    /**
     * <summary>
     * The evaluator is walks up every "branch" in the Abstract Syntax Tree and then executes everything in order.
     * In order to resolve a branch below, the evaluator has to execute the branch which comes from it first, to
     * get it's return.
     *
     * For the majority of this class, the real execution happens in the individual node. However, for instances
     * where context is more important, there execution happens here. for example, when executing a method. 
     * </summary>
     *
     */
    public class Walker
    {
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
                    DeclareIntVarNode declarerNode => declarerNode.Execute(declarerNode.Left as ReferenceNode, 
                        Execute(declarerNode.Right) as IntNode),
                    IntOpNode opNode => opNode.Execute(Execute(opNode.Left) as IntNode, 
                        Execute(opNode.Right) as IntNode),
                    ReferenceNode identity => new IntNode(identity.Value),
                    FuncCallNode call => ExecuteFunctionCall(call),
                    IntNode intNode => intNode,
                    MethodNode method => ExecuteDeclareMethod(method),
                    LoopNode loop => ExecuteLoopNode(loop),
                    ConditionalNode conditional => ExecuteConditional(conditional),
                    _ => new IntNode(-1)
                };
            }
            catch (UndeclaredSymbolException)
            { throw; }
        }

        private INode ExecuteConditional(ConditionalNode node)
        {
            var right = Execute(node.Right) as IntNode;
            var left = Execute(node.Left) as IntNode;

            Debug.Assert(right != null, nameof(right) + " != null");
            Debug.Assert(left != null, nameof(left) + " != null");
            
            if (right.Value == left.Value)
            {
                Execute(node.Body);
            }

            return null;
        }
        
        /**
         * Function calls are mapped to functions in _func. The identifier node identifies the method and then executes
         * the nodes as parameters before invoking the method.
         */
        private INode ExecuteFunctionCall(FuncCallNode node)
        {
            if (!(node.Identifier is ReferenceNode identity)) return null;
            var parameters = node.Parameters.Select(parameter => Execute(parameter) as IntNode).ToList();

            if (_func.ContainsKey(identity.Value))
            {
                _func[identity.Value].DynamicInvoke(parameters);
                return null;
            }
                
            ExecuteMethod(identity, parameters);
            return null;
        }
        
        /**
         * Executes user defined method if it exists
         */
        private INode ExecuteMethod(ReferenceNode reference, List<IntNode> parameters)
        {
            var method = MethodNode.Find(reference);
            
            method.PrepareState();
                
            Execute(method.Execute(parameters, reference));
                
            method.ResetStateChanges();
            
            return null;
        }
        
        private INode ExecuteDeclareMethod(MethodNode method)
        {
            method.StoreVariable(method.Identifier.Value);
            return null;
        }

        private INode ExecuteLoopNode(LoopNode loop)
        {
            if (loop.ForValue is ReferenceNode id)
            {
                var count = new IntNode(id.Value).Value;

                for (var i = 0; i < count; i++)
                {
                    count = new IntNode(id.Value).Value;
                    
                    Execute(loop.Body);
                }
            }
            else if (loop.ForValue is IntNode value)
            {
                var count = value.Value;
                
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
        }
    }
}