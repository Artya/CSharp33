using System;

namespace AnimalSorting
{
    class Program
    {
        static void Main(string[] args)
        {
            var animalsArray = new Animal[]
            {
                new Animal("g1", 87),
                new Animal("g2", 151),
                new Animal("g0", 100),
            };
            var animals = new Animals(animalsArray);

            Console.WriteLine("Unsorted array:");
            printAnimals(animals);
            Console.WriteLine();

            Console.WriteLine("Sorted by genus, ascending:");
            Array.Sort(animalsArray);
            printAnimals(animals);
            Console.WriteLine();

            Console.WriteLine("Sorted by genus, descending:");
            Array.Sort(animalsArray, Animal.SortByGenusDescending());
            printAnimals(animals);
            Console.WriteLine();

            Console.WriteLine("Sorted by weight, ascending:");
            Array.Sort(animalsArray, Animal.SortByWeightAscending());
            printAnimals(animals);
            Console.WriteLine();
        }

        private static void printAnimals(Animals animals)
        {
            foreach (var animal in animals)
                Console.WriteLine($"weight: {animal.Weight}, genus: {animal.Genus}");
        }
    }
}
