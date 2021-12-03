using System;

public class SortingBy
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

    private static void ascendingSorting(Node node, ref List<double> list)
    {
        if (node.Left != null)
        {
            ascendingSorting(node.Left, ref list);
        }

        list.Add(node.Value);

        if (node.Right != null)
        {
            ascendingSorting(node.Right, ref list);
        }
    }

    private static void descendingSorting(Node node, ref List<double> list)
    {
        if (node.Right != null)
        {
            descendingSorting(node.Right, ref list);
        }

        list.Add(node.Value);

        if (node.Left != null)
        {
            descendingSorting(node.Left, ref list);
        }
    }


    public static double[] BinaryTree(double[] array, Direction direction)
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
                ascendingSorting(tree, ref sortedList);
                break;
            case Direction.descending:
                descendingSorting(tree, ref sortedList);
                break;
        }

        return sortedList.ToArray();
    }
}
