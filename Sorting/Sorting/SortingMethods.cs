using System.Collections.Generic;

namespace Sorting
{
    public class SortingMethods
    {
        public enum Direction
        {
            ascending,
            descending
        }
        private class Node
        {
            public Node Left { get; set; }
            public Node Right { get; set; }
            public double Value { get; set; }
            public void Add(double value)
            {
                if (this.Value > value)
                {
                    if (this.Left == null)
                    {
                        this.Left = new Node { Value = value };
                    }
                    else
                    {
                        this.Left.Add(value);
                    }
                }
                else
                {
                    if (this.Right == null)
                    {
                        this.Right = new Node { Value = value };
                    }
                    else
                    {
                        this.Right.Add(value);
                    }
                }
            }
        }

        private static void ascendingTreeSorting(Node node, ref List<double> list)
        {
            if (node.Left != null)
            {
                ascendingTreeSorting(node.Left, ref list);
            }

            list.Add(node.Value);

            if (node.Right != null)
            {
                ascendingTreeSorting(node.Right, ref list);
            }
        }

        private static void descendingTreeSorting(Node node, ref List<double> list)
        {
            if (node.Right != null)
            {
                descendingTreeSorting(node.Right, ref list);
            }

            list.Add(node.Value);

            if (node.Left != null)
            {
                descendingTreeSorting(node.Left, ref list);
            }
        }


        public static double[] BinaryTreeSort(double[] array, Direction direction)
        {
            var tree = new Node();
            tree.Value = array[0];

            for (int index = 1; index < array.Length; index++)
            {
                tree.Add(array[index]);
            }

            var sortedList = new List<double>();

            switch (direction)
            {
                case Direction.ascending:
                    ascendingTreeSorting(tree, ref sortedList);
                    break;
                case Direction.descending:
                    descendingTreeSorting(tree, ref sortedList);
                    break;
            }

            return sortedList.ToArray();
        }

        public static double[] InsertionSort(double[] array, Direction direction)
        {
            switch (direction)
            {
                case Direction.ascending:
                    {
                        for (int i = 0; i < array.Length; i++)
                        {
                            var temp = array[i];
                            var countIndex = i;

                            while(countIndex > 0 && array[countIndex - 1] > temp)
                            {
                                array[countIndex] = array[countIndex - 1];
                                countIndex--;
                            }

                            array[countIndex] = temp;
                        }
                    }
                    break;
                case Direction.descending:
                    {
                        for (int i =0; i < array.Length; i++)
                        {
                            var temp = array[i];
                            var countIndex = i;

                            while (countIndex > 0 && array[countIndex-1] < temp)
                            {
                                array[countIndex] = array[countIndex - 1];
                                countIndex--;
                            }

                            array[countIndex] = temp;
                        }
                    }
                    break;
                default:
                    break;
            }
            return array;
        }

        public static double[] BubleSort(double[] array, Direction direction)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        var temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
            switch (direction)
            {
                case Direction.ascending:
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        for (int j = 0; j < array.Length - i - 1; j++)
                        {
                            if (array[j] > array[j + 1])
                            {
                                var temp = array[j];
                                array[j] = array[j + 1];
                                array[j + 1] = temp;
                            }
                        }
                    }
                    break;
                case Direction.descending:
                    for (int i = 0; i < array.Length - 1; i++)
                    {
                        for (int j = 0; j < array.Length - i - 1; j++)
                        {
                            if (array[j] < array[j + 1])
                            {
                                var temp = array[j];
                                array[j] = array[j + 1];
                                array[j + 1] = temp;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            return array;
        }

    }
}
