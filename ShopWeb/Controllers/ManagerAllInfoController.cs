using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class ManagerAllInfoController : Controller
    {
        // GET: ManagerAllInfo
        public ActionResult Index()
        {
            ShopBusinessLogic.Manager manager = new ShopBusinessLogic.Manager();
            var list = manager.GetAllMember().Select(mem_info => new MemberLoginViewModel()
            {
                mem_phone = mem_info.mem_phone,
                mem_name = mem_info.mem_name,
                mem_pwd = mem_info.mem_pwd,
            }).ToList();
            var resView = new MemberListViewModel()
            {
                mem_list = list,
            };
            return View(resView);
        }
    }
}