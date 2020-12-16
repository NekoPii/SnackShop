using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopModel;
using MySql.Data.MySqlClient;

namespace ShopRepository.MySQL
{
    public class Seller_ShopRp
    {

        private string connectionstring = "server=localhost;database=webdb;username=root;pwd=123;Old Guids=true;";
        //Old Guids=true;必须要加

        public bool Add_Goods(string seller_phone, string goods_name, string goods_tag, decimal goods_price, string goods_detail, int goods_stock,string goods_img_path)
        {
            bool flag = true;
            int volume = 0;
            int goods_id = GetAddGoodsId();
            string sql1 = "insert into sell_goods set seller_phone=@phone ,sell_goods_id=@id , sell_stock=@stock ,sell_volume=@volume ";
            string sql2 = "insert into goods set goods_name=@name ,goods_price=@price , goods_detail=@detail ,goods_tag=@tag ,goods_img_path=@img_path ";
            string sql3 = "insert into goods_img set goods_id=@id ,img_path=@img_path";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                MySqlCommand cmd3 = new MySqlCommand(sql3, conn);

                conn.Open();

                cmd1.Parameters.Add(new MySqlParameter("@phone", seller_phone));
                cmd1.Parameters.Add(new MySqlParameter("@id", goods_id));
                cmd1.Parameters.Add(new MySqlParameter("@stock", goods_stock));
                cmd1.Parameters.Add(new MySqlParameter("@volume", volume));

                cmd2.Parameters.Add(new MySqlParameter("@name", goods_name));
                cmd2.Parameters.Add(new MySqlParameter("@id", goods_id));
                cmd2.Parameters.Add(new MySqlParameter("@price", goods_price));
                cmd2.Parameters.Add(new MySqlParameter("@detail", goods_detail));
                cmd2.Parameters.Add(new MySqlParameter("@tag", goods_tag));
                cmd2.Parameters.Add(new MySqlParameter("@img_path", goods_img_path));

                cmd3.Parameters.Add(new MySqlParameter("@id", goods_id));
                cmd3.Parameters.Add(new MySqlParameter("@img_path", goods_img_path));

                try
                {
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();
                }
                catch
                {
                    flag = false;
                }

                conn.Close();
            }
            return flag;
        }

        public bool Add_Img(int goods_id,string img_path)
        {
            bool flag = true;
            string sql1 = "insert into goods_img set goods_id=@id ,img_path=@img_path";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);

                conn.Open();

                cmd1.Parameters.Add(new MySqlParameter("@id", goods_id));
                cmd1.Parameters.Add(new MySqlParameter("@img_path", img_path));
                try
                {
                    cmd1.ExecuteNonQuery();
                }
                catch
                {
                    flag = false;
                }
                conn.Close();
            }
            return flag;
        }

        public bool Modify_Goods(string seller_phone, int goods_id, string goods_name, string goods_tag, decimal goods_price, string goods_detail, int goods_stock)
        {
            bool flag = true;
            string sql1 = "update sell_goods set sell_stock=@stock where seller_phone=@phone and sell_goods_id=@id";
            string sql2 = "update goods set goods_name=@name ,goods_price=@price , goods_detail=@detail , goods_tag=@tag where goods_id=@id ";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
                MySqlCommand cmd2 = new MySqlCommand(sql2, conn);

                conn.Open();

                cmd1.Parameters.Add(new MySqlParameter("@phone", seller_phone));
                cmd1.Parameters.Add(new MySqlParameter("@id", goods_id));
                cmd1.Parameters.Add(new MySqlParameter("@stock", goods_stock));

                cmd2.Parameters.Add(new MySqlParameter("@name", goods_name));
                cmd2.Parameters.Add(new MySqlParameter("@id", goods_id));
                cmd2.Parameters.Add(new MySqlParameter("@price", goods_price));
                cmd2.Parameters.Add(new MySqlParameter("@detail", goods_detail));
                cmd2.Parameters.Add(new MySqlParameter("@tag", goods_tag));

                try
                {
                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                }
                catch
                {
                    flag = false;
                }
                conn.Close();
            }
            return flag;
        }

        public void Delete_Goods(string seller_phone,int goods_id)
        {
            string sql1 = "delete from sell_goods where sell_goods_id=@id and seller_phone=@phone";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);

                conn.Open();

                cmd1.Parameters.Add(new MySqlParameter("@id", goods_id));
                cmd1.Parameters.Add(new MySqlParameter("@phone", seller_phone));

                cmd1.ExecuteNonQuery();

                conn.Close();

            }
        }

        public bool IsInGoodsList(int goods_id)
        {
            bool flag = true;
            string sql1 = "select count(*) as num from goods where goods_id=@id";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);

                conn.Open();

                cmd1.Parameters.Add(new MySqlParameter("@id", goods_id));
                try
                {
                    MySqlDataReader reader = cmd1.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetInt32("num") == 0) flag = false;
                    }
                }
                catch
                {
                    flag = false;
                }
                conn.Close();
            }
            return flag;
        }

        public void Reduce_Stock(int goods_id,int reduce_num)
        {
            string sql1 = "update sell_goods set sell_stock=sell_stock-@num where sell_goods_id=@id";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);

                conn.Open();

                cmd1.Parameters.Add(new MySqlParameter("@id", goods_id));
                cmd1.Parameters.Add(new MySqlParameter("@num", reduce_num));

                cmd1.ExecuteNonQuery();

                conn.Close();

            }
        }

        public void Add_Volume(int goods_id, int add_num)
        {
            string sql1 = "update sell_goods set sell_volume=sell_volume+@num where sell_goods_id=@id";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);

                conn.Open();

                cmd1.Parameters.Add(new MySqlParameter("@id", goods_id));
                cmd1.Parameters.Add(new MySqlParameter("@num", add_num));

                cmd1.ExecuteNonQuery();

                conn.Close();

            }
        }

        public List<Goods> Get_AllSellGoods(string seller_phone)
        {
            var res = new List<Goods>();
            string sql1 = "select * from sell_goods join goods where sell_goods_id=goods_id and seller_phone=@phone";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);

                conn.Open();

                cmd1.Parameters.Add(new MySqlParameter("@phone", seller_phone));

                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    res.Add(new Goods
                    {
                        seller_phone = reader.GetString("seller_phone"),
                        goods_id = reader.GetInt32("sell_goods_id"),
                        goods_stock = reader.GetInt32("sell_stock"),
                        goods_volume = reader.GetInt32("sell_volume"),
                        goods_name = reader.GetString("goods_name"),
                        goods_price = reader.GetDecimal("goods_price"),
                        goods_details = reader.GetString("goods_detail"),
                        goods_img_path = reader.GetString("goods_img_path"),
                        goods_tag = reader.GetString("goods_tag"),
                    });
                }

                conn.Close();
            }
            return res;
        }

        public Goods Get_SellGoods(string seller_phone, int goods_id)
        {
            var res = new Goods();
            string sql1 = "select * from sell_goods join goods where sell_goods_id=@goods_id and seller_phone=@phone and sell_goods_id=goods_id ";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);

                conn.Open();

                cmd1.Parameters.Add(new MySqlParameter("@phone", seller_phone));
                cmd1.Parameters.Add(new MySqlParameter("@goods_id", goods_id));

                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    res = new Goods()
                    {
                        seller_phone = reader.GetString("seller_phone"),
                        goods_id = reader.GetInt32("sell_goods_id"),
                        goods_stock = reader.GetInt32("sell_stock"),
                        goods_volume = reader.GetInt32("sell_volume"),
                        goods_name = reader.GetString("goods_name"),
                        goods_price = reader.GetDecimal("goods_price"),
                        goods_details = reader.GetString("goods_detail"),
                        goods_img_path = reader.GetString("goods_img_path"),
                        goods_tag = reader.GetString("goods_tag"),
                    };
                }

                conn.Close();
            }
            return res;
        }

        public int GetAddGoodsId()
        {
            int goods_id = 0;
            string sql0 = "SELECT `AUTO_INCREMENT` as next_id FROM `information_schema`.`TABLES` WHERE `TABLE_NAME`='goods' and `AUTO_INCREMENT` is not null";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd0 = new MySqlCommand(sql0, conn);
                conn.Open();
                MySqlDataReader reader = cmd0.ExecuteReader();
                while (reader.Read()) goods_id = reader.GetInt32("next_id");
                conn.Close();
            }
            return goods_id;
        }

        public List<PurchaseList> GetSellList(string seller_phone)
        {
            var res = new List<PurchaseList>();
            string sql1 = "select * from purchaselists where plist_seller_phone=@phone order by plist_date desc";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);

                conn.Open();

                cmd1.Parameters.Add(new MySqlParameter("@phone", seller_phone));

                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    res.Add(new PurchaseList
                    {
                        mem_phone=reader.GetString("plist_mem_phone"),
                        goods_id=reader.GetInt32("plist_goods_id"),
                        goods_name=reader.GetString("plist_goods_name"),
                        goods_num=reader.GetInt32("plist_goods_num"),
                        date=reader.GetDateTime("plist_date"),
                        unit_price=reader.GetDecimal("plist_goods_unit_price"),
                        total_price=reader.GetDecimal("plist_goods_total_price"),
                        seller_phone=seller_phone,
                        plist_id=reader.GetString("plist_id"),
                    });
                }

                conn.Close();
            }
            return res;
        }

        public List<PurchaseList> GetSellListPerGoods(string seller_phone,int sell_goods_id)
        {
            var res = new List<PurchaseList>();
            string sql1 = "select * from purchaselists where plist_seller_phone=@phone and plist_goods_id=@sell_goods_id order by plist_date desc";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);

                conn.Open();

                cmd1.Parameters.Add(new MySqlParameter("@phone", seller_phone));
                cmd1.Parameters.Add(new MySqlParameter("@sell_goods_id", sell_goods_id));

                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    res.Add(new PurchaseList
                    {
                        mem_phone = reader.GetString("plist_mem_phone"),
                        goods_id = sell_goods_id,
                        goods_name = reader.GetString("plist_goods_name"),
                        goods_num = reader.GetInt32("plist_goods_num"),
                        date = reader.GetDateTime("plist_date"),
                        unit_price = reader.GetDecimal("plist_goods_unit_price"),
                        total_price = reader.GetDecimal("plist_goods_total_price"),
                        seller_phone = seller_phone,
                        plist_id = reader.GetString("plist_id"),
                    });
                }

                conn.Close();
            }
            return res;
        }

        public decimal GetAllIncome(string seller_phone)
        {
            decimal res = 0;
            string sql1 = "select sum(plist_goods_total_price) as sum from purchaselists where plist_seller_phone=@phone";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);

                conn.Open();

                cmd1.Parameters.Add(new MySqlParameter("@phone", seller_phone));

                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    res = reader["sum"].ToString().Equals("") ? 0 : reader.GetDecimal("sum");
                }

                conn.Close();
            }
            return res;
        }

        public decimal GetIncomeByMonth(string seller_phone,DateTime now)
        {
            decimal res = 0;
            string sql1 = "select sum(plist_goods_total_price) as sum from purchaselists where plist_seller_phone=@phone and date_format(plist_date,'%Y%m')=date_format(@now,'%Y%m')";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd1 = new MySqlCommand(sql1, conn);

                conn.Open();

                cmd1.Parameters.Add(new MySqlParameter("@phone", seller_phone));
                cmd1.Parameters.Add(new MySqlParameter("@now", now));

                MySqlDataReader reader = cmd1.ExecuteReader();
                while(reader.Read())
                {
                    res = reader["sum"].ToString().Equals("")?0:reader.GetDecimal("sum");
                }   
                conn.Close();
            }
            return res;
        }
    }
}
