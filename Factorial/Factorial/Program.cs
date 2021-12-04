using System;

namespace Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter a number please:");

            var input = Console.ReadLine();
            var number = int.Parse(input);

            int result = 1;

            if (number > 1)
            {
                for (var index = number; index > 1; index--)
                {
                    result *= index;
                }
            }

            Console.WriteLine(number.ToString() + "! = " + result.ToString());
        }
    }
}
