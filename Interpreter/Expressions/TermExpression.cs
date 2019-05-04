using System.Collections.Generic;

namespace Interpreter.Expressions
{
    internal class TermExpression : AbstractExpression
    {

        public PowerExpression Power { get; private set; }

        public TermExpression Term { get; private set; }

        public bool IsDivision { get; private set; }

        public override double Interpret()
        {
            if (Term is null)
            {
                return Power.Interpret();
            }

            if (IsDivision)
            {
                return Power.Interpret() / Term.Interpret();
            }

            return Power.Interpret() * Term.Interpret();
        }

        public override void Parse(IList<char> input)
        {
            Power = new PowerExpression();
            Power.Parse(input);

            if (input.Count == 0)
            {
                return;
            }

            if (input[0] != '*' && input[0] != '/')
            {
                return;
            }

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
