using System;

namespace SimpleCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter first operand, please");

            var input = Console.ReadLine();
            var operand1 = int.Parse(input);
            
            Console.WriteLine(@"Select the arithmetic operation:
1. Multiplication
2. Divide
3. Addition
4. Subtraction
5. Exponentiation");

            input = Console.ReadLine();
            var operation = (operations)int.Parse(input);

            Console.WriteLine("Enter second operand, please");
            input = Console.ReadLine();
            var operand2 = int.Parse(input);

            double result = 0;
            string operationSymbol = "";

            switch (operation)
            {
                case operations.Multiplication:
                    result = operand1 * operand2;
                    operationSymbol = " * ";
                    break;

                case operations.Divide:

                    if (operand2 == 0)
                    {
                        Console.WriteLine("Divivding by zero is impossible");
                        return;
                    }

                    result = operand1 / operand2;
                    operationSymbol = " / ";
                    break;

                case operations.Addition:
                    result = operand1 + operand2;
                    operationSymbol = " + ";
                    break;
                case operations.Subtraction:
                    result = operand1 - operand2;
                    operationSymbol = " - ";
                    break;
                case operations.Exponentiation:
                    operationSymbol = " ^ ";
                    if (operand2 > 0)
                        result = operand1;

                    for (int index = 2; index <= operand2; index++)
                        result *= operand1;
                    break;
            }

            Console.WriteLine("" + operand1.ToString() + operationSymbol + operand2.ToString() + " = " + result.ToString());

        }
    }
}
