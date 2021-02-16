using System;

namespace Exo5_Externe
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
                Console.WriteLine($"<HTML><BODY>Hello (from external) {args[0]}</BODY></HTML>");
            if (args.Length == 2)
                Console.WriteLine($"<HTML><BODY>Hello (from external) {args[0]} and {args[1]}</BODY></HTML>");
            else
                Console.WriteLine("Exo5_External <string parameter>");
        }
    }
}
