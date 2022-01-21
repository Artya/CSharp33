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
            var eldorado = new OnlineShop("eldorado.ua");
            var rozetka = new OnlineShop("ROZETKA");

            eldorado.NewGoods("supergood");
            rozetka.NewGoods("supergood");

            var vasia = new Customer("Vasia");
            var galia = new Customer("Galia");
            var petro = new Customer("Petro");
            var ira = new Customer("Ira");
            var apolinariy = new Customer("Apolinariy");

            eldorado.NewGoodsHandler += vasia.GotNewGoods;
            eldorado.NewGoodsHandler += galia.GotNewGoods;
            eldorado.NewGoodsHandler += petro.GotNewGoods;
            eldorado.NewGoodsHandler += ira.GotNewGoods;
            eldorado.NewGoodsHandler += apolinariy.GotNewGoods;

            rozetka.NewGoodsHandler += vasia.GotNewGoods;
            rozetka.NewGoodsHandler += petro.GotNewGoods;
            rozetka.NewGoodsHandler += ira.GotNewGoods;

            eldorado.NewGoods("supergood");
            rozetka.NewGoods("supergood");
        }
    }
}
