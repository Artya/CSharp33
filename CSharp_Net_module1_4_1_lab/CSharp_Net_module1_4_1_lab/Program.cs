using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_4_1_lab
{
    class Program
    {
        static void Main(string[] args)
        {
            // 9) declare object of OnlineShop 
            var eldorado = new OnlineShop("eldorado.ua");
            var rozetka = new OnlineShop("ROZETKA");

            eldorado.NewGoods("supergood");
            rozetka.NewGoods("supergood");

            // 10) declare several objects of Customer
            var vasia = new Customer("Vasia");
            var galia = new Customer("Galia");
            var petro = new Customer("Petro");
            var ira = new Customer("Ira");
            var apolinariy = new Customer("Apolinariy");

            // 11) subscribe method GotNewGoods() of every Customer instance 
            // to event NewGoodsInfo of object of OnlineShop
            eldorado.NewGoodsHandler += vasia.GotNewGoods;
            eldorado.NewGoodsHandler += galia.GotNewGoods;
            eldorado.NewGoodsHandler += petro.GotNewGoods;
            eldorado.NewGoodsHandler += ira.GotNewGoods;
            eldorado.NewGoodsHandler += apolinariy.GotNewGoods;

            rozetka.NewGoodsHandler += vasia.GotNewGoods;
            rozetka.NewGoodsHandler += petro.GotNewGoods;
            rozetka.NewGoodsHandler += ira.GotNewGoods;

            // 12) invoke method NewGoods() of object of OnlineShop
            // discuss results
            eldorado.NewGoods("supergood");
            rozetka.NewGoods("supergood");

        }
    }
}
