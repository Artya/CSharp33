using System;

namespace SimpleCalculator
{
    
    public enum Operations
    {
        Multiplication = 1,
        Division,
        Addition,
        Subtraction,
        Exponentiation,
        Factorial
    }

                          
    class Program
    {
        public static double Calc(Operations operation, double operand1, double operand2 = 0)
        {
            var result = 1d;

            switch (operation)
            {
                case Operations.Multiplication: result = operand1 * operand2;
                    break;
                case Operations.Division: result = operand1 / operand2;
                    break;
                case Operations.Addition: result = operand1 + operand2;
                    break;
                case Operations.Subtraction: result = operand1 - operand2;
                    break;
                case Operations.Exponentiation: result = Math.Pow(operand1, operand2);
                    break;
                case Operations.Factorial:
                    {
                        if (operand1 != 0)
                        {
                            for (var i = operand1; i >= 2; i--)
                            {
                                result *= i;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            return result;
        }
        static void Main(string[] args)
        {
            var input = string.Empty;

            Console.WriteLine(@"Select the arifmetic operation, please:
1. Multiplication
2. Divide
3. Addition
4. Subtraction
5. Exponentiation
6. Factorial");

            var inputCorrect = false;
            var operation = new Operations();
           
            while (!inputCorrect)
            {
                input = Console.ReadLine();
                if (Enum.TryParse(input, out operation))
                {
                    if (((int)operation) >= 1 && ((int)operation) <= 6)
                    {
                        inputCorrect = true;
                    }
                    else
                    {
                        Console.Write($"There is no operation with number {input}. It should be number from 1 to 6. Check it and enter it again, please : ");
                    }
                }
                else
                {
                    Console.Write("Thomething wrong with value you entered. It should be number from 1 to 6. Check it and enter it again, please : ");
                }

            }

            switch (operation)
            {
                case Operations.Multiplication:

                case Operations.Division:

                case Operations.Addition:

                case Operations.Subtraction:
                     {
                        Console.Write("Please, enter the first operand: ");
                        input = Console.ReadLine();

                        var operand1 = 0d;

                        while (!double.TryParse(input, out operand1))
                        {
                            Console.Write("The value you entered is not a number. Check it and enter it again, please : ");
                            input = Console.ReadLine();
                        }

                        Console.Write("//n Now, please enter the second operand: ");
                        input = Console.ReadLine();

                        var operand2 = 0d;

                        while (!double.TryParse(input, out operand2))
                        {
                            Console.Write("The value you entered is not a number. Check it and enter it again, please : ");
                            input = Console.ReadLine();
                        }

                        Console.Write("The result of the operation is : ");
                        Console.WriteLine(Calc(operation, operand1, operand2));
                    }
                    break;

                case Operations.Exponentiation:
                    {
                        Console.Write("Please, enter the base: ");
                        input = Console.ReadLine();

                        var operand1 = 0d;

                        while (!double.TryParse(input, out operand1))
                        {
                            Console.Write("The value you entered is not a number. Check it and enter it again, please : ");
                            input = Console.ReadLine();
                        }

                        Console.Write("/n Now, please enter the power: ");
                        input = Console.ReadLine();

                        var operand2 = 0d;

                        while (!double.TryParse(input, out operand2))
                        {
                            Console.Write("The value you entered is not a number. Check it and enter it again, please : ");
                            input = Console.ReadLine();
                        }

                        Console.Write("The result of the operation is : ");
                        Console.WriteLine(Calc(operation, operand1, operand2));
                    }
                    break;
                case Operations.Factorial:
                    {
                        Console.Write("Please, enter the value for factorial: ");
                        input = Console.ReadLine();

                        int operand1 = 0;

                        while (!int.TryParse(input, out operand1))
                        {
                            Console.Write("The value you entered is not a number. Check it and enter it again, please : ");
                            input = Console.ReadLine();
                        }
                        Console.WriteLine("The result of the operation is :" );
                        Console.WriteLine(Calc(operation, operand1));
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
