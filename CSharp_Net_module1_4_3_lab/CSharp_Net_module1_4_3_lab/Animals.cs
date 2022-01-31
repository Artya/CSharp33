using System;
using System.Collections;

namespace CSharp_Net_module1_4_3_lab
{
    class Animals : IEnumerable
    {
        private Animal[] animals;
        public int Length { get => animals.Length; }

        public Animal this[int index]
        {
            get
            {
                if (index < 0 || index > animals.Length)
                    throw new IndexOutOfRangeException($"Index {index} is out of range");

                return animals[index];
            }
        }

        public Animals(Animal[] inputAnimals)
        {
            this.animals = inputAnimals;
        }

        public void Sort(SortTypes sortType)
        {
            switch (sortType)
            {
                case SortTypes.WeightAscending:
                    Array.Sort(this.animals, Animal.SortWeightAscending);
                    break;

                case SortTypes.GenusDescending:
                    Array.Sort(this.animals, Animal.SortGenusDescending);
                    break;

                default:
                    Array.Sort(this.animals);
                    break;
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var currentAnimal in this.animals)
            {
                yield return currentAnimal;
            }
        }
    }
}
