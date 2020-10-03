using LearnersLanguage.InternalTypes;

namespace LearnersLanguage.Nodes.Func
{
    public class DeclarerNode : INode
    {
        public INode Left;
        public INode Right;

        public DeclarerNode(INode left, INode right)
        {
            Left = left;
            Right = right;
        }
        
        public DeclarerNode(INode left)
        {
            Left = left;
        }
        
        public override string ToString()
        {
            return "[" + Left + ","+ "=" +"," + Right + "]";
        }
    }
}