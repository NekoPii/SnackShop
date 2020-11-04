using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopModel
{
    public class Goods
    {
        public int goods_id { set; get; }
        public string goods_name { set; get; }
        public float goods_price { set; get; }
        public string goods_tag { set; get; }
        public string goods_details { set; get; }
        public string goods_img_path { set; get; }
        public int goods_volume { set; get; }
        public int goods_stock { set; get; }
        public string seller_phone { set; get; }
    }
}
