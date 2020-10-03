namespace LearnersLanguage.Nodes.Data
{
    /**
     * Holds a identifier which is used to reference a value or function
     */
    public class IdentifierNode : INode
    {
        public string Identifier;

        public IdentifierNode(string identifier)
        {
            Identifier = identifier;
        }
        
        public override string ToString()
        {
            return "ID:"+Identifier;
        }
    }
}