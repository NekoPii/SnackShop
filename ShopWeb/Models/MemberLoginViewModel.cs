using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ShopWeb.Models
{
    public class MemberLoginViewModel
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="手机号不能为空")]
        [RegularExpression("1+[3456789]+[0-9]{9}", ErrorMessage = "手机号格式不对")]
        public string mem_phone { set; get; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="密码不能为空")]
        public string mem_pwd { set; get; }

        public string mem_name { set; get; }
    }

    public class MemberListViewModel
    {
        public List<MemberLoginViewModel> mem_list { set; get; }
    }

    /*public class ResModel
    {
        public List<MemberViewModel> memberList { set; get; }
        public string id { set; get; }
        public string pwd { set; get; }
        public string nname { set; get; }
    }*/
}