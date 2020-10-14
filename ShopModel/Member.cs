using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopModel
{
    public class Member
    {   
        public string mem_phone { set; get; }//用于登入，作为账号
        public string mem_pwd { set; get; }//用于登入，作为密码
        public string mem_name { set; get; }
        public string mem_address { set; get; }
    }
}
