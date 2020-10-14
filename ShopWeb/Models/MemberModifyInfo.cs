using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopWeb.Models
{
    public class MemberModifyInfo
    {
        public string old_true_pwd { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        [Compare("old_true_pwd",ErrorMessage ="密码不正确")]
        public string old_mem_pwd { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "新密码不能为空")]
        public string new_mem_pwd { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "再次输入新密码不能为空")]
        [Compare("new_mem_pwd", ErrorMessage = "两次输入密码不一致")]
        public string new_mem_re_pwd { set; get; }

        public string old_mem_name { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "新用户名不能为空")]
        public string new_mem_name { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "新地址不能为空")]
        public string new_mem_address { set; get; }
    }
}