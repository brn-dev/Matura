using System.Collections.Generic;

namespace Interpreter.Expressions
{
    internal class FactorExpression : AbstractExpression
    {
        public ExpressionsExpression Expression { get; set; }

        public double Number { get; set; }

        public override double Interpret()
        {
            return Expression?.Interpret() ?? Number;
        }

        public override void Parse(IList<char> input)
        {
            if (input[0] == '(')
            {
                input.RemoveAt(0);
                if (input.Count == 0)
                {
                    throw new UnexpectedTokenException();
                }

                Expression = new ExpressionsExpression();
                Expression.Parse(input);

                if (input[0] != ')')
                {
                    throw new UnexpectedTokenException(input[0].ToString(), "')'");
                }

                input.RemoveAt(0);
            }
            else
            {
                //var numberStr = "";

                //do
                //{
                //    numberStr += input[0];
                //    input.RemoveAt(0);
                //} while (input.Count > 0 && char.IsDigit(input[0]));

                //Number = double.Parse(numberStr);

                var numberStr = "";
                if (input[0] == '-')
                {
                    numberStr += '-';
                    input.RemoveAt(0);
                }
                if (input.Count == 0)
                {
                    throw new UnexpectedTokenException();
                }

                do
                {
                    numberStr += input[0];
                    input.RemoveAt(0);
                } while (input.Count > 0 && (char.IsDigit(input[0]) || input[0] == '.'));

                if (double.TryParse(numberStr, out double temp))
                {
                    Number = temp;
                }
                else
                {
                    throw new UnexpectedTokenException(numberStr);
                }
            }
        }
    }
}