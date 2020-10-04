namespace LearnersLanguage.Nodes.Func
{
    /**
     * Holds the identifier and the thing its equal to.
     */
    public class DeclareVarNode : INode
    {
        public INode Left;
        public INode Right;

        public DeclareVarNode(INode left, INode right)
        {
            Left = left;
            Right = right;
        }
        
        public DeclareVarNode(INode left)
        {
            Left = left;
        }
        
        public override string ToString()
        {
            return "[" + Left + ","+ "=" +"," + Right + "]";
        }
    }
}