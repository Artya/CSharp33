using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_4_1_lab
{
    class GoodsInfoEventArgs : EventArgs
    {
        public string GoodsName { get; private set; }

        public GoodsInfoEventArgs(string goodsName)
        {
            this.GoodsName = goodsName;
        }
    }
}
