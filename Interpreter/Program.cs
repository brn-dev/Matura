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
                expression.Parse(list);
                var result = expression.Interpret();
                Console.WriteLine(result);
            }
        }
    }
}
