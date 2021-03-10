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
            Console.ReadLine();
        }
    }
}
