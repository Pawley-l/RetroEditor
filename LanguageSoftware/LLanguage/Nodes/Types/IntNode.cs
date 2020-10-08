using System;
using System.Collections.Generic;
using LLanguage.Exceptions;

namespace LLanguage.Nodes.Types
{
    /**
     * Holds a integer value
     */
    public class IntNode : DataNode<int>, IVariableNode<int>
    {
        private static Dictionary<string, int> _stored = new Dictionary<string, int>();
        private static Dictionary<string, int> _storedbackbuffer = new Dictionary<string, int>();

        public IntNode(int value) : base(value, "Integer")
        { }

        public IntNode() : base("Integer")
        { }

        /*
         * Create int with the variable as its value
         */
        public IntNode(string reference): base("Integer")
        {
            Value = GetVariable(reference);
        }

        public int GetVariable(string reference)
        {
            try
            {
                return _stored[reference];
            }
            catch (Exception)
            {
                throw new UndeclaredSymbolException(reference + " has not been declared. "
                    , reference);
            }
        }

        public void SetVariable(string reference, int value)
        {
            _stored[reference] = value;
        }

        public void StoreVariable(string reference)
        {
            SetVariable(reference, Value);
        }

        public static void SwapBuffers(bool clear, bool forward)
        {
            if (forward)
            {
                _storedbackbuffer = _stored;
            }
            else
            {
                _stored = _storedbackbuffer;
            }
            if (clear) 
                _stored.Clear();
        }
    }
}