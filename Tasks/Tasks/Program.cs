using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenSource = new CancellationTokenSource();
            var cancellationToken = tokenSource.Token;
            cancellationToken.Register(Helper.FactorialCancellationRequested);

            try
            {
                var factorialTask1 = Task.Run(
                    () => Helper.FactorialWithCancellationToken(5, cancellationToken),
                    cancellationToken);

                factorialTask1.ContinueWith(
                    _ => Console.WriteLine($"Factorial for number 5 is {factorialTask1.Result}"),
                    TaskContinuationOptions.OnlyOnRanToCompletion);

                factorialTask1.ContinueWith(
                    _ => Console.WriteLine($"Factorial calculating is aborted"),
                    TaskContinuationOptions.NotOnRanToCompletion);

                Console.Write("Cancel factorial calculating? [y/n]: ");
                var key = Console.ReadKey().Key;
                Console.WriteLine();

                if (key == ConsoleKey.Y)
                    tokenSource.Cancel();

                factorialTask1.Wait();
            }
            catch { }

            var factorialTask2 = Helper.FactorialAsync(6);
            Console.WriteLine($"Factorial for number 6 is {factorialTask2.Result}");
        }
    }
}
