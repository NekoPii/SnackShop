using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopModel;
using MySql.Data.MySqlClient;

namespace ShopRepository.MySQL
{
    public class Login_ShopRp
    {
        private string connectionstring = "server=localhost;database=webdb;username=root;pwd=123;Old Guids=true;";
        //Old Guids=true;必须要加

        public Member Login_GetMemberByPhone(string phone)
        {
            string queryString = "select * from members where mem_phone=@phone";
            var result = new Member();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = new Member()
                        {
                            mem_phone = reader.GetString("mem_phone"),
                            mem_pwd = reader.GetString("mem_pwd"),
                            mem_name = reader.GetString("mem_name"),
                            mem_type=reader.GetInt32("mem_type"),
                        };
                    }
                    reader.Close();
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public Seller GetSeller(string phone)
        {
            string queryString = "select * from seller where seller_phone=@phone";
            var result = new Seller();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = new Seller()
                        {
                            seller_address=reader.GetString("seller_address"),
                            seller_count=reader.GetString("seller_count"),
                        };
                    }
                    reader.Close();
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public bool SignUp_Member(string phone,string pwd,string name)
        {
            int mem_type = 1;
            bool flag = true;
            string queryString = "insert into members set mem_phone=@phone , mem_pwd=@pwd , mem_name=@name ,mem_type=@type";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd.Parameters.Add(new MySqlParameter("@pwd", pwd));
                cmd.Parameters.Add(new MySqlParameter("@name", name));
                cmd.Parameters.Add(new MySqlParameter("@type", mem_type));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch {
                    flag = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return flag;
        }

        //在注册成为用户的同时也直接注册成为卖家
        public bool SignUp_Seller_Direct(string phone, string pwd, string name,string seller_address,string seller_count)
        {
            int mem_type = 2;
            bool flag = true;
            string queryString1 = "insert into members set mem_phone=@phone , mem_pwd=@pwd , mem_name=@name ,mem_type=@mem_type ";
            string queryString2 = "insert into seller set seller_phone=@phone , seller_address=@seller_address , seller_count=@seller_count";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(queryString1, conn);
                MySqlCommand cmd2 = new MySqlCommand(queryString2, conn);
                cmd1.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd1.Parameters.Add(new MySqlParameter("@pwd", pwd));
                cmd1.Parameters.Add(new MySqlParameter("@name", name));
                cmd1.Parameters.Add(new MySqlParameter("@type", mem_type));
                cmd2.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd2.Parameters.Add(new MySqlParameter("@seller_address", seller_address));
                cmd2.Parameters.Add(new MySqlParameter("@seller_count", seller_count));
                try
                {
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                }
                catch
                {
                    flag = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return flag;
        }

        //注册成为用户后再注册为卖家
        public bool SignUp_Seller(string phone,string seller_address, string seller_count)
        {
            int mem_type = 2;
            bool flag = true;
            string queryString1 = "update members set mem_type=@type where mem_phone=@phone";
            string queryString2 = "insert into seller set seller_phone=@phone , seller_address=@seller_address , seller_count=@seller_count";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(queryString1, conn);
                MySqlCommand cmd2 = new MySqlCommand(queryString2, conn);
                cmd1.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd1.Parameters.Add(new MySqlParameter("@type", mem_type));
                cmd2.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd2.Parameters.Add(new MySqlParameter("@seller_address", seller_address));
                cmd2.Parameters.Add(new MySqlParameter("@seller_count", seller_count));
                try
                {
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                }
                catch
                {
                    flag = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return flag;
        }

        public List<Address> Show_MemberAddress(string phone)
        {
            string queryString = "select * from address where mem_phone=@phone order by address_tag desc";
            var result = new List<Address>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Address()
                        {
                            address = reader.GetString("mem_address"),
                            address_tag = reader.GetString("address_tag"),
                        });
                    }
                    reader.Close();
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public bool Add_MemberAddress(string phone,string address,string address_tag)
        {
            bool flag = true;
            string queryString = "insert into address set mem_phone=@phone , mem_address=@address ,address_tag=@address_tag ";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd.Parameters.Add(new MySqlParameter("@address", address));
                cmd.Parameters.Add(new MySqlParameter("@address_tag", address_tag));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    flag = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return flag;
        }

        public void Delete_MemberAddress(string phone,string address,string address_tag)
        {
            string sql = "delete from address where mem_phone=@phone and mem_address=@address and address_tag=@address_tag";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd.Parameters.Add(new MySqlParameter("@address", address));
                cmd.Parameters.Add(new MySqlParameter("@address_tag", address_tag));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
        }

        /*
        public void Modify_MemAddress(string phone, string old_address, string new_address,string old_address_tag,string new_address_tag)
        {

            //string queryString = "update address set mem_address=replace(mem_address , @old_address , @new_address) and address_tag=replace(address_tag , @old_address_tag , @new_address_tag) where mem_phone=@phone";
            //string queryString = "update address set mem_address=@new_address and address_tag=@new_address_tag where mem_phone=@phone and mem_address=@old_address and address_tag=@old_address_tag";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd.Parameters.Add(new MySqlParameter("@old_address", old_address));
                cmd.Parameters.Add(new MySqlParameter("@new_address", new_address));
                cmd.Parameters.Add(new MySqlParameter("@old_address_tag", old_address_tag));
                cmd.Parameters.Add(new MySqlParameter("@new_address_tag", new_address_tag));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
        }
        */

        public void Modify_MemberName(string phone,string new_name)
        {
            string queryString = "update members set mem_name=@new_name where mem_phone=@phone ";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd.Parameters.Add(new MySqlParameter("@new_name", new_name));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch{}
                finally
                {
                    conn.Close();
                }
            }
        }

        public void Modify_MemberPwd(string phone, string new_pwd)
        {
            string queryString = "update members set mem_pwd=@new_pwd where mem_phone=@phone ";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd.Parameters.Add(new MySqlParameter("@new_pwd", new_pwd));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch{}
                finally
                {
                    conn.Close();
                }
            }
        }

        public void Modify_SellerCount(string phone, string new_count)
        {
            string queryString = "update seller set seller_count=@new_count where seller_phone=@phone ";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd.Parameters.Add(new MySqlParameter("@new_count", new_count));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void Modify_SellerAddress(string phone, string new_address)
        {
            string queryString = "update seller set seller_address=@new_address where seller_phone=@phone ";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd.Parameters.Add(new MySqlParameter("@new_address", new_address));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch { }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
