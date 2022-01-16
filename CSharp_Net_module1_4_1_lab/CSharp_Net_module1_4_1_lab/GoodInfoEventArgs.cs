using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Net_module1_4_1_lab
{
    // 1) inherit EventArgs
    class GoodsInfoEventArgs: EventArgs
    {
        // 2) declare property GoodsName
        // think about get and set attributes
        public string GoodsName { get; private set; }

        // 3) declare constructor to initialize GoodsName
        public GoodsInfoEventArgs(string goodsName)
        {
            this.GoodsName = goodsName;
        }       
    }
}
