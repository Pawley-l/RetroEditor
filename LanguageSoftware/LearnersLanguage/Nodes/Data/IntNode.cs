namespace LearnersLanguage.InternalTypes
{
    public class IntNode : INode
    {
        public int Number;

        public IntNode(int number)
        {
            Number = number;
        }
        
        public override string ToString()
        {
            return Number.ToString();
        }
    }
}