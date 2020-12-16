using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWeb.Models
{
    public class MemberPurchaseCarViewModel
    {
        public string mem_phone { set; get; }
        public string seller_phone { set; get; }
        public int goods_id { set; get; }
        public string goods_name { set; get; }
        public string goods_img_path { set; get; }
        public decimal unit_price { set; get; }
        public decimal total_price { set; get; }
        public int goods_num { set; get; }
        public int sell_stock { set; get; }
        public int sell_volume { set; get; }
        public List<MemberPurchaseCarViewModel> pcar_list { set; get; }
        public List<MemberPurchaseCarViewModel> select_list { set; get; }
    }
}