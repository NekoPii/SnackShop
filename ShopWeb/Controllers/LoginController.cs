using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index(MemberLoginViewModel memberViewModel)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ErrorMessage = null;
                ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
                //string userPhone = Request.Params["phone"];
                //ShopWeb.Models.MemberViewModel memberViewModel = new MemberViewModel();
                string userPhone = memberViewModel.mem_phone;
                //string userPwd = Request.Params["password"];
                string userPwd = memberViewModel.mem_pwd;
                string truePwd = loginMember.GetMemberByPhone(userPhone).mem_pwd;
                //var memView = new MemberViewModel();
                if (truePwd == userPwd)
                {
                    //memView.mem_name = loginMember.GetMemberByPhone(userPhone).mem_name;
                    //memView.mem_phone = loginMember.GetMemberByPhone(userPhone).mem_phone;
                    //memView.mem_pwd = loginMember.GetMemberByPhone(userPhone).mem_pwd;
                    string mem_name = loginMember.GetMemberByPhone(userPhone).mem_name;
                    string mem_phone = loginMember.GetMemberByPhone(userPhone).mem_phone;
                    string mem_pwd = loginMember.GetMemberByPhone(userPhone).mem_pwd;
                    Session["mem_name"] = mem_name;
                    Session["mem_phone"] = mem_phone;
                    Session["mem_pwd"] = mem_pwd;
                    Session["has_login"] = "true";
                    Session.Timeout = 30;
                    if (Session["ReturnToPurchaseCar"] != null) return Redirect("/PurchaseCar");
                    else if (Session["ReturnToWishList"] != null) return Redirect("/WishList");
                    else if (Session["ReturnToPurchaseList"] != null) return Redirect("/PurchaseList");
                    else if (Session["ReturnToMemberInfo"] != null) return Redirect("/MemberInfo");
                    else return Redirect("/Home");
                }
                else
                {
                    ViewBag.LoginErrorMessage = "用户名或密码错误";
                    return View();
                }
            }
            else return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(MemberLoginViewModel memberViewModel)
        {
            if (ModelState.IsValid)
            {
                ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
                //string userPhone = Request.Params["phone"];
                //ShopWeb.Models.MemberViewModel memberViewModel = new MemberViewModel();
                string userPhone = memberViewModel.mem_phone;
                //string userPwd = Request.Params["password"];
                string userPwd = memberViewModel.mem_pwd;
                string truePwd = loginMember.GetMemberByPhone(userPhone).mem_pwd;
                //var memView = new MemberViewModel();
                if (truePwd == userPwd)
                {
                    //memView.mem_name = loginMember.GetMemberByPhone(userPhone).mem_name;
                    //memView.mem_phone = loginMember.GetMemberByPhone(userPhone).mem_phone;
                    //memView.mem_pwd = loginMember.GetMemberByPhone(userPhone).mem_pwd;
                    string mem_name = loginMember.GetMemberByPhone(userPhone).mem_name;
                    string mem_phone = loginMember.GetMemberByPhone(userPhone).mem_phone;
                    string mem_pwd = loginMember.GetMemberByPhone(userPhone).mem_pwd;
                    Session["mem_name"] = mem_name;
                    Session["mem_phone"] = mem_phone;
                    Session["mem_pwd"] = mem_pwd;
                    Session["has_login"] = "true";
                    Session.Timeout = 30;
                    return Redirect("/Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "用户名或密码错误";
                    return View();
                }
            }
            else return Redirect("/Login");
        }

        /*[HttpPost]
        public JsonResult Check_pwd(MemberViewModel memberViewModel)
        {

            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            string inputPhone = memberViewModel.mem_phone;
            string inputPwd = memberViewModel.mem_pwd;
            string truePwd = loginMember.GetMemberByPhone(inputPhone).mem_pwd;
            if ( inputPwd!=truePwd)
            {
                return Json("输入密码错误", JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }*/


        /*
        var memView = new MemberViewModel();
        if (loginMember.GetMemberByPhone(userPhone).mem_pwd == userPwd)
        {
            memView.mem_name = loginMember.GetMemberByPhone(userPhone).mem_name;
            memView.mem_phone = loginMember.GetMemberByPhone(userPhone).mem_phone;
            memView.mem_pwd = loginMember.GetMemberByPhone(userPhone).mem_pwd;
            Session["mem_name"] = memView.mem_name;
            Session["mem_phone"] = memView.mem_phone;
            Session["mem_pwd"] = memView.mem_pwd;
            Session["has_login"] = "true";
            Session.Timeout = 1;
            return Redirect("/Home");
        }
        else
        {
            return RedirectToAction("Index");
        }
        */
        //}

        public ActionResult LoginOut()
        {
            Session.Clear();
            return Redirect("/Home");
        }

        /*public ActionResult Get(string id)
        {
            ShopBusinessLogic.Manager manager = new ShopBusinessLogic.Manager();
            var membinfo = manager.GetMemberId(id);
            if (membinfo == null) return HttpNotFound();
            var memberview = new MemberViewModel()
            {
                id = membinfo.id,
                pwd = membinfo.pwd,
                nname = membinfo.nname
            };

            return View(memberview);

            /*
           var meminfo = members.Where(now => now.id == id).FirstOrDefault();
           if (meminfo == null)
               return HttpNotFound();
           ViewBag.MemberInfo = meminfo;
           return View();
           */
        /*}

        public ActionResult ShowInfo(string id)
        //{

            ShopBusinessLogic.Manager manager = new ShopBusinessLogic.Manager();
            var meminfo = manager.GetAllMember().Select(member => new MemberViewModel()
            //{
                id = member.id,
                pwd = member.pwd,
                nname = member.nname
            }).ToList();
            var resViewModel = new ResModel()
            //{
                id = meminfo.Count.ToString(),
                pwd = "222",
                nname = "333",
                memberList = meminfo
            };
            return View(resViewModel);
           */
    }
}

    
