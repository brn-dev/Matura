using System;
using System.Linq;
using Interpreter.Expressions;

namespace Interpreter
{
    internal class Program
    {
        private static void Main()
        {
            while (true)
            {
                var input = Console.ReadLine();
                var list = input.ToCharArray().ToList();
                var expression = new ExpressionsExpression();
                try
                {
                    expression.Parse(list);
                }
                catch(UnexpectedTokenException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                var result = expression.Interpret();
                Console.WriteLine(result);
            }
        }
    }
}
