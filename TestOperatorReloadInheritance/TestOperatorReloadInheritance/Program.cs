using System;

namespace TestOperatorReloadInheritance
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var left = new MyBaseClass("left");
            var right = new MyBaseClass("right");
            var result = left + right;
            Console.WriteLine(result.Name);

            var leftDerived = new MyDerivedClass("derived left");
            var rightDerived = new MyDerivedClass("derived right");
            var derivedResult = leftDerived + rightDerived;
            Console.WriteLine(derivedResult.Name);

            var leftSecondDerived = new MySecondDerived ("second derived left");
            var rightSecondDerived = new MySecondDerived("second derived right");
            var secondDerivedResult = leftSecondDerived + rightSecondDerived;
            Console.WriteLine(secondDerivedResult.Name);

        }
    }
}
