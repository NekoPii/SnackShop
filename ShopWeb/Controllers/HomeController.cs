using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using ShopWeb.Models;
using PagedList;

namespace ShopWeb.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(int? page)
        {
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            string search_tag = this.Request.QueryString["search_tag"];
            string search_price = this.Request.QueryString["search_price"];
            Regex regex1 = new Regex("^[0-9]*[1-9][0-9]*$");
            Regex regex2 = new Regex("^\\d+_\\d+$");
            if (search_price!=null&&!regex1.IsMatch(search_price) && !regex2.IsMatch(search_price)&&search_price!="all") return Redirect("/Error");
            var total_goods_list = memberPurchase.getGoodsList().Select(goods_info => new MemberPurchaseCarViewModel()
            {
                goods_id = goods_info.goods_id,
                goods_name = goods_info.goods_name,
                goods_img_path = goods_info.goods_img_path,
                unit_price = goods_info.goods_price,
                sell_stock = goods_info.goods_stock,
                sell_volume = goods_info.goods_volume,
                seller_phone = goods_info.seller_phone,
            }).ToList();
            var search_goods_list = new List<MemberPurchaseCarViewModel>();
            if ((search_price == null || search_price == "all" || search_price == "") && (search_tag == null || search_tag == "all" || search_tag == ""))
            {
                search_goods_list = memberPurchase.getGoodsList().Select(goods_info => new MemberPurchaseCarViewModel()
                {
                    goods_id = goods_info.goods_id,
                    goods_name = goods_info.goods_name,
                    goods_img_path = goods_info.goods_img_path,
                    unit_price = goods_info.goods_price,
                    sell_stock = goods_info.goods_stock,
                    sell_volume = goods_info.goods_volume,
                    seller_phone = goods_info.seller_phone,
                }).ToList();
            }
            else
            {
                search_goods_list = memberPurchase.getGoodsList(search_tag,search_price).Select(goods_info => new MemberPurchaseCarViewModel()
                {
                    goods_id = goods_info.goods_id,
                    goods_name = goods_info.goods_name,
                    goods_img_path = goods_info.goods_img_path,
                    unit_price = goods_info.goods_price,
                    sell_stock = goods_info.goods_stock,
                    sell_volume = goods_info.goods_volume,
                    seller_phone = goods_info.seller_phone,
                }).ToList();
            }
            int pageNumber = page ?? 1;
            int pageSize = 8;
            var tag_list = memberPurchase.getAllTags().Select(tag => new GoodsTag()
            {
                goods_tag = tag.tag,
            }).ToList();
            int _index;
            string tmp_select_price;
            if (search_price == "all"||search_price==null||search_price=="") tmp_select_price = "";
            else
            {
                _index = search_price.IndexOf('_');
                if (_index == -1)
                {
                    tmp_select_price = "￥" + search_price + " 以上";
                }
                else
                {
                    tmp_select_price = "￥" + search_price.Substring(0, _index) + " ~ ￥" + search_price.Substring(_index + 1, search_price.Length - _index - 1);
                }
            }        
            var resView = new PurchaseHomeTotalInfo()
            {
                select_price_interval=tmp_select_price,
                select_tag=(search_tag=="all"||search_tag==""||search_tag==null)?"":search_tag,
                total_goods_list = total_goods_list,
                goods_tags=tag_list,
                page_goods_list= (PagedList<MemberPurchaseCarViewModel>)search_goods_list.ToPagedList(pageNumber,pageSize),
            };
            return Request.IsAjaxRequest()? (ActionResult)PartialView("HomePart1", resView):View(resView);
        }

        public ActionResult SearchHome()
        {
            return View();
        }
        /*
        public ActionResult Search()
        {
            return View();
        }*/
        /*
        [HttpPost]
        public ActionResult Search(string search_content)
        {
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            var goods_list = memberPurchase.getGoodsList(search_content).Select(goods_info => new MemberPurchaseCarViewModel()
            {
                goods_id = goods_info.goods_id,
                goods_name = goods_info.goods_name,
                goods_img_path = goods_info.goods_img_path,
                unit_price = goods_info.goods_price,
                sell_stock = goods_info.goods_stock,
                sell_volume = goods_info.goods_volume,
                seller_phone = goods_info.seller_phone,
            }).ToList();
            var resView = new PurchaseHomeTotalInfo()
            {
                total_goods_list = goods_list,
                search_name=search_content,
            };
            return View(resView);
        }
        */

        [HttpGet]
        public ActionResult Search(int? page)
        {
            string search_content = this.Request.QueryString["search_content"];
            if (search_content == null) return Redirect("/Error");
            else if (search_content == ""||search_content.Trim()=="") return Redirect("/Home/SearchHome");
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            var goods_list = memberPurchase.getGoodsList(search_content).Select(goods_info => new MemberPurchaseCarViewModel()
            {
                goods_id = goods_info.goods_id,
                goods_name = goods_info.goods_name,
                goods_img_path = goods_info.goods_img_path,
                unit_price = goods_info.goods_price,
                sell_stock = goods_info.goods_stock,
                sell_volume = goods_info.goods_volume,
                seller_phone = goods_info.seller_phone,
            }).ToList();
            int pageNumber = page ?? 1;
            int pageSize = 8;
            var resView = new PurchaseHomeTotalInfo()
            {
                total_goods_list = goods_list,
                search_content = search_content,
                page_goods_list = (PagedList<MemberPurchaseCarViewModel>)goods_list.ToPagedList(pageNumber, pageSize),
            };
            return Request.IsAjaxRequest()? (ActionResult)PartialView("HomePart2", resView):View(resView);
        }
    }
}