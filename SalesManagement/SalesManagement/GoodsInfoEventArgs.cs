using System;

namespace SalesManagement
{
    public class GoodsInfoEventArgs : EventArgs
    {
        public string GoodsName { get; private set; }

        public GoodsInfoEventArgs(string goodsName)
        {
            if (goodsName == null)
                throw new ArgumentNullException("goodsName can't be null.");

            this.GoodsName = goodsName;
        }
    }
}
