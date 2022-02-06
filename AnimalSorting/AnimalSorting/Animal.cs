using System;
using System.Collections;
using System.Collections.Generic;

namespace AnimalSorting
{
    public class Animal : IComparable<Animal>
    {
        public string Genus { get; private set; }
        public int Weight { get; private set; }

        public Animal(string genus, int weight)
        {
            if (genus == null)
                throw new ArgumentNullException("genus can't be null.");

            this.Genus = genus;
            this.Weight = weight;
        }

        public int CompareTo(Animal obj)
        {
            return this.Genus.CompareTo(obj.Genus);
        }

        public static IComparer SortByWeightAscending()
        {
            return new SortByWeightAscendingHelper();
        }
        public static IComparer SortByGenusDescending()
        {
            return new SortByGenusDescendingHelper();
        }

        class SortByWeightAscendingHelper : IComparer<Animal>, IComparer
        {
            public int Compare(Animal left, Animal right)
            {
                if (left == null)
                    throw new ArgumentNullException("left animal can't be null.");

                if (right == null)
                    throw new ArgumentNullException("right animal can't be null.");

                return left.Weight.CompareTo(right.Weight);
            }

            public int Compare(object x, object y)
            {
                if (x is Animal left && y is Animal right)
                    return this.Compare(left, right);

                throw new InvalidOperationException("One of objects was not type of animal.");
            }
        }
        class SortByGenusDescendingHelper : IComparer<Animal>, IComparer
        {
            public int Compare(Animal left, Animal right)
            {
                if (left == null)
                    throw new ArgumentNullException("left animal can't be null.");

                if (right == null)
                    throw new ArgumentNullException("right animal can't be null.");

                return right.Genus.CompareTo(left.Genus);
            }

            public int Compare(object x, object y)
            {
                if (x is Animal left && y is Animal right)
                    return this.Compare(left, right);

                throw new InvalidOperationException("One of objects was not type of animal.");
            }
        }
    }
}
