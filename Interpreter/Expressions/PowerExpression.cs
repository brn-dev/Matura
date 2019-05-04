using System;
using System.Collections.Generic;

namespace Interpreter.Expressions
{
    class PowerExpression : AbstractExpression
    {

        public FactorExpression Factor { get; private set; }

        public PowerExpression Power { get; private set; }

        public override double Interpret()
        {
            return Power is null ? Factor.Interpret() : Math.Pow(Factor.Interpret(), Power.Interpret());
        }

        public override void Parse(IList<char> input)
        {
            Factor = new FactorExpression();
            Factor.Parse(input);

            if (input.Count == 0)
            {
                return;
            }

            if (input[0] != '^')
            {
                return;
            }

            input.RemoveAt(0);

            if (input.Count == 0)
            {
                throw new UnexpectedTokenException();
            }

            Power = new PowerExpression();
            Power.Parse(input);
        }
    }
}
