using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWeb.Models
{
    public class MemberPurchaseCarViewModel
    {
        public string mem_phone { set; get; }
        public string goods_id { set; get; }
        public string goods_name { set; get; }
        public string goods_img_path { set; get; }
        public string goods_small_img_path { set; get; }
        public float unit_price { set; get; }
        public float total_price { set; get; }
        public int goods_num { set; get; }
        public List<MemberPurchaseCarViewModel> pcar_list { set; get; }
        public List<MemberPurchaseCarViewModel> select_list { set; get; }
    }
}