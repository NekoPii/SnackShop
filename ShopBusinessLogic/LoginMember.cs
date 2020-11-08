using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopModel;

namespace ShopBusinessLogic
{
    public class LoginMember
    {
        private ShopRepository.MySQL.Login_ShopRp rp = new ShopRepository.MySQL.Login_ShopRp();

        public Member GetMemberByPhone(string phone)
        {
            return rp.Login_GetMemberByPhone(phone);
        }

        public Seller getSeller(string phone)
        {
            return rp.GetSeller(phone);
        }

        public bool SignUpMemberByPhone(string phone,string pwd,string name)
        {
            return rp.SignUp_Member(phone, pwd, name);
        }

        public bool SignUpSellerDir(string phone,string pwd,string name,string seller_address,string seller_account)
        {
            return rp.SignUp_Seller_Direct(phone, pwd, name, seller_address, seller_account);
        }

        public bool SignUpSeller(string phone,string seller_address,string seller_account)
        {
            return rp.SignUp_Seller(phone, seller_address, seller_account);
        }

        public List<Address> ShowMemberAddress(string phone)
        {
            return rp.Show_MemberAddress(phone);
        }

        public bool AddMemberAddress(string phone,string address,string address_tag)
        {
            return rp.Add_MemberAddress(phone, address, address_tag);
        }

        public void DeleteMemberAddress(string phone,string address,string address_tag)
        {
            rp.Delete_MemberAddress(phone, address, address_tag);
        }

        public bool ModifyMemberAddress(string phone, string old_address,string new_address,string old_address_tag,string new_address_tag)
        {
            rp.Delete_MemberAddress(phone, old_address, old_address_tag);
            return rp.Add_MemberAddress(phone, new_address, new_address_tag);
        }

        public void ModifyMemberName(string phone,string new_name)
        {
           rp.Modify_MemberName(phone, new_name);
        }

        public void ModifyMemberPwd(string phone,string new_pwd)
        {
            rp.Modify_MemberPwd(phone, new_pwd);
        }

        public void ModifySellerAddress(string phone,string address)
        {
            rp.Modify_SellerAddress(phone, address);
        }

        public void ModifySellerAccount(string phone,string count)
        {
            rp.Modify_SellerAccount(phone, count);
        }
    }
}
