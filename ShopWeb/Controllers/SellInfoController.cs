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
                sell_count = loginMember.getSeller(phone).seller_count,
                sell_address = loginMember.getSeller(phone).seller_address,
                sell_name = loginMember.GetMemberByPhone(phone).mem_name,
                all_income = sellerSell.getAllIncome(phone),
                now_month_income = sellerSell.getIncomeByMonth(phone, now),
                sell_list=sell_list,
            };
            return View(resView);
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
            int pageSize = 3;
            return View(sell_list.ToPagedList(pageNumber,pageSize));
        }
    }
}