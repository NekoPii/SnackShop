using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopModel;

namespace ShopBusinessLogic
{
    public class Manager

    {
        private ShopRepository.MySQL.Manager_ShopRp rp = new ShopRepository.MySQL.Manager_ShopRp();
        
        public List<Member> GetAllMember()
        {
            return rp.Manager_GetAllMemberData();
        }
        public Member GetMemberById(string uid)
        {
            return rp.Managar_GetMemberByUid(uid);
        }
        public Member GetMemberByPhone(string phone)
        {
            return rp.Managar_GetMemberByPhone(phone);
        }

    }
}
