namespace LearnersLanguage.Nodes
{
    /**
     * All operations are when something gets applied to the next thing. For example, adding two integers together
     */
    public abstract class OperationNode<Tl, Tr, TEr, TEl, TE> : INode
    {
        public Tl Left { get; set; }
        public Tr Right { get; set; }
        
        /*
         * Storing the operation type in string format for easy logging
         */
        protected string _operation_type;
        
        protected OperationNode(Tl left, Tr right, string operation) : this(left, operation)
        { Right = right; }

        protected OperationNode(Tl left, string operation) : this(operation)
        { Left = left; }

        protected OperationNode(string operation)
        { _operation_type = operation; }

        public abstract TE Execute(TEr left, TEl right);

        public override string ToString()
        {
            return "[" + Left + "," + _operation_type + "," + Right + "]";
        }
    }
}