using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopWeb.Models
{
    public class SellGoodsViewModel
    {
        public int goods_id { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "商品名称不能为空")]
        public string goods_name { set; get; }

        public string goods_tag { set; get; }

        public string goods_detail { set; get; }

        public string goods_img_path { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "商品价格不能为空")]
        [Display(Name ="商品价格")]
        [Range(0,double.MaxValue,ErrorMessage ="商品价格不正确")]
        public decimal goods_price { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "商品库存不能为空")]
        [Display(Name ="商品库存")]
        [Range(0,int.MaxValue,ErrorMessage ="商品库存不正确")]
        public int sell_stock { set; get; }

        public int sell_volume { set; get; }

        public PagedList.PagedList<MemberPurchaseListViewModel> goods_page_sell_list;

        public List<SellGoodsViewModel> total_sell_goods { set; get; }

        public List<GoodsTag> total_goods_tags { set; get; }

        public List<GoodsImgView> img_list { set; get; }
    }
}