using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopWeb.Models
{
    public class SellerSignViewModel
    {

        public string mem_phone { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "发货地址不能为空")]
        public string sell_address { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "绑定账户不能为空")]
        public string sell_count { set; get; }
    }
}