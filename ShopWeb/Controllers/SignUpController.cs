using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Member()
        {
            return View();
        }

        public ActionResult Seller()
        {
            if(Session["mem_phone"]==null) return Redirect("/Error");
            return View();
        }

        public ActionResult SellerDir()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Member(MemberSignViewModel memberSignViewModel)
        {
            ViewBag.SignUpErrorMessage = null;
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            //string userPhone = Request.Params["phone"];
            //string userPwd = Request.Params["password"];
            string userPhone = memberSignViewModel.mem_phone;
            string userName = memberSignViewModel.mem_name;
            string userPwd = memberSignViewModel.mem_pwd;
            string userRePwd = memberSignViewModel.mem_re_pwd;
            if(ModelState.IsValid)
            {
                if(loginMember.SignUpMemberByPhone(userPhone,userPwd,userName))
                {
                    Session["mem_name"] = loginMember.GetMemberByPhone(userPhone).mem_name;
                    Session["mem_phone"] = loginMember.GetMemberByPhone(userPhone).mem_phone;
                    Session["mem_pwd"] = loginMember.GetMemberByPhone(userPhone).mem_pwd;
                    Session["mem_type"] = 1;
                    Session["has_login"] = "true";
                    Session.Timeout = 30;
                    return Redirect("/Home");
                }
                else
                {
                    ViewBag.SignUpErrorMessage = "该手机号已注册";
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult SellerDir(SellerDirSignViewModel sellerDirSignViewModel)
        {
            ViewBag.SignUpErrorMessage = null;
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            //string userPhone = Request.Params["phone"];
            //string userPwd = Request.Params["password"];
            string userPhone = sellerDirSignViewModel.mem_phone;
            string userName = sellerDirSignViewModel.mem_name;
            string userPwd = sellerDirSignViewModel.mem_pwd;
            string userRePwd = sellerDirSignViewModel.mem_re_pwd;
            string sellCount = sellerDirSignViewModel.seller_account;
            string sellAddress = sellerDirSignViewModel.seller_address;
            if (ModelState.IsValid)
            {
                if (loginMember.SignUpSellerDir(userPhone, userPwd, userName,sellAddress,sellCount))
                {
                    Session["mem_name"] = userName;
                    Session["mem_phone"] = userPhone;
                    Session["mem_pwd"] = userPwd;
                    Session["seller_account"] = sellCount;
                    Session["seller_address"] = sellAddress;
                    Session["mem_type"] = 2;
                    Session["has_login"] = "true";
                    Session.Timeout = 30;
                    return Redirect("/Home");
                }
                else
                {
                    ViewBag.SignUpErrorMessage = "该手机号已注册";
                    return View();
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Seller(SellerSignViewModel sellerSignViewModel)
        {
            ViewBag.SignUpErrorMessage = null;
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            //string userPhone = Request.Params["phone"];
            //string userPwd = Request.Params["password"];
            if (Session["mem_phone"] == null) return Redirect("/Home");
            string phone = Session["mem_phone"].ToString();
            string sellCount = sellerSignViewModel.seller_account;
            string sellAddress = sellerSignViewModel.seller_address;
            if (ModelState.IsValid)
            {
                if (loginMember.SignUpSeller(phone, sellAddress, sellCount))
                {
                    Session.Clear();
                    Session["mem_phone"] = phone;
                    Session["mem_name"] = loginMember.GetMemberByPhone(phone).mem_name;
                    Session["mem_pwd"] = loginMember.GetMemberByPhone(phone).mem_pwd;
                    Session["seller_account"] = sellCount;
                    Session["seller_address"] = sellAddress;
                    Session["mem_type"] = loginMember.GetMemberByPhone(phone).mem_type;
                    Session["has_login"] = "true";
                    Session.Timeout = 30;
                    return Redirect("/Home");
                }
                else return View();
            }
            return View();
        }

        public ActionResult LoginOut()
        {
            Session.Clear();
            return Redirect("/Home");
        }
    }
}
