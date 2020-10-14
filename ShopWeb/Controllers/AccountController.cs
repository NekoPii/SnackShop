using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class AccountController : Controller
    {
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
        public ActionResult pay2048(AccountModels accountModels)
        {

            if (Session["has_pay"] != null&&accountModels.address_text!=null)
            {
                Session.Remove("has_pay");
                accountModels.score = 0;
                for (int i = 0; i < accountModels.accountModeList.Count; ++i)
                {
                    accountModels.all_price += accountModels.accountModeList[i].total_price;
                }
                accountModels.all_price = accountModels.all_price / 100 + accountModels.all_price;
                return View(accountModels);
            }
            if (accountModels.address_text == null) return RedirectToAction("order_fail");
            else return Redirect("/PurchaseList");
        }

        [HttpPost]
        public ActionResult order_success(AccountModels accountModels)
        {
            if (accountModels.score >= accountModels.all_price)
            {
                Session.Remove("has_pay");
                ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
                var account_list = accountModels.accountModeList;
                string mem_phone = Session["mem_phone"].ToString();
                DateTime now_time = DateTime.Now;
                for (int i = 0; i < account_list.Count; ++i)
                {
                    var now_plist_id = now_time.ToString("yyyyMMddHHmmssfff") + mem_phone;
                    memberPurchase.addPurchaseLists(now_plist_id,mem_phone, account_list[i].goods_id, account_list[i].goods_num, now_time);
                    memberPurchase.deletePurchaseCar(mem_phone, account_list[i].goods_id);
                }
                return View(accountModels);
            }
            else return Redirect("/PurchaseCar");
        }

        [HttpPost]
        public ActionResult Index(MemberPurchaseCarViewModel memberPurchaseCarViewModels, string[] selected)
        {
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            var submit_list = memberPurchaseCarViewModels.select_list;
            var select_list = new List<AccountModels>();
            var address_list = loginMember.ShowMemberAddress(Session["mem_phone"].ToString()).Select(address_info => new MemberAddress()
            {
                address = address_info.address,
                address_tag = address_info.address_tag,
            }).ToList();
            float all_price = 0;
            for (int i = 0; i < submit_list.Count; ++i)
            {
                if (selected[i] != null && selected[i] == "on")
                {
                    var item = new AccountModels()
                    {
                        goods_id = submit_list[i].goods_id,
                        goods_name = memberPurchase.getGoods(submit_list[i].goods_id).goods_name,
                        goods_num = submit_list[i].goods_num,
                        unit_price = memberPurchase.getGoods(submit_list[i].goods_id).goods_price,
                        total_price = memberPurchase.getGoods(submit_list[i].goods_id).goods_price * submit_list[i].goods_num,
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
                addresses=address_list,
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
            return PartialView("AccountPart1",resView);
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