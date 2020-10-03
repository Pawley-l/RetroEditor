using System.Linq;
using LearnersLanguage.InternalTypes;

namespace LearnersLanguage.Nodes.Func
{
    public class FuncCallNode : INode
    {
        public INode Identifier;
        public INode[] Parameters;


        public FuncCallNode(INode identifier, INode[] parameters)
        {
            Identifier = identifier;
            Parameters = parameters;
        }
        
        public override string ToString()
        {
            var info = "[func," + Identifier + ", ";
            info = Parameters.Aggregate(info, (current, parameter) => current + parameter);
            return info + "]";
        }
    }
}