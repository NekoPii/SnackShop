using PagedList;
using ShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopWeb.Controllers
{
    public class SellInfoController : Controller
    {
        // GET: SellInfo
        public ActionResult Index()
        {
            if (Session["mem_phone"] == null) return Redirect("/Login");
            ShopBusinessLogic.SellerSell sellerSell = new ShopBusinessLogic.SellerSell();
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            
            string phone = Session["mem_phone"].ToString();
            DateTime now = DateTime.Now;
            var sell_list = sellerSell.getAllSellList(phone).Select(p_info => new MemberPurchaseListViewModel()
            {
                mem_phone=p_info.mem_phone,
                mem_name=loginMember.GetMemberByPhone(p_info.mem_phone).mem_name,
                goods_id=p_info.goods_id,
                goods_name=p_info.goods_name,
                goods_num=p_info.goods_num,
                date=p_info.date,
                plist_id=p_info.plist_id,
                unit_price=p_info.unit_price,
                total_price=p_info.total_price,
            }).ToList();
            var resView = new SellerInfoViewModel()
            {
                sell_phone = phone,
                seller_account = loginMember.getSeller(phone).seller_account,
                seller_address = loginMember.getSeller(phone).seller_address,
                sell_name = loginMember.GetMemberByPhone(phone).mem_name,
                all_income = sellerSell.getAllIncome(phone),
                now_month_income = sellerSell.getIncomeByMonth(phone, now),
                sell_list=sell_list,
            };
            return View(resView);
        }

        public ActionResult Modify()
        {
            if (Session["mem_phone"] == null) return Redirect("/Login");
            return View();
        }

        //用于在卖家信息页修改信息
        [HttpPost]
        public ActionResult Modify(SellerInfoViewModel sellerInfoViewModel)
        {
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            string phone = Session["mem_phone"].ToString();
            string new_pwd = sellerInfoViewModel.new_sell_pwd;
            string new_name = sellerInfoViewModel.new_sell_name;
            string new_address = sellerInfoViewModel.new_sell_address;
            string new_account = sellerInfoViewModel.new_sell_account;
            if (new_pwd != null)
            {
                loginMember.ModifyMemberPwd(phone, new_pwd);
                Session["mem_pwd"] = new_pwd;
            }
            if (new_name != null)
            {
                loginMember.ModifyMemberName(phone, new_name);
                Session["mem_name"] = new_name;
            }
            if(new_address!=null)
            {
                loginMember.ModifySellerAddress(phone, new_address);
                Session["seller_address"] = new_address;
            }
            if (new_account != null)
            {
                loginMember.ModifySellerAccount(phone, new_account);
                Session["seller_account"] = new_account;
            }
            return Redirect("/SellInfo");
        }

        public ActionResult SellList(int? page)
        {
            if (Session["mem_phone"] == null) return Redirect("/Login");
            ShopBusinessLogic.SellerSell sellerSell = new ShopBusinessLogic.SellerSell();
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            string phone = Session["mem_phone"].ToString();
            var sell_list = sellerSell.getAllSellList(phone).Select(p_info => new MemberPurchaseListViewModel()
            {
                mem_phone = p_info.mem_phone,
                mem_name = loginMember.GetMemberByPhone(p_info.mem_phone).mem_name,
                goods_id = p_info.goods_id,
                goods_name = p_info.goods_name,
                goods_num = p_info.goods_num,
                date = p_info.date,
                plist_id = p_info.plist_id,
                unit_price = p_info.unit_price,
                total_price = p_info.total_price,
            }).ToList();
            int pageNumber = page ?? 1;
            int pageSize = 10;
            return View(sell_list.ToPagedList(pageNumber,pageSize));
        }

        [HttpGet]
        public ActionResult Validate(string old_sell_pwd)
        {
            string phone = Session["mem_phone"].ToString();
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            string true_pwd = loginMember.GetMemberByPhone(phone).mem_pwd;
            return Json(old_sell_pwd == true_pwd, JsonRequestBehavior.AllowGet);
        }
    }
}