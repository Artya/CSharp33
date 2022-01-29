using System;

namespace SalesManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            var citrus = new OnlineShop();

            var petya = new Customer("Petya");
            var vasya = new Customer("Vasya");
            var maksim = new Customer("Maksim");

            citrus.OnNewGoods += petya.GotNewGoods;
            citrus.OnNewGoods += vasya.GotNewGoods;
            citrus.OnNewGoods += maksim.GotNewGoods;

            citrus.NewGoods("IPhone 18 512TB");
        }
    }
}
