using System;

namespace LearnersLanguage.InternalTypes
{
    public class OpNode : INode
    {
        public enum Type
        {
            Add, Sub, Mul, Div
        }

        public Type OpType;
        public INode Left;
        public INode Right;

        public OpNode(Type type, INode left, INode right)
        {
            OpType = type;
            Left = left;
            Right = right;
        }
        
        public OpNode(Type type, INode left)
        {
            OpType = type;
            Left = left;
        }

        private string TypetoStr(Type type)
        {
            switch (type)
            {
                case Type.Add:
                    return "ADD";
                case Type.Sub:
                    return "SUB";
                case Type.Mul:
                    return "MUL";
                case Type.Div:
                    return "DIV";
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        
        public override string ToString()
        {
            return "[" + Left + "," + TypetoStr(OpType) +"," + Right + "]";
        }
    }
}