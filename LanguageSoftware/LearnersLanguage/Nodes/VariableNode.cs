using System.Collections.Generic;
using LearnersLanguage.Nodes.Static;

namespace LearnersLanguage.Nodes
{
    /**
     * For any type which will need to be stored on runtime
     */
    public interface IVariableNode<T> : INode
    {
        T GetVariable(string reference);
        void SetVariable(string reference, T value);
        void StoreVariable(string reference);
    }
}