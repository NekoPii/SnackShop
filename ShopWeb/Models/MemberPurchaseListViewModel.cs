using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWeb.Models
{
    public class MemberPurchaseListViewModel
    {
        public string search_id { set; get; }
        public string plist_id { set; get; }
        public string mem_phone { set; get; }
        public string goods_id { set; get; }
        public string goods_name { set; get; }
        public string goods_img_path { set; get; }
        public DateTime date { set; get; }
        public int goods_num { set; get; }
        public float unit_price { set; get; }
        public float total_price { set; get; }
        public List<MemberPurchaseListViewModel> purchase_lists { set; get; }
    }
}