using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWeb.Models
{
    public class SellerInfoViewModel
    {
        public string sell_phone { set; get; }
        public string sell_name { set; get; }
        public string sell_count { set; get; }
        public string sell_address { set; get; }
        public float all_income { set; get; }
        public float now_month_income { set; get; }
        public List<MemberPurchaseListViewModel> sell_list { set; get; }
    }
}