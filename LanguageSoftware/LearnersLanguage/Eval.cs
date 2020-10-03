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
     * The evaluator is walks up every "branch" in the Abstract Syntax Tree and then executes everything in order.
     * In order to resolve a branch below, the evaluator has to execute the branch which comes from it first, to
     * get it's return.
     * </summary>
     */
    public class Eval
    {
        Dictionary<string, IntNode> _variables = new Dictionary<string, IntNode>();
        Dictionary<string, Func<List<IntNode>, IntNode>> _func = new Dictionary<string, Func<List<IntNode>, IntNode>>();
        
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
            return node switch
            {
                DeclarerNode declarerNode => ExecuteDeclareNode(declarerNode),
                OpNode opNode => ExecuteOpNode(opNode),
                IdentifierNode identity => GetVariable(identity),
                FuncCallNode call => ExecuteFunctionCall(call),
                IntNode intNode => intNode,
                _ => new IntNode(-1)
            };
        }
        
        /**
         * Resolves left and right values then executes itself turning into a IntNode
         */
        private INode ExecuteOpNode(OpNode node)
        {
            node.Right = Execute(node.Right);
            node.Left = Execute(node.Left);

            return node.OpType switch
            {
                OpNode.Type.Add => new IntNode(((IntNode) node.Left).Number + ((IntNode) node.Right).Number),
                OpNode.Type.Sub => new IntNode(((IntNode) node.Left).Number - ((IntNode) node.Right).Number),
                OpNode.Type.Mul => new IntNode(((IntNode) node.Left).Number * ((IntNode) node.Right).Number),
                OpNode.Type.Div => new IntNode(((IntNode) node.Left).Number / ((IntNode) node.Right).Number),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        /**
         * Executes its equal first then sets the variable to it's value
         */
        private INode ExecuteDeclareNode(DeclarerNode node)
        {
            // Dont execute because it hasnt been declared yet
            var identifier = node.Left as IdentifierNode;
            node.Right = Execute(node.Right);

            if (identifier != null)
                SetVariable(identifier, node.Right as IntNode);
            return node.Right;
        }
        
        /**
         * Function calls are mapped to functions in _func. The identifier node identifies the method and then executes
         * the nodes as parameters before invoking the method.
         */
        private INode ExecuteFunctionCall(FuncCallNode node)
        {
            if (node.Identifier is IdentifierNode identity)
            {
                var parameters = new List<IntNode>();
                foreach (var parameter in node.Parameters)
                {
                    parameters.Add(Execute(parameter) as IntNode);
                }
                
                _func[identity.Identifier].DynamicInvoke(parameters);
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
            catch (Exception e)
            {
                throw new UndeclaredSymbolException(identity.Identifier + " has not been declared. ");
            }
        }
    }
}