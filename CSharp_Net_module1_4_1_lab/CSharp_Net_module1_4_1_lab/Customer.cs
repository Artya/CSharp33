using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_4_1_lab
{
    class Customer
    {
        private string name;

        public Customer(string customerName)
        {
            this.name = customerName;
        }

        public void GotNewGoods(object sender, GoodsInfoEventArgs eventArgs)
        {
            var shop = (OnlineShop)sender;
            Console.WriteLine($"Customer {this.name} got new good {eventArgs.GoodsName} from shop {shop.Name}");
        } 
    }
}
