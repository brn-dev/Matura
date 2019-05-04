using System.Collections.Generic;

namespace Interpreter.Expressions
{
    internal class ExpressionsExpression : AbstractExpression
    {

        public TermExpression Term { get; private set; }

        public ExpressionsExpression Expression { get; private set; }

        public bool IsSubtraction { get; private set; }

        public override double Interpret()
        {
            if (Expression is null)
            {
                return Term.Interpret();
            }

            if (IsSubtraction)
            {
                return Term.Interpret() - Expression.Interpret();
            }

            return Term.Interpret() + Expression.Interpret();
        }

        public override void Parse(IList<char> input)
        {
            Term = new TermExpression();
            Term.Parse(input);

            if (input.Count == 0)
            {
                return;
            }

            if (input[0] != '-' && input[0] != '+')
                return;

            if (input[0] == '-')
            {
                IsSubtraction = true;
            }

            input.RemoveAt(0);

            Expression = new ExpressionsExpression();
            Expression.Parse(input);
        }
    }
}
