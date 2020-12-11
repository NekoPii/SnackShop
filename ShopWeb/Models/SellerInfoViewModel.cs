using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ShopWeb.Models
{
    public class SellerInfoViewModel
    {
        public string sell_phone { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        [Remote("Validate", "SellInfo", ErrorMessage = "密码不正确！")]
        public string old_sell_pwd { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "新密码不能为空")]
        public string new_sell_pwd { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "再次输入新密码不能为空")]
        [System.ComponentModel.DataAnnotations.Compare("new_sell_pwd", ErrorMessage = "两次输入密码不一致")]
        public string new_sell_re_pwd { set; get; }

        public string sell_name { set; get; }

        public string seller_address { set; get; }

        public string seller_account { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "新用户名不能为空")]
        public string new_sell_name { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "新地址不能为空")]
        public string new_sell_address { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "新账户不能为空")]
        public string new_sell_account { set; get; }

        public decimal all_income { set; get; }
        public decimal now_month_income { set; get; }
        public List<MemberPurchaseListViewModel> sell_list { set; get; }
    }
}