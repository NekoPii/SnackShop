using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopModel
{
    public class Goods
    {
        public string goods_id { set; get; }
        public string goods_name { set; get; }
        public float goods_price { set; get; }
        public string goods_details { set; get; }
        public string goods_img_path { set; get; }
        public string goods_small_img_path { set; get; }
    }
}
