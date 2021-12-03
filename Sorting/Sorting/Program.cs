using System;
using System.Diagnostics;

namespace Sorting
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random();

            Console.Write("Please, enter the length of the array you want to form and sort: ");
            var arrayLength = int.Parse(Console.ReadLine());
            var originalArray = new double[arrayLength];
            for (int i = 0; i < arrayLength; i++)
            {
                originalArray[i] = rnd.Next(0, arrayLength);
            }

            double[] array = new double[arrayLength];

            originalArray.CopyTo(array, 0);
            
            Console.WriteLine();

            var stopwatch = new Stopwatch();


            stopwatch.Start();
            Array.Sort(array);
            stopwatch.Stop();

            Console.WriteLine("Array.Sort Sorting: " + stopwatch.ElapsedMilliseconds);

            originalArray.CopyTo(array, 0);

            var sortedArray = new double[arrayLength];
            stopwatch.Reset();
            stopwatch.Start();
            sortedArray = SortingMethods.BubleSort(array, SortingMethods.Direction.ascending);
            stopwatch.Stop();

            Console.WriteLine("Buble Sorting: " + stopwatch.ElapsedMilliseconds);

            originalArray.CopyTo(array, 0);

            stopwatch.Reset();
            stopwatch.Start();
            sortedArray = SortingMethods.InsertionSort(array, SortingMethods.Direction.ascending);
            stopwatch.Stop();

            Console.WriteLine("Insertion Sorting: " + stopwatch.ElapsedMilliseconds);

            originalArray.CopyTo(array, 0);

            stopwatch.Reset();
            stopwatch.Start();
            sortedArray = SortingMethods.BinaryTreeSort(array, SortingMethods.Direction.ascending);
            stopwatch.Stop();

            Console.WriteLine("Binary Tree Sorting: " + stopwatch.ElapsedMilliseconds);

            Console.WriteLine();
        }
    }
}
