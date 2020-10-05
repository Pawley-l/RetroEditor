using System.Collections.Generic;

namespace LearnersLanguage.Nodes.Func
{
    public class ConditionalNode : INode
    {
        public INode Left;
        public INode Right;
        public List<INode> Body;
    }
}