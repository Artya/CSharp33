using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_2_3_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            // 10) declare 2 objects
            var uah1 = new Money(CurrencyTypes.UAH, 120.5);
            Console.WriteLine("Created uah1: " + uah1);

            var uah2 = new Money(CurrencyTypes.UAH, 12.38);
            Console.WriteLine("Created uah2: " + uah2);

            // 11) do operations
            // add 2 objects of Money
            var uah3 = uah1 + uah2;
            Console.WriteLine("uah3 = uah1 + uah2 = " + uah3);

            // add 1st object of Money and double
            var uah4 = uah3 + 17.35;
            Console.WriteLine("uah4 = uah3 + 17.35 =" + uah4);

            // decrease 2nd object of Money by 1 
            uah2--;
            Console.WriteLine("uah2-- = " + uah2);

            // increase 1st object of Money
            var uah5 = uah1 * 3;
            Console.WriteLine("uah5 = uah1 * 3 = " + uah5);

            // compare 2 objects of Money
            Console.WriteLine("uah1 > uah2 =" + (uah1 > uah2).ToString());
            Console.WriteLine("uah1 < uah2 =" + (uah1 < uah2).ToString());

            if (uah1)
            {
                Console.WriteLine("uah1 true");
            }

            var USD = new Money(CurrencyTypes.USD, 100);

            if (USD)
            {
                Console.WriteLine("USD true");
            }
            else
                Console.WriteLine("USD false");

            // compare 2nd object of Money and string // I don`t understand logic of this task
            // check CurrencyType of every object // yes of course

            // convert 1st object of Money to string
            string strUah = uah1;
            Console.WriteLine("implicit string uah1 = " + strUah);

            double dUah2 = uah2;
            Console.WriteLine("implicit double uah2 = " + dUah2);

            var doubleMoney = (Money)27.29;
            Console.WriteLine("Explicit double to money  " + doubleMoney.ToString());

            var uahToUsdCourse = 1 / 27.29;
            var convertedToUSD = Money.ConvertCurrency(doubleMoney, CurrencyTypes.USD, uahToUsdCourse);
            Console.WriteLine($"Double money {doubleMoney} converted to USD by course {uahToUsdCourse} = {convertedToUSD}");

            var USD1 = (Money)"3697,48 USD";
            Console.WriteLine("Explicit string to Money 3697,48 USD = " + USD1);

            var EUR = (Money)"EU 4627,12";
            Console.WriteLine("Explicit string to Money EU 4627,12 = " + EUR);

            Console.WriteLine(@"// error convert: var EUR2 = (Money)""EU46 27, 12"";");
        }
    }
}
