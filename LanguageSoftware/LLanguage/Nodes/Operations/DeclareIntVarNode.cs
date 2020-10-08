using LLanguage.Nodes.Static;
using LLanguage.Nodes.Types;

namespace LLanguage.Nodes.Operations
{
    /**
     * Declares a int by storing it for later recalling
     */
    public class DeclareIntVarNode : OperationNode<INode, INode, ReferenceNode, IntNode, IntNode>
    {
        public DeclareIntVarNode(INode left, INode right) : base(left, right, "Declare Int")
        { }
        
        public DeclareIntVarNode(INode left) : base(left, "Declare Int")
        { }

        public override IntNode Execute(ReferenceNode left, IntNode right)
        {
            right?.SetVariable(left.Value, right.Value);
            return right;
        }
    }
}