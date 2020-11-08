using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using PagedList;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class SellGoodsController : Controller
    {
        public static string savepath = @"E:\MJX\Language\C#\ShopWeb(Web)\ShopWeb\Content\image\";

        // GET: SellGoods
        public ActionResult Index()
        {
            if (Session["mem_phone"] == null) return Redirect("/Login");
            if (Session["mem_phone"] != null && Convert.ToInt32(Session["mem_type"]) == 1) return Redirect("/SignUp/Seller");
            string phone = Session["mem_phone"].ToString();
            string now_goods_id = ControllerContext.RouteData.GetRequiredString("id");
            int now_goods_id_int = Convert.ToInt32(now_goods_id);
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            ShopBusinessLogic.SellerSell sellerSell = new ShopBusinessLogic.SellerSell();
            ShopBusinessLogic.MemberPurchase purchase = new ShopBusinessLogic.MemberPurchase();
            var now_goods = sellerSell.getSellGoods(phone, now_goods_id_int);
            var now_img_list = purchase.getGoodsImgs(now_goods_id_int).Select(img_info => new GoodsImgView()
            {
                img_path = img_info.img_path,
            }).ToList();
            var tag_list = purchase.getAllTags().Select(tag => new GoodsTag()
            {
                goods_tag = tag.tag,
            }).ToList();
            var sell_list = sellerSell.getPerGoodsSellList(phone, now_goods_id_int).Select(p_list => new MemberPurchaseListViewModel()
            {
                plist_id=p_list.plist_id,
                goods_num=p_list.goods_num,
                mem_phone=p_list.mem_phone,
                mem_name=loginMember.GetMemberByPhone(p_list.mem_phone).mem_name,
                unit_price=p_list.unit_price,
                total_price=p_list.total_price,
                date=p_list.date,
                goods_name=p_list.goods_name,
            }).ToList();
            var resView = new SellGoodsViewModel()
            {
                goods_id = now_goods_id_int,
                goods_detail = now_goods.goods_details,
                goods_name = now_goods.goods_name,
                goods_price = now_goods.goods_price,
                goods_img_path = now_goods.goods_img_path,
                goods_tag = now_goods.goods_tag,
                sell_stock = now_goods.goods_stock,
                sell_volume = now_goods.goods_volume,
                img_list = now_img_list,
                total_goods_tags = tag_list,
                perGoodsSellList=sell_list,
            };
            return View(resView);
        }

        //修改信息
        [HttpPost]
        public ActionResult Index(SellGoodsViewModel sellGoodsViewModel)
        {
            string phone = Session["mem_phone"].ToString();
            int goods_id = sellGoodsViewModel.goods_id;
            string goods_name = sellGoodsViewModel.goods_name;
            float goods_price = sellGoodsViewModel.goods_price;
            string goods_detail = sellGoodsViewModel.goods_detail;
            string goods_tag = sellGoodsViewModel.goods_tag;
            int sell_stock = sellGoodsViewModel.sell_stock;

            if (goods_detail == null) goods_detail = "暂无";

            ShopBusinessLogic.SellerSell sellerSell = new ShopBusinessLogic.SellerSell();
            ShopBusinessLogic.MemberPurchase purchase = new ShopBusinessLogic.MemberPurchase();
            if (sellerSell.modifyGoods(phone, goods_id, goods_name, goods_tag, goods_price, goods_detail, sell_stock))
            {
                var now_goods = sellerSell.getSellGoods(phone, goods_id);
                var tag_list = purchase.getAllTags().Select(tag => new GoodsTag()
                {
                    goods_tag = tag.tag,
                }).ToList();
                var now_img_list = purchase.getGoodsImgs(goods_id).Select(img_info => new GoodsImgView()
                {
                    img_path = img_info.img_path,
                }).ToList();
                var resView = new SellGoodsViewModel()
                {
                    goods_id = goods_id,
                    goods_detail = now_goods.goods_details,
                    goods_name = now_goods.goods_name,
                    goods_price = now_goods.goods_price,
                    goods_img_path = now_goods.goods_img_path,
                    goods_tag = now_goods.goods_tag,
                    sell_stock = now_goods.goods_stock,
                    sell_volume = now_goods.goods_volume,
                    img_list = now_img_list,
                    total_goods_tags = tag_list,
                };
                return Redirect("/SellGoods/Index/" +goods_id.ToString());
            }
            else return Redirect("/SellGoods/Index/" + goods_id.ToString());
        }

        //下架商品
        [HttpPost]
        public ActionResult DeleteGoods(int deleteGoodsId)
        {
            string phone = Session["mem_phone"].ToString();
            ShopBusinessLogic.SellerSell sellerSell = new ShopBusinessLogic.SellerSell();

            sellerSell.deleteGoods(deleteGoodsId);

            string nowpath = savepath + phone + @"\" + deleteGoodsId.ToString();
            if (Directory.Exists(nowpath)) Directory.Delete(nowpath, true);

            return Redirect("/SellHome");
        }

        //上传图片
        public ActionResult AddImg(int img_goods_id)
        {
            ShopBusinessLogic.SellerSell sellerSell = new ShopBusinessLogic.SellerSell();
            string phone = Session["mem_phone"].ToString();
            string nowpath = savepath + phone + @"\" + img_goods_id.ToString() + @"\";
            HttpPostedFileBase fileBase = Request.Files["image"];
            string file_name = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string img_path = "";
            if (fileBase.ContentType == "image/jpeg" || fileBase.ContentType == "image/png")
            {
                if (int.Parse(fileBase.ContentLength.ToString()) > (4 * 1024 * 1024)) return Redirect("/SellGoods/Index/"+img_goods_id.ToString());
                //图片存储路径
                if (!Directory.Exists(nowpath)) Directory.CreateDirectory(nowpath);
                fileBase.SaveAs(nowpath + file_name + ".jpg");
                img_path = "/content/image/" + phone + "/" + img_goods_id.ToString() + "/" + file_name + ".jpg";
                if (sellerSell.addImg(img_goods_id, img_path)) return Redirect("/SellGoods/Index/" + img_goods_id.ToString());
            }
            return Redirect("/SellGoods/Index/" + img_goods_id.ToString());
        }
    }
}