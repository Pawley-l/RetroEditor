namespace LearnersLanguage.Nodes.Data
{
    /**
     * Holds a integer value
     */
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