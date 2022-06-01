using System;

namespace UnsafeCode
{
    class Program
    {
        unsafe static void Main(string[] args)
        {
            var number = 5;
            var pointerToNumber = &number;

            Console.WriteLine(PointerOperation.Power(pointerToNumber, 3));

            PointerOperation.NullableInt = null;
            PointerOperation.SetAsNull();

            var bytes = PointerOperation.ConvertToByte(301);

            foreach (var b in bytes)
                Console.Write($"{b} ");

            Console.WriteLine();
        }
    }
}
