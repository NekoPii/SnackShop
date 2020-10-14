using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWeb.Models
{
    public class MemberWishListViewModel
    {
        public string mem_phone { set; get; }
        public string goods_id { set; get; }
        public string goods_name { set; get; }
        public string goods_img_path { set; get; }
        public float goods_unit_price { set; get; }
        public List<MemberWishListViewModel> wish_lists { set; get; }
    }
}