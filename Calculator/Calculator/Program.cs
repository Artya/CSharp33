using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            int previous;
            int current;
            string operation;

            while(true)
            {
                StartCalculation:
                while(true)
                {
                    Console.Write("Введите число: ");
                    var priv = Console.ReadLine();
                    try
                    {
                        previous = int.Parse(priv);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Это не число");
                    }
                }

                while(true)
                {
                    Console.Write("Введите операцию: ");
                    operation = Console.ReadLine();
                    if (operation == "!")
                    {
                        Console.WriteLine($"Факториал числа {previous} равен {Factorial(previous)}");
                        goto StartCalculation;
                    }
                    if (operation == "+" || operation == "-" || operation == "*" || operation == "/" || operation == "^")
                        break;
                    else
                        Console.WriteLine("Неверная операция, доступны только: + - * / ^ !");
                }

                while (true)
                {
                    Console.Write("Введите число: ");
                    var curr = Console.ReadLine();
                    try
                    {
                        current = int.Parse(curr);
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Это не число");
                    }
                }

                switch(operation)
                {
                    case "+":
                        Console.WriteLine($"{previous} + {current} = {previous + current}");
                        break;
                    case "-":
                        Console.WriteLine($"{previous} - {current} = {previous - current}");
                        break;
                    case "*":
                        Console.WriteLine($"{previous} * {current} = {previous * current}");
                        break;
                    case "/":
                        Console.WriteLine($"{previous} / {current} = {previous / current}");
                        break;
                    case "^":
                    	Console.WriteLine($"{previous} ^ {current} = {Math.Pow(previous, current)}");
                    	break;
                }
            }
        }

        static int Factorial(int n)
        {
            if (n <= 0)
                return 1;
            return n * Factorial(n - 1);
        }
    }
}
