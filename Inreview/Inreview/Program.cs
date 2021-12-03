using System;
using System.Collections;

//Implement method IncrementX that would increase Struct.X by 1 for every item of a list created by CreateList method.
//Taking into account performance.
//You can change Struct providing it remains a struct?
namespace Rextester
{
    public interface IStruct
    {
        int X { get; set; }
    }
    public struct Struct : IStruct
    {
        public int X { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var list = CreateList();
            IncrementX(list);

            foreach (Struct item in list)
                Console.WriteLine(item.X);
        }

        private static ArrayList CreateList()
        {
            var list = new ArrayList();
            for (var index = 0; index < 10; index++)
            {
                var obj = new Struct() { X = 10 };
                list.Add(obj);
            }

            return list;
        }
        private static void IncrementX(ArrayList list)
        {
            for (int index = 0; index < 10; index++)
            {
                var obj = (IStruct)list[index];
                obj.X += 1;
            }
        }
    }
}