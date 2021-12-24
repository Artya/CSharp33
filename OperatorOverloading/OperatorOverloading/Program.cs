using System;

namespace OperatorOverloading
{
    class Program
    {
        static void Main(string[] args)
        {
            var moneyObj1 = new Money(100, CurrencyType.USD);
            var moneyObj2 = new Money(200, CurrencyType.UAN);

	    moneyObj1 *= 2;

            moneyObj1++;
            moneyObj2--;

            Console.WriteLine(@$"Comparing two objects of money 
    (first object: {moneyObj1.Amount}, {moneyObj1.CurrencyType} ==
    second object: {moneyObj2.Amount}, {moneyObj2.CurrencyType}) = {moneyObj1 == moneyObj2}");
            const string stringToCompare = "199";
            Console.WriteLine(@$"Comparing 2nd object of money and string
    (2nd object of money (amout: {moneyObj2.Amount}, currency: {moneyObj2.CurrencyType}) == 
    string {stringToCompare}) = {moneyObj2 == (Money)stringToCompare}");

            Console.WriteLine($"Currency type of first object: {moneyObj1.CurrencyType}");
            Console.WriteLine($"Currency type of second object: {moneyObj2.CurrencyType}");

            Console.WriteLine($"Converting first object to string: {(string)moneyObj1}");
        }
    }
}
