using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWeb.Models
{
    public class PurchaseHomeTotalInfo
    {
        public string mem_phone { set; get; }
        public string pcar_goods_id { set; get; }
        public int pcar_goods_num { set; get; }
        public string wish_goods_id { set; get; }

        public string now_goods_id { set; get; }
        public string now_goods_name { set; get; }
        public float now_goods_unit_price { set; get; }
        public string now_goods_detail { set; get; }

        public List<MemberWishListViewModel> wish_lists { set; get; }
        public List<MemberPurchaseCarViewModel> pcar_list { set; get; }
        public List<MemberPurchaseCarViewModel> selected_goods_list { set; get; }
        public List<MemberPurchaseCarViewModel> total_goods_list { set; get; }
        public List<GoodsImgView> now_img_lists { set; get; }
    }
}