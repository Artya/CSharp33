using System;
using System.Collections;

namespace CSharp_Net_module1_4_3_lab
{
    public class Animal : IComparable
    {
        public static IComparer SortWeightAscending
        {
            get
            {
                return new SortWeightAscendingHelper();
            }
        }

        public static IComparer SortGenusDescending
        {
            get
            {
                return new SortGenusDescendingHelper();
            }
        }

        public string Genus { get; }
        public int Weight { get; }

        public Animal(string genus, int weight)
        {
            this.Genus = genus;
            this.Weight = weight;
        }

        public int CompareTo(object obj)
        {
            var animal = (Animal)obj;

            return string.Compare(this.Genus, animal.Genus);
        }

        private class SortWeightAscendingHelper : IComparer
        {
            public int Compare(object x, object y)
            {
                var left = (Animal)x;
                var right = (Animal)y;

                return left.Weight - right.Weight;
            }
        }

        private class SortGenusDescendingHelper : IComparer
        {
            public int Compare(object x, object y)
            {
                var left = (Animal)x;
                var right = (Animal)y;

                return String.Compare(right.Genus, left.Genus);
            }
        }
    }
}
