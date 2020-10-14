using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopWeb.Models
{
    public class MemberSignViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "手机号不能为空")]
        [RegularExpression("1+[3456789]+[0-9]{9}",ErrorMessage ="手机号格式不对")]
        public string mem_phone { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "密码不能为空")]
        public string mem_pwd { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "再次输入密码不能为空")]
        [Compare("mem_pwd",ErrorMessage ="两次输入密码不一致")]
        public string mem_re_pwd { set; get; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="用户名不能为空")]
        public string mem_name { set; get; }
    }
}