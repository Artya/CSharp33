using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    public static class Helper
    {
        public static void FactorialCancellationRequested()
        {
            Console.WriteLine("Cancellation requested");
        }

        public static async Task<int> FactorialAsync(int number)
        {
            return await Task.Run(
                () => Factorial(number));
        }

        public static int FactorialWithCancellationToken(int number, CancellationToken cancellationToken)
        {
            Thread.Sleep(3000); // simulating long work
            cancellationToken.ThrowIfCancellationRequested();

            return Factorial(number);
        }

        public static int Factorial(int number)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException("number cannot be less than zero");

            if (number == 0)
                return 1;

            return number * Factorial(number - 1);
        }
    }
}
