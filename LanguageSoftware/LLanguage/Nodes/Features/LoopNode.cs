using System.Collections.Generic;

namespace LLanguage.Nodes.Features
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