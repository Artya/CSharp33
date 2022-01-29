using System;

namespace SalesManagement
{
    public class Customer
    {
        public readonly string Name;

        public Customer(string name)
        {
            if (name == null)
                throw new ArgumentNullException("Name can't be null.");

            this.Name = name;
        }

        public void GotNewGoods(object sender, GoodsInfoEventArgs eventArgs)
        {
            Console.WriteLine($"{this.Name} got new goods: {eventArgs.GoodsName}");
        }
    }
}
