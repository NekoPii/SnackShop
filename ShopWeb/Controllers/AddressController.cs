using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ShopModel;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class AddressController : Controller
    {
        // GET: Address
        public ActionResult Index()
        {
            if (Session["has_login"] == null)
            {
                return Redirect("/Login");
            }
            else
            {
                ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
                var address_list = loginMember.ShowMemberAddress(Session["mem_phone"].ToString()).Select(address_info => new MemberAddress()
                {
                    address = address_info.address,
                    address_tag = address_info.address_tag,
                }).ToList();
                var resView = new MemberAddress()
                {
                    addresses = address_list,
                };
                return View(resView);
            }
        }

        [HttpPost]
        public ActionResult Add(MemberAddress ad)
        {
            string phone = Session["mem_phone"].ToString();
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            string new_address = ad.address;
            string new_address_tag = ad.address_tag;
            if (loginMember.AddMemberAddress(phone, new_address, new_address_tag))
            {
                var now_address_list = loginMember.ShowMemberAddress(phone).Select(address_info => new MemberAddress()
                {
                    address = address_info.address,
                    address_tag = address_info.address_tag,
                }).ToList();
                var resView = new MemberAddress()
                {
                    addresses = now_address_list,
                };
                return PartialView("AddressPart1", resView);
            }
            else return PartialView("AddressPart1", ad);
        }

        [HttpPost]
        public ActionResult Delete(MemberAddress ad)
        {
            string phone = Session["mem_phone"].ToString();
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            string delete_address = ad.address;
            string delete_address_tag = ad.address_tag;
            loginMember.DeleteMemberAddress(phone, delete_address, delete_address_tag);
            var now_address_list = loginMember.ShowMemberAddress(phone).Select(address_info => new MemberAddress()
            {
                address = address_info.address,
                address_tag = address_info.address_tag,
            }).ToList();
            var resView = new MemberAddress()
            {
                addresses=now_address_list,
            };
            return PartialView("AddressPart1", resView);

            //return Redirect("/Address");
        }

        [HttpPost]
        public ActionResult Modify(MemberAddress ad)
        {
            string phone = Session["mem_phone"].ToString();
            ShopBusinessLogic.LoginMember loginMember = new ShopBusinessLogic.LoginMember();
            string old_ad = ad.old_address;
            string old_ad_tag = ad.old_address_tag;
            string new_ad = ad.new_address;
            string new_ad_tag = ad.new_address_tag;
            if (loginMember.ModifyMemberAddress(phone, old_ad, new_ad, old_ad_tag, new_ad_tag))
            {
                var now_address_list = loginMember.ShowMemberAddress(phone).Select(address_info => new MemberAddress()
                {
                    address = address_info.address,
                    address_tag = address_info.address_tag,
                }).ToList();
                var resView = new MemberAddress()
                {
                    addresses = now_address_list,
                };
                return PartialView("AddressPart1", resView);
            }
            else return PartialView("AddressPart1", ad);
        }
    }
}