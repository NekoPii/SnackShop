using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ShopWeb.Models
{
    public class AccountModels
    {
        public string mem_phone { set; get; }
        public string goods_id { set; get; }
        public string goods_name { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "地址不能为空")]
        public string address { set; get; }

        public string address_text { set; get; }

        public string address_tag { set; get; }

        public string old_address { set; get; }

        public string old_address_tag { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "地址不能为空")]
        public string new_address { set; get; }

        public string new_address_tag { set; get; }

        public float unit_price { set; get; }
        public int goods_num { set; get; }
        public float total_price { set; get; }
        public float all_price { set; get; }
        public float score { set; get; }
        public List<MemberAddress> addresses { set; get; }
        public List<AccountModels> accountModeList { set; get; }
    }
}