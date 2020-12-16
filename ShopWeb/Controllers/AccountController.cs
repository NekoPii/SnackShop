using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWeb.Models;
using System.Drawing;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using QRCoder;

namespace ShopWeb.Controllers
{
    public class myQRCoder
    {
        public static string path = @"E:\MJX\Language\C#\ShopWeb(Web)\ShopWeb\Content\image\";
        //生成二维码
        public static Bitmap QRCodeEncoderUtil(string qrCodeContent,string pay_type)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeContent, QRCodeGenerator.ECCLevel.Q);
            QRCode qrcode = new QRCode(qrCodeData);
            //中间小图图标
            Bitmap icon = new Bitmap(path+pay_type+"Square.png");
            if(pay_type=="alipay")
            {
                Bitmap img = qrcode.GetGraphic(30, Color.Black, Color.White, icon, 30, 6, false);
                return img;
            }
            if(pay_type=="weixin")
            {
                Bitmap img = qrcode.GetGraphic(30, Color.Black, Color.White, icon, 30, 6, false);
                return img;
            }
            return null;
        }
    }

    public class AccountController : Controller
    {
        public static string savepath = @"E:\MJX\Language\C#\ShopWeb(Web)\ShopWeb\Content\image\";

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult order_fail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Pay(AccountModels accountModels)
        {
            if (Session["mem_phone"] == null) return Redirect("/Login");
            string phone = Session["mem_phone"].ToString();
            string nowdir = savepath + phone + @"\";
            string nowpath1 = nowdir + @"alipay_coder.jpg";
            string nowpath2 = nowdir + @"weixin_coder.jpg";
            if (!Directory.Exists(nowdir)) Directory.CreateDirectory(nowdir);
            if (System.IO.File.Exists(nowpath1)) System.IO.File.Delete(nowpath1);
            if (System.IO.File.Exists(nowpath2)) System.IO.File.Delete(nowpath2);
            if (Session["has_pay"] != null && accountModels.address_text != null)
            {
                Session.Remove("has_pay");
                for (int i = 0; i < accountModels.accountModeList.Count; ++i)
                {
                    accountModels.all_price += accountModels.accountModeList[i].total_price;
                }
                accountModels.all_price = Math.Round(10*(accountModels.all_price / 100 + accountModels.all_price))/10;
                string qrText = "用户" + Session["mem_name"].ToString() + "已支付" + accountModels.all_price + "元";
                Bitmap mybitmap1 = myQRCoder.QRCodeEncoderUtil(qrText,"alipay");
                Bitmap mybitmap2 = myQRCoder.QRCodeEncoderUtil(qrText,"weixin");
                mybitmap1.Save(nowpath1);
                mybitmap2.Save(nowpath2);
                return View(accountModels);
            }
            if (accountModels.address_text == null) return RedirectToAction("order_fail");
            else return Redirect("/PurchaseList");
        }

        [HttpPost]
        public ActionResult order_success(AccountModels accountModels)
        {
            Session.Remove("has_pay");
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            ShopBusinessLogic.SellerSell sellerSell = new ShopBusinessLogic.SellerSell();
            var account_list = accountModels.accountModeList;
            string mem_phone = Session["mem_phone"].ToString();
            DateTime now_time = DateTime.Now;
            for (int i = 0; i < account_list.Count; ++i)
            {
                if (account_list[i].goods_num == 0) continue;
                var now_plist_id = now_time.ToString("yyyyMMddHHmmssfff") + mem_phone;
                //string now_seller_phone = memberPurchase.getGoods(account_list[i].goods_id).seller_phone;
                memberPurchase.addPurchaseLists(now_plist_id, mem_phone, account_list[i].goods_id, account_list[i].goods_num, now_time, account_list[i].seller_phone);
                memberPurchase.deletePurchaseCar(mem_phone, account_list[i].goods_id);
                sellerSell.reduceStock(account_list[i].goods_id, account_list[i].goods_num);
                sellerSell.addVolume(account_list[i].goods_id, account_list[i].goods_num);
            }
            return View(accountModels);
            //return Redirect("/PurchaseCar");//支付失败
        }

        [HttpPost]
        public ActionResult Index(MemberPurchaseCarViewModel memberPurchaseCarViewModels, string[] selected)
        {
            if (memberPurchaseCarViewModels == null) return Redirect("/Error");
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            var submit_list = memberPurchaseCarViewModels.select_list;
            var select_list = new List<AccountModels>();
            var address_list = loginMember.ShowMemberAddress(Session["mem_phone"].ToString()).Select(address_info => new MemberAddress()
            {
                address = address_info.address,
                address_tag = address_info.address_tag,
            }).ToList();
            decimal all_price = 0;
            for (int i = 0; i < submit_list.Count; ++i)
            {
                if (selected[i] != null && selected[i] == "on")
                {
                    var now_item = memberPurchase.getGoods(submit_list[i].goods_id);
                    var item = new AccountModels()
                    {
                        goods_id = submit_list[i].goods_id,
                        goods_name = now_item.goods_name,
                        goods_num = submit_list[i].goods_num,
                        unit_price = now_item.goods_price,
                        total_price = now_item.goods_price * submit_list[i].goods_num,
                        seller_phone = now_item.seller_phone,
                    };
                    if (item.goods_num > 0)
                    {
                        all_price += item.total_price;
                        select_list.Add(item);
                    }
                }
            }
            var resView = new AccountModels()
            {
                mem_phone = Session["mem_phone"].ToString(),
                addresses = address_list,
                accountModeList = select_list,
                all_price = all_price,
            };
            Session["has_pay"] = "true";
            return View(resView);
        }

        [HttpPost]
        public ActionResult Add(AccountModels ad)
        {
            string phone = Session["mem_phone"].ToString();
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            string new_address = ad.address;
            string new_address_tag = ad.address_tag;
            if (loginMember.AddMemberAddress(phone, new_address, new_address_tag))
            {
                var address_list = loginMember.ShowMemberAddress(Session["mem_phone"].ToString()).Select(address_info => new MemberAddress()
                {
                    address = address_info.address,
                    address_tag = address_info.address_tag,
                }).ToList();
                var resView = new AccountModels()
                {
                    accountModeList = ad.accountModeList,
                    mem_phone = Session["mem_phone"].ToString(),
                    addresses = address_list,
                    all_price = ad.all_price,
                };
                return PartialView("AccountPart1", resView);
            }
            else
            {
                return PartialView("AccountPart1", ad);
            }
        }

        [HttpPost]
        public ActionResult Delete(AccountModels ad)
        {
            string phone = Session["mem_phone"].ToString();
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            string delete_address = ad.address;
            string delete_address_tag = ad.address_tag;
            loginMember.DeleteMemberAddress(phone, delete_address, delete_address_tag);
            var address_list = loginMember.ShowMemberAddress(Session["mem_phone"].ToString()).Select(address_info => new MemberAddress()
            {
                address = address_info.address,
                address_tag = address_info.address_tag,
            }).ToList();
            var resView = new AccountModels()
            {
                accountModeList = ad.accountModeList,
                mem_phone = Session["mem_phone"].ToString(),
                addresses = address_list,
                all_price = ad.all_price,
            };
            return PartialView("AccountPart1", resView);
        }

        [HttpPost]
        public ActionResult Modify(AccountModels ad)
        {
            string phone = Session["mem_phone"].ToString();
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            string old_ad = ad.old_address;
            string old_ad_tag = ad.old_address_tag;
            string new_ad = ad.new_address;
            string new_ad_tag = ad.new_address_tag;
            if (loginMember.ModifyMemberAddress(phone, old_ad, new_ad, old_ad_tag, new_ad_tag))
            {
                var address_list = loginMember.ShowMemberAddress(Session["mem_phone"].ToString()).Select(address_info => new MemberAddress()
                {
                    address = address_info.address,
                    address_tag = address_info.address_tag,
                }).ToList();
                var resView = new AccountModels()
                {
                    accountModeList = ad.accountModeList,
                    mem_phone = Session["mem_phone"].ToString(),
                    addresses = address_list,
                    all_price = ad.all_price,
                };
                return PartialView("AccountPart1", resView);
            }
            else
            {
                return PartialView("AccountPart1", ad);
            }
        }
    }
}