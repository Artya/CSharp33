using System;
using System.Collections.Generic;
using System.Threading;

namespace Multithreading
{
    public static class ThreadManipulator
    {
        private static object lockObject = new object();

        public static void AddingOne(object argument)
        {
            try
            {
                if (argument is not int divisibleBy)
                    return;

                for (var i = 0; i < 100; i++)
                {
                    if (i % divisibleBy == 0)
                    {
                        lock (lockObject)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine($"Divisible by {divisibleBy}, current value - {i}");
                            Console.ResetColor();
                        }

                        Thread.Sleep(500);
                    }
                }
            }
            catch (ThreadInterruptedException) { }
        }

        public static void AddingCustomValue(object arguments)
        {
            try
            {
                if (arguments is not int[] parameters)
                    return;

                if (parameters.Length != 2)
                    return;

                var divisibleBy = parameters[0];
                var step = parameters[1];

                for (var i = 0; i < 100; i += step)
                {
                    if (i % divisibleBy == 0)
                    {
                        lock (lockObject)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine($"Divisible by {divisibleBy}, current value - {i}");
                            Console.ResetColor();
                        }

                        Thread.Sleep(500);
                    }
                }
            }
            catch (ThreadInterruptedException) { }
        }

        public static void Stop(object arguments)
        {
            if (arguments is not Thread[] threads)
                return;

            var stopped = false;

            while (!stopped)
            {
                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.Q:
                        threads[0].Interrupt();
                        threads[1].Interrupt();
                        break;
                    case ConsoleKey.W:
                        threads[2].Interrupt();
                        break;
                    case ConsoleKey.Escape:
                        stopped = true;
                        break;
                }
            }
        }
    }
}
