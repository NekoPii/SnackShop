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

        [HttpPost]
        public ActionResult Index(MemberSignViewModel memberSignViewModel)
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
                if(loginMember.SignUpMemberByPhone(userPhone, userPwd,userName))
                {
                    Session["mem_name"] = loginMember.GetMemberByPhone(userPhone).mem_name;
                    Session["mem_phone"] = loginMember.GetMemberByPhone(userPhone).mem_phone;
                    Session["mem_pwd"] = loginMember.GetMemberByPhone(userPhone).mem_pwd;
                    Session["has_login"] = "true";
                    Session.Timeout = 10;
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

        public ActionResult LoginOut()
        {
            Session.Clear();
            return Redirect("/Home");
        }
    }
}
