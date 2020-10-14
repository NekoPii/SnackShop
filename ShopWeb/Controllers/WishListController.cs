using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class WishListController : Controller
    {
        // GET: WishList
        public ActionResult Index()
        {
            if (Session["has_login"] == null)
            {
                if (Session["ReturnToWishList"] == null) Session["ReturnToWishList"] = "true";
                else Session["ReturnToWishList"] = null;
                return Redirect("/Login");
            }
            else
            {
                Session.Remove("ReturnToWishList");
                ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
                var wish_list = memberPurchase.getWishLists(Session["mem_phone"].ToString()).Select(wish_info => new MemberWishListViewModel()
                {
                    goods_id=wish_info.goods_id,
                    goods_name=memberPurchase.getGoods(wish_info.goods_id).goods_name,
                    goods_unit_price=memberPurchase.getGoods(wish_info.goods_id).goods_price,
                    goods_img_path=memberPurchase.getGoods(wish_info.goods_id).goods_img_path,
                }).ToList();
                var resView = new MemberWishListViewModel()
                {
                    mem_phone = Session["mem_phone"].ToString(),
                    wish_lists=wish_list,
                };
                return View(resView);
            }
        }

        //用于主页更新心愿单增删
        [HttpPost]
        public JsonResult AddWish(PurchaseHomeTotalInfo purchaseHomeTotalInfo)
        {
            Session.Remove("ReturnToWishList");
            string mem_phone = Session["mem_phone"].ToString();
            string goods_id = purchaseHomeTotalInfo.wish_goods_id;
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            if (memberPurchase.addWishList(mem_phone, goods_id)) { }
            else
            {
                memberPurchase.deleteWishList(mem_phone, goods_id);
            }
            /*memberPurchase.addWishList(mem_phone, goods_id);*/
            return Json("心愿单更新成功");
        }

        //更新心愿单，主要是在心愿单页删除心愿商品
        [HttpPost]
        public ActionResult Index(string DeleteWishId)
        {
            Session.Remove("ReturnToWishList");
            string mem_phone = Session["mem_phone"].ToString();
            string goods_id = DeleteWishId;
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            memberPurchase.deleteWishList(mem_phone, goods_id);
            var wish_list = memberPurchase.getWishLists(Session["mem_phone"].ToString()).Select(wish_info => new MemberWishListViewModel()
            {
                goods_id = wish_info.goods_id,
                goods_name = memberPurchase.getGoods(wish_info.goods_id).goods_name,
                goods_unit_price = memberPurchase.getGoods(wish_info.goods_id).goods_price,
                goods_img_path = memberPurchase.getGoods(wish_info.goods_id).goods_img_path,
            }).ToList();
            var resView = new MemberWishListViewModel()
            {
                mem_phone = Session["mem_phone"].ToString(),
                wish_lists = wish_list,
            };
            return PartialView("WishListPart1",resView);
        }

    }
}