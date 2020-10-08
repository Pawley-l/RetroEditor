using System.Collections.Generic;

namespace LLanguage.Nodes.Features
{
    public class ConditionalNode : INode
    {
        public INode Left;
        public INode Right;
        public List<INode> Body;
    }
}