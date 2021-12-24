using System;

namespace Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"5! = {Factorial(5)}");
        }

        public static int Factorial(int n)
        {
            if (n <= 0)
                return 1;
            return n * Factorial(n - 1);
        }
    }
}
