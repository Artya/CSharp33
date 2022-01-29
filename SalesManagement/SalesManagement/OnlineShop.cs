using System;

namespace SalesManagement
{
    public class OnlineShop
    {
        public event EventHandler<GoodsInfoEventArgs> OnNewGoods;

        public void NewGoods(string goodsName)
        {
            if (this.OnNewGoods == null)
                throw new InvalidOperationException("Event OnNewGoods must have subscribers.");

            this.OnNewGoods(this, new GoodsInfoEventArgs(goodsName));
        }
    }
}
