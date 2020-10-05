using System;

namespace LearnersLanguage.Exceptions
{
    public class SyntaxErrorException : Exception
    {
        public int Line;
        public string ErrorMessage;
        
        public SyntaxErrorException(string error)
        {
            ErrorMessage = error;
        }
    }
}