using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD4
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator.CalculatorSoap calculator = new Calculator.CalculatorSoapClient();
            Console.WriteLine(calculator.Add(11, 11));

            MathsOperations.IMathsOperations mathsOp = new MathsOperations.MathsOperationsClient();
            Console.WriteLine(mathsOp.Add(11, 11));
            Console.WriteLine(mathsOp.Subtract(11, 5));
            Console.WriteLine(mathsOp.Multiply(10, 11));
            Console.WriteLine(mathsOp.Divide(10, 5));

            Console.ReadLine();
        }
    }
}
