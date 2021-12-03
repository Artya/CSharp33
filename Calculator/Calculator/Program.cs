using System;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            int previous = default;
            int current = default;
            string operation = default;

            while(true)
            {
                var askFirstOperand = true;
                var askSecondOperand = true;
                var askOperation = true;
                
                while(askFirstOperand)
                {
                    Console.Write("Введите число: ");
                    var priv = Console.ReadLine();
                    try
                    {
                        previous = int.Parse(priv);
                        askFirstOperand = false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Это не число");
                    }
                }
                
                while(askOperation)
                {
                    Console.Write("Введите операцию: ");
                    operation = Console.ReadLine();
                    
                    if (operation == "!")
                    {
			askOperation = false;
			askSecondOperand = false;
                    }
                    else if (operation == "+" || operation == "-" || operation == "*" || operation == "/" || operation == "^")
                    {
                        askOperation = false;
                    }
                    else
                    {
                        Console.WriteLine("Неверная операция, доступны только: + - * / ^ !");
                    }
                }

                while (askSecondOperand)
                {
                    Console.Write("Введите число: ");
                    var curr = Console.ReadLine();
                    try
                    {
                        current = int.Parse(curr);
                        askSecondOperand = false;
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
                    case "!":
                    	Console.WriteLine($"{previous}! = {Factorial(previous)}");
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
