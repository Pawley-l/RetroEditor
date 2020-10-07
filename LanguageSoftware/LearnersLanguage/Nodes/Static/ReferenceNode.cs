namespace LearnersLanguage.Nodes.Static
{
    /**
     * Holds a reference which is used to reference a value or function
     */
    public class ReferenceNode : DataNode<string>
    {
        public ReferenceNode(string value) : base(value, "Reference")
        { }
        
        public ReferenceNode() : base("Reference")
        { }
    }
}