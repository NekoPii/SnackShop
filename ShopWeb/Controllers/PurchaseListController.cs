using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class PurchaseListController : Controller
    {
        public static string savepath = @"E:\MJX\Language\C#\ShopWeb(Web)\ShopWeb\Content\image\";

        // GET: PurchaseList

        public ActionResult Index()
        {
            if (Session["has_login"] == null)
            {
                if (Session["ReturnToPurchaseList"] == null) Session["ReturnToPurchaseList"] = "true";
                else Session["ReturnToPurchaseList"] = null;
                return Redirect("/Login");
            }
            else
            {
                Session.Remove("ReturnToPurchaseList");
                string phone = Session["mem_phone"].ToString();
                string nowdir = savepath + phone + @"\";
                string nowpath1 = nowdir + @"alipay_coder.jpg";
                string nowpath2 = nowdir + @"weixin_coder.jpg";
                if (System.IO.File.Exists(nowpath1)) System.IO.File.Delete(nowpath1);
                if (System.IO.File.Exists(nowpath2)) System.IO.File.Delete(nowpath2);
                ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
                var p_list = memberPurchase.getPurchaseLists(phone).Select(p_info => new MemberPurchaseListViewModel()
                {
                    plist_id=p_info.plist_id,
                    goods_id = p_info.goods_id,
                    goods_name = p_info.goods_name,
                    goods_img_path=memberPurchase.getGoods(p_info.goods_id).goods_img_path,
                    goods_num = p_info.goods_num,
                    date=p_info.date,     
                    unit_price = p_info.unit_price,
                    total_price = p_info.total_price,
                }).ToList();
                var resView = new MemberPurchaseListViewModel()
                {
                    mem_phone = phone,
                    purchase_lists = p_list,
                };
                //return PartialView("PlistPart1", resView);
                return View(resView);
            }
        }
        
        [HttpPost]
        public ActionResult Search(MemberPurchaseListViewModel memberPurchaseListViewModel)
        {
            if(memberPurchaseListViewModel==null) return Redirect("/Error");
            string search_id = memberPurchaseListViewModel.search_id;
            string mem_phone = Session["mem_phone"].ToString();
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            if(search_id==null)
            {
                var p_list = memberPurchase.getPurchaseLists(mem_phone).Select(p_info => new MemberPurchaseListViewModel()
                {
                    plist_id = p_info.plist_id,
                    goods_id = p_info.goods_id,
                    goods_name = p_info.goods_name,
                    goods_img_path = memberPurchase.getGoods(p_info.goods_id).goods_img_path,
                    goods_num = p_info.goods_num,
                    date = p_info.date,
                    unit_price = p_info.unit_price,
                    total_price = p_info.total_price,
                }).ToList();
                var resView = new MemberPurchaseListViewModel()
                {
                    mem_phone = mem_phone,
                    purchase_lists = p_list,
                };
                return PartialView("PlistPart1", resView);
            }
            else
            {
                var p_list = memberPurchase.getPurchaseListsBySearchId(mem_phone,search_id).Select(p_info => new MemberPurchaseListViewModel()
                {
                    plist_id = p_info.plist_id,
                    goods_id = p_info.goods_id,
                    goods_name = p_info.goods_name,
                    goods_img_path = memberPurchase.getGoods(p_info.goods_id).goods_img_path,
                    goods_num = p_info.goods_num,
                    date = p_info.date,
                    unit_price = p_info.unit_price,
                    total_price = p_info.total_price,
                }).ToList();
                var resView = new MemberPurchaseListViewModel()
                {
                    mem_phone = mem_phone,
                    purchase_lists = p_list,
                };
                return PartialView("PlistPart1", resView);
            }
        }

        [HttpPost]
        public JsonResult SelectGoodsToBuy(PurchaseHomeTotalInfo purchaseHomeTotalInfo)
        {
            Session.Remove("ReturnToPurchaseCar");
            string mem_phone = Session["mem_phone"].ToString();
            var goods_items = purchaseHomeTotalInfo.selected_goods_list;
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            DateTime now_time = DateTime.Now;
            foreach(var goods in goods_items)
            {
                var plist_id = now_time.ToString("yyyyMMddHHmmssfff") + mem_phone;
                memberPurchase.addPurchaseLists(plist_id,mem_phone, goods.goods_id, goods.goods_num,now_time,goods.seller_phone);
                memberPurchase.deletePurchaseCar(mem_phone, goods.goods_id);
            }
            return Json("购买商品成功");
        }
    }
}