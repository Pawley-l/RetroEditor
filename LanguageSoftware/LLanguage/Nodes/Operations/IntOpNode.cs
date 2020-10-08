using System;
using LLanguage.Nodes.Types;

namespace LLanguage.Nodes.Operations
{
    /**
     * Holds the left and right values. And holds the operation type
     */
    public class IntOpNode : OperationNode<INode, INode, IntNode, IntNode, IntNode>
    {
        public enum Type
        {
            Add, Sub, Mul, Div
        }

        public Type OpType { get; set; }

        public IntOpNode(Type type, INode left, INode right) : base(left, right, "Integer Operation")
        { OpType = type; }

        public IntOpNode(Type type, INode left) : base(left, "Integer Operation")
        { OpType = type; }
        
        public IntOpNode(Type type) : base("Integer Operation")
        { OpType = type; }

        public IntOpNode() : base("Integer Operation")
        { }
        
        public override IntNode Execute(IntNode left, IntNode right)
        {
            return OpType switch
            {
                Type.Add => new IntNode(left.Value + right.Value),
                Type.Sub => new IntNode(left.Value - right.Value),
                Type.Mul => new IntNode(left.Value * right.Value),
                Type.Div => new IntNode(left.Value / right.Value),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}