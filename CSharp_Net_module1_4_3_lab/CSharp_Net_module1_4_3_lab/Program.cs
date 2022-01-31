using System;
using System.Collections;

namespace CSharp_Net_module1_4_3_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            var animalsArray = new Animal[]
            {
                new Animal("Horse", 500),
                new Animal("Cat", 3),
                new Animal("Dog", 10),                
                new Animal("Elephant", 3000),
                new Animal("Wolf", 50)
            };

            var animals = new Animals(animalsArray);

            ShowAnimals(animals);

            foreach (var currentSortType in Enum.GetValues(typeof(SortTypes)))
            {
                var sortType = (SortTypes)currentSortType;

                Console.WriteLine($"Sorting in {sortType}");
                animals.Sort(sortType);
                ShowAnimals(animals);
            }
        }

        private static void ShowAnimals(Animals animals)
        {
            foreach (var currentAnimal in animals)
            {
                ShowAnimal((animal) => 
                {
                    Console.WriteLine($"{animal.Genus} - {animal.Weight}");
                },
                (Animal)currentAnimal);
            }

            Console.WriteLine("-------------------------------------");
        }

        private static void ShowAnimal(Action<Animal> action, Animal animal)
        {
            action(animal);
        }
    }
}
