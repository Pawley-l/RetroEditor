using System.Collections.Generic;

namespace LearnersLanguage.Nodes.Features
{
    public class LoopNode : INode
    {
        public INode ForValue;
        public List<INode> Body;
        
        public LoopNode(INode forValue)
        {
            ForValue = forValue;
        }
    }
}