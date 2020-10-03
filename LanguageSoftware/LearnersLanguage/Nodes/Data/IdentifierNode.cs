namespace LearnersLanguage.InternalTypes
{
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