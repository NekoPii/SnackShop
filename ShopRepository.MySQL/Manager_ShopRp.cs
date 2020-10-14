using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopModel;
using MySql.Data.MySqlClient;

namespace ShopRepository.MySQL
{
    public class Manager_ShopRp
    {
        private string connectionstring = "server=localhost;database=webdb;username=root;pwd=123;Old Guids=true;";
        //Old Guids=true;必须要加

        public List<Member> Manager_GetAllMemberData()
        {
            string queryString = "select * from members";

            var result = new List<Member>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(queryString, conn))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Member()
                        {
                            mem_phone = reader.GetString("mem_phone"),
                            mem_pwd = reader.GetString("mem_pwd"),
                            mem_name = reader.GetString("mem_name"),
                        });
                    }
                    reader.Close();
                }
                conn.Close();

            }
            return result;
        }

        public Member Managar_GetMemberByUid(string uid)
        {
            string queryString = "select * from members where mem_uid=@uid";
            var result = new Member();
            using(MySqlConnection conn=new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@uid", uid));            
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

        public Member Managar_GetMemberByPhone(string phone)
        {
            string queryString = "select * from members where mem_phone=@phone";
            var result = new Member();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone",phone));
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
    }
}
