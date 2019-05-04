using System.Collections.Generic;

namespace Interpreter.Expressions
{
    internal abstract class AbstractExpression
    {
        public abstract double Interpret();

        public abstract void Parse(IList<char> input);
    }
}
