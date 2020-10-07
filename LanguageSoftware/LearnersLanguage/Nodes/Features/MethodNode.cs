using System.Collections.Generic;
using LearnersLanguage.Exceptions;
using LearnersLanguage.Nodes.Static;
using LearnersLanguage.Nodes.Types;

namespace LearnersLanguage.Nodes.Features
{
    /**
     * Holds identifiers for parameters, identifier for itself and it's own AST which is executed when the method is called
     */
    public class MethodNode : OperationNode<List<INode>, List<INode>, List<IntNode>, ReferenceNode, List<INode>>, 
        IVariableNode<MethodNode>
    {
        public ReferenceNode Identifier; // Identifier for method
        /*
         * Left - The methods parameters
         * Right - The methods body
         */

        private static List<MethodNode> _stored = new List<MethodNode>();

        public MethodNode(List<INode> left, List<INode> right) : base(left, right, "Executing Method")
        { }

        public MethodNode(List<INode> left) : base(left, "Executing Method")
        { }

        public MethodNode() : base("Executing Method") 
        { }
        
        public override List<INode> Execute(List<IntNode> left, ReferenceNode right)
        {
            for (var i = 0; i < left.Count; i++)
            {
                if (Left[i] is ReferenceNode id)
                    left[i].StoreVariable(id.Value);
            }

            return Right;
        }

        public static MethodNode Find(ReferenceNode reference)
        {
            foreach (var method in _stored)
            {
                if (!(method.Identifier is { } nodeid)) continue;
                if (nodeid.Value != reference.Value) continue;

                return method;
            }

            return null;
        }

        public void PrepareState()
        {
            IntNode.SwapBuffers(false, true);
        }

        public void ResetStateChanges()
        {
            IntNode.SwapBuffers(false, false);
        }

        public MethodNode GetVariable(string reference)
        {
            foreach (var method in _stored)
            {
                if (!(method.Identifier is { } nodeid)) continue;
                if (nodeid.Value != reference) continue;

                return method;
            }this

            throw new SyntaxErrorException(reference + " has not been defined");
        }

        public void SetVariable(string reference, MethodNode value)
        {
            _stored.Add(value);
        }

        public void StoreVariable(string reference)
        {
            _stored.Add(this);
        }
    }
}