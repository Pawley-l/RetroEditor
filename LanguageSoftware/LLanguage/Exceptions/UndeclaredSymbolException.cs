using System;

namespace LLanguage.Exceptions
{
    public class UndeclaredSymbolException : Exception
    {
        public int Line;
        public string Statement;
        public string Identifier;
        
        public UndeclaredSymbolException(string statement, string identifier)
        {
            Statement = statement;
            Identifier = identifier;
        }
    }
}