using System.Collections.Generic;

namespace Interpreter.Expressions
{
    internal class TermExpression : AbstractExpression
    {

        public FactorExpression Factor { get; private set; }

        public TermExpression Term { get; private set; }

        public bool IsDivision { get; private set; }

        public override double Interpret()
        {
            if (Term is null)
            {
                return Factor.Interpret();
            }

            if (IsDivision)
            {
                return Factor.Interpret() / Term.Interpret();
            }

            return Factor.Interpret() * Term.Interpret();
        }

        public override void Parse(IList<char> input)
        {
            Factor = new FactorExpression();
            Factor.Parse(input);

            if (input.Count == 0)
            {
                return;
            }

            if (input[0] != '*' && input[0] != '/')
                return;

            if (input[0] == '/')
            {
                IsDivision = true;
            }

            input.RemoveAt(0);

            Term = new TermExpression();
            Term.Parse(input);
        }
    }
}
