using System.Collections.Generic;

namespace LearnersLanguage.Nodes.Features
{
    public class ConditionalNode : INode
    {
        public INode Left;
        public INode Right;
        public List<INode> Body;
    }
}