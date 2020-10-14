using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class PurchaseCarController : Controller
    {
        // GET: PurchaseCar
        public ActionResult Index()
        {
            if (Session["has_login"] == null)
            {
                if (Session["ReturnToPurchaseCar"] == null) Session["ReturnToPurchaseCar"] = "true";
                else Session["ReturnToPurchaseCar"] = null;
                return Redirect("/Login");
            }
            else
            {
                Session.Remove("ReturnToPurchaseCar");
                ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
                var pcar_list = memberPurchase.getPurchaseCarList(Session["mem_phone"].ToString()).Select(pcar_info => new MemberPurchaseCarViewModel()
                {
                    goods_id = pcar_info.goods_id,
                    goods_num = pcar_info.goods_num,
                    goods_name = memberPurchase.getGoods(pcar_info.goods_id).goods_name,
                    goods_img_path=memberPurchase.getGoods(pcar_info.goods_id).goods_img_path,
                    unit_price = memberPurchase.getGoods(pcar_info.goods_id).goods_price,
                    total_price = memberPurchase.getGoods(pcar_info.goods_id).goods_price * pcar_info.goods_num,
                }).ToList();
                var resView = new MemberPurchaseCarViewModel()
                {
                    mem_phone = Session["mem_phone"].ToString(),
                    pcar_list = pcar_list,
                };
                return View(resView);
            }
        }

        //用于从购物车页删除商品
        [HttpPost]
        public ActionResult  Index(string DeletePcarId)
        {
            Session.Remove("ReturnToPurchaseCar");
            string mem_phone = Session["mem_phone"].ToString();
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            memberPurchase.deletePurchaseCar(mem_phone, DeletePcarId);
            var pcar_list = memberPurchase.getPurchaseCarList(Session["mem_phone"].ToString()).Select(pcar_info => new MemberPurchaseCarViewModel()
            {
                goods_id = pcar_info.goods_id,
                goods_num = pcar_info.goods_num,
                goods_name = memberPurchase.getGoods(pcar_info.goods_id).goods_name,
                goods_img_path = memberPurchase.getGoods(pcar_info.goods_id).goods_img_path,
                unit_price = memberPurchase.getGoods(pcar_info.goods_id).goods_price,
                total_price = memberPurchase.getGoods(pcar_info.goods_id).goods_price * pcar_info.goods_num,
            }).ToList();
            var resView = new MemberPurchaseCarViewModel()
            {
                mem_phone = Session["mem_phone"].ToString(),
                pcar_list = pcar_list,
            };
            return View(resView);
        }

        //用于从主页添加至购物车
        [HttpPost]
        public JsonResult AddCart(PurchaseHomeTotalInfo purchaseHomeTotalInfo)
        {
            Session.Remove("ReturnToPurchaseCar");
            string mem_phone = Session["mem_phone"].ToString();
            string goods_id = Request.Params["goods_id_to_cart"];
            int goods_num = purchaseHomeTotalInfo.pcar_goods_num;
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            if (memberPurchase.addPurchaseCar(mem_phone, goods_id, goods_num)) { }
            else
            {
                var now_goods_num = memberPurchase.getPurchaseCarItem(mem_phone, goods_id).goods_num;
                memberPurchase.updatePurchaseCar(mem_phone, goods_id, now_goods_num + goods_num);
            }
            return Json("添加该商品至购物车成功");
        }
    }
}