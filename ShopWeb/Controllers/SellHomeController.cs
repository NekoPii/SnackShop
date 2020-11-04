using ShopWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopWeb.Controllers
{
    public class SellHomeController : Controller
    {
        public static string savepath = @"E:\MJX\Language\C#\ShopWeb(Web)\ShopWeb\Content\image\";
        // GET: SellHome
        public ActionResult Index()
        {
            if (Session["mem_phone"] == null) return Redirect("/Login");
            if (Session["mem_phone"] != null && Convert.ToInt32(Session["mem_type"]) == 1) return Redirect("/SignUp/Seller");
            string phone = Session["mem_phone"].ToString();

            ShopBusinessLogic.SellerSell sellerSell = new ShopBusinessLogic.SellerSell();
            ShopBusinessLogic.MemberPurchase purchase = new ShopBusinessLogic.MemberPurchase();
            var sell_goods_list = sellerSell.getAllSellGoods(phone).Select(goods_info => new SellGoodsViewModel()
            {
                goods_id = goods_info.goods_id,
                goods_name = goods_info.goods_name,
                goods_tag=goods_info.goods_tag,
                goods_img_path = goods_info.goods_img_path,
                goods_price=goods_info.goods_price,
                sell_stock=goods_info.goods_stock,
                sell_volume=goods_info.goods_volume,
            }).ToList();
            var tag_list = purchase.getAllTags().Select(tag => new GoodsTag()
            {
                goods_tag = tag.tag,
            }).ToList();
            var resView = new SellGoodsViewModel()
            {
                 total_sell_goods=sell_goods_list,
                 total_goods_tags=tag_list,
            };
            return View(resView);
        }

        //上架商品
        [HttpPost]
        public ActionResult Index(SellGoodsViewModel sellGoodsViewModel)
        {
            ShopBusinessLogic.SellerSell sellerSell = new ShopBusinessLogic.SellerSell();
            ShopBusinessLogic.MemberPurchase purchase = new ShopBusinessLogic.MemberPurchase();

            
            string phone = Session["mem_phone"].ToString();
            int add_goods_id = sellerSell.getAddGoodsId();
            string nowpath = savepath+phone+@"\"+add_goods_id.ToString()+@"\";
            HttpPostedFileBase fileBase = Request.Files["image"];
            string file_name = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string img_path = "";
            if(fileBase.ContentType=="image/jpeg"|| fileBase.ContentType == "image/png")
            {
                if (int.Parse(fileBase.ContentLength.ToString()) > (4 * 1024 * 1024)) return Redirect("/SellHome");
                //图片存储路径
                if (!Directory.Exists(nowpath)) Directory.CreateDirectory(nowpath);
                fileBase.SaveAs(nowpath + file_name + ".jpg");
                img_path = "/content/image/"+phone+"/"+add_goods_id.ToString()+"/"+file_name + ".jpg";
            }


            string goods_name = sellGoodsViewModel.goods_name;
            float goods_price = sellGoodsViewModel.goods_price;
            string goods_detail = sellGoodsViewModel.goods_detail;
            string goods_tag = sellGoodsViewModel.goods_tag;
            string goods_img_path = img_path==""?"暂无": img_path;
            int sell_stock = sellGoodsViewModel.sell_stock;

            if (goods_detail == null) goods_detail = "暂无";
            if (sellerSell.addGoods(phone, goods_name, goods_tag, goods_price, goods_detail, sell_stock,goods_img_path))
            {

                var sell_goods_list = sellerSell.getAllSellGoods(phone).Select(goods_info => new SellGoodsViewModel()
                {
                    goods_id = goods_info.goods_id,
                    goods_name = goods_info.goods_name,
                    goods_tag = goods_info.goods_tag,
                    goods_img_path = goods_info.goods_img_path,
                    goods_price = goods_info.goods_price,
                    sell_stock = goods_info.goods_stock,
                    sell_volume = goods_info.goods_volume,
                }).ToList();
                var tag_list = purchase.getAllTags().Select(tag => new GoodsTag()
                {
                    goods_tag = tag.tag,
                }).ToList();
                var resView = new SellGoodsViewModel()
                {
                    total_sell_goods = sell_goods_list,
                    total_goods_tags = tag_list,
                };
                return Redirect("/SellHome");
            }
            else return Redirect("/SellHome");
        }

        [HttpPost]
        public ActionResult DeleteGoods(int deleteGoodsId)
        {
            string phone = Session["mem_phone"].ToString();
            ShopBusinessLogic.SellerSell sellerSell = new ShopBusinessLogic.SellerSell();
            ShopBusinessLogic.MemberPurchase purchase = new ShopBusinessLogic.MemberPurchase();

            sellerSell.deleteGoods(deleteGoodsId);

            string nowpath = savepath + phone + @"\" + deleteGoodsId.ToString();
            if (Directory.Exists(nowpath)) Directory.Delete(nowpath,true);

            var sell_goods_list = sellerSell.getAllSellGoods(phone).Select(goods_info => new SellGoodsViewModel()
            {
                goods_id = goods_info.goods_id,
                goods_name = goods_info.goods_name,
                goods_tag = goods_info.goods_tag,
                goods_img_path = goods_info.goods_img_path,
                goods_price = goods_info.goods_price,
                sell_stock = goods_info.goods_stock,
                sell_volume = goods_info.goods_volume,
            }).ToList();
            var tag_list = purchase.getAllTags().Select(tag => new GoodsTag()
            {
                goods_tag = tag.tag,
            }).ToList();
            var resView = new SellGoodsViewModel()
            {
                total_sell_goods = sell_goods_list,
                total_goods_tags = tag_list,
            };
            return PartialView("SellHomePart1", resView);
        }

    }
}