using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWeb.Models
{
    public class PurchaseHomeTotalInfo
    {
        public string mem_phone { set; get; }
        public int pcar_goods_id { set; get; }
        public int pcar_goods_num { set; get; }
        public int wish_goods_id { set; get; }

        public int now_goods_id { set; get; }
        public string now_goods_name { set; get; }
        public decimal now_goods_unit_price { set; get; }
        public string now_goods_detail { set; get; }
        public string now_goods_tag { set; get; }

        public int now_stock { set; get; }
        public int now_volume { set; get; }
        public string now_seller_phone { set; get; }
        public string now_seller_name { set; get; }

        public string search_content { set; get; }

        public string search_tag { set; get; }
        public string search_price { set; get; }

        public string select_price_interval { set; get; }
        public string select_tag { set; get; }

        public List<MemberWishListViewModel> wish_lists { set; get; }
        public List<MemberPurchaseCarViewModel> pcar_list { set; get; }
        public List<MemberPurchaseCarViewModel> selected_goods_list { set; get; }
        public List<MemberPurchaseCarViewModel> total_goods_list { set; get; }
        public List<MemberPurchaseCarViewModel> rand_display_goods_list { set; get; }
        public List<MemberPurchaseCarViewModel> ai_display_goods_list { set; get; }
        public List<MemberPurchaseCarViewModel> tag_goods_list { set; get; }
        public List<GoodsImgView> now_img_lists { set; get; }
        public List<GoodsTag> goods_tags { set; get; }
        public PagedList.PagedList<MemberPurchaseCarViewModel> page_goods_list;
    }
}