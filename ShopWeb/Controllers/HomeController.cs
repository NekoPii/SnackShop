using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWeb.Models;
using PagedList;

namespace ShopWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page)
        {
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            var goods_list = memberPurchase.getGoodsList().Select(goods_info => new MemberPurchaseCarViewModel()
            {
                goods_id = goods_info.goods_id,
                goods_name = goods_info.goods_name,
                goods_img_path = goods_info.goods_img_path,
                unit_price = goods_info.goods_price,
                sell_stock=goods_info.goods_stock,
                sell_volume=goods_info.goods_volume,
                seller_phone=goods_info.seller_phone,
            }).ToList();
            int pageNumber = page ?? 1;
            int pageSize = 8;
            
            var resView = new PurchaseHomeTotalInfo()
            {
                total_goods_list = goods_list,
                page_goods_list= (PagedList<MemberPurchaseCarViewModel>)goods_list.ToPagedList(pageNumber,pageSize),
            };
            return View(resView);
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
            return View(resView);
        }
    }
}