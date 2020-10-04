using System.Collections.Generic;

namespace LearnersLanguage.Nodes.Func
{
    /**
     * Holds identifiers for parameters, identifier for itself and it's own AST which is executed when the method is called
     */
    public class MethodNode : INode
    {
        public INode Identifier; // Identifier for method
        public INode[] Parameters; // Identifier Node for parameters
        public List<INode> Body; // AST to be executed

        public MethodNode()
        {
            
        }
        
        public MethodNode(INode identifier, INode[] parameters, List<INode> body)
        {
            Identifier = identifier;
            Parameters = parameters;
            Body = body;
        }
    }
}