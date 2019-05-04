using System;

namespace Interpreter.Expressions
{
    class UnexpectedTokenException : Exception
    {
        public UnexpectedTokenException() 
            : base("Unexpected Token")
        {
        }

        public UnexpectedTokenException(string token) 
            : base($"Unexpected Token '{token}'")
        {
        }

        public UnexpectedTokenException(string token, string expected) 
            : base($"Unexpected Token '{token}', expected {expected}")
        {
        }
    }
}
