

namespace LearnersLanguage.Nodes
{
    /**
     * <summary>
     * Node representation for any type of value that needs to be manipulated/used in script.
     * It's only purpose is to hold the data.
     * </summary>
     */
    public class DataNode<T> : INode
    {
        public T Value { get; set; }
        protected string ValueType;
        
        public DataNode(T value, string valueType) : this(valueType)
        {
            Value = value;
        }
        
        /**
         * Forcing all children classes to define a valueType
         */
        public DataNode(string valueType)
        {
            ValueType = valueType;
        }

        /*
         * Automatic string from its type
         */
        public override string ToString()
        {
            return ValueType + ":"+ Value;
        }
    }
}