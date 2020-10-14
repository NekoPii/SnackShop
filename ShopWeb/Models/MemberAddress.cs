using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopWeb.Models
{
    public class MemberAddress
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "地址不能为空")]
        public string address { set; get; }

        public string address_tag { set; get; }

        public string old_address { set; get; }

        public string old_address_tag { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "地址不能为空")]
        public string new_address { set; get; }

        public string new_address_tag { set; get; }

        public List<MemberAddress> addresses { set; get; }
    }
}