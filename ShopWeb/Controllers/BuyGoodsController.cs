using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class BuyGoodsController : Controller
    {
        // GET: BuyGoods
        public ActionResult Index()
        {
            if (ControllerContext.RouteData.GetRequiredString("id")==null) return Redirect("/Home");
            //if (ControllerContext.RouteData.GetRequiredString("id") == null) return HttpNotFound();
            else
            {
                string now_goods_id = ControllerContext.RouteData.GetRequiredString("id");
                ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
                var goods_list = memberPurchase.getGoodsList().Select(goods_info => new MemberPurchaseCarViewModel()
                {
                    goods_id = goods_info.goods_id,
                    goods_name = goods_info.goods_name,
                    goods_img_path = goods_info.goods_img_path,
                    unit_price = goods_info.goods_price,
                }).ToList();
                var now_img_list = memberPurchase.getGoodsImgs(now_goods_id).Select(img_info => new GoodsImgView()
                {
                    img_path = img_info.img_path,
                }).ToList();
                var resView = new PurchaseHomeTotalInfo()
                {
                    now_goods_id = now_goods_id,
                    now_goods_name = memberPurchase.getGoods(now_goods_id).goods_name,
                    now_goods_unit_price = memberPurchase.getGoods(now_goods_id).goods_price,
                    now_goods_detail = memberPurchase.getGoods(now_goods_id).goods_details,
                    total_goods_list = goods_list,
                    now_img_lists = now_img_list,
                };
                return View(resView);
            }
        }
    }
}