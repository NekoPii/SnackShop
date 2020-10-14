using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopModel;
using MySql.Data.MySqlClient;

namespace ShopRepository.MySQL
{
    public class Pruchase_ShopRp
    {
        private string connectionstring = "server=localhost;database=webdb;username=root;pwd=123;Old Guids=true;";
        //Old Guids=true;必须要加

        public List<PurchaseCar> GetPurchaseCarLists(string mem_phone)
        {
            string sql = "select pcar_goods_id,pcar_goods_num from purchasecars where pcar_mem_phone=@mem_phone";
            var result = new List<PurchaseCar>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@mem_phone", mem_phone));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new PurchaseCar()
                        {
                            goods_id = reader.GetString("pcar_goods_id"),
                            goods_num = reader.GetInt32("pcar_goods_num"),
                        });
                    }
                    reader.Close();
                }
                catch  { }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public PurchaseCar GetPurchaseCarItem(string mem_phone,string goods_id)
        {
            string sql = "select pcar_goods_num from purchasecars where pcar_mem_phone=@mem_phone and pcar_goods_id=@goods_id";
            var result = new PurchaseCar();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@mem_phone", mem_phone));
                cmd.Parameters.Add(new MySqlParameter("@goods_id", goods_id));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = new PurchaseCar()
                        {
                            goods_id = goods_id,
                            goods_num = reader.GetInt32("pcar_goods_num"),
                        };
                    }
                    reader.Close();
                }
                catch  { }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public List<WishList> GetWishLists(string mem_phone)
        {
            string sql = "select wish_goods_id from wishlists where wish_mem_phone=@mem_phone";
            var result = new List<WishList>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@mem_phone", mem_phone));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new WishList()
                        {
                            goods_id=reader.GetString("wish_goods_id"),
                        });
                    }
                    reader.Close();
                }
                catch  { }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public List<GoodsImg> GetGoodsImgPath(string goods_id)
        {
            string sql = "select img_path from goods_img where goods_id=@goods_id";
            var result = new List<GoodsImg>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@goods_id", goods_id));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new GoodsImg()
                        {
                            goods_id=goods_id,
                            img_path=reader.GetString("img_path"),
                        });
                    }
                    reader.Close();
                }
                catch  { }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public List<PurchaseList> GetPurchaseLists(string mem_phone)
        {
            string sql = "select plist_id,plist_goods_id,plist_goods_name,plist_goods_num,plist_date,plist_goods_unit_price,plist_goods_total_price from purchaselists where plist_mem_phone=@mem_phone order by plist_date desc";
            var result = new List<PurchaseList>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@mem_phone", mem_phone));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new PurchaseList()
                        {
                            plist_id=reader.GetString("plist_id"),
                            goods_id=reader.GetString("plist_goods_id"),
                            goods_name=reader.GetString("plist_goods_name"),
                            goods_num=reader.GetInt32("plist_goods_num"),
                            date=reader.GetDateTime("plist_date"),
                            unit_price=reader.GetFloat("plist_goods_unit_price"),
                            total_price=reader.GetFloat("plist_goods_total_price"),
                        });
                    }
                    reader.Close();
                }
                catch  { }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public List<PurchaseList> GetPurchaseListsBySearchId(string mem_phone,string search_id)
        {
            string sql = "select plist_id,plist_goods_id,plist_goods_name,plist_goods_num,plist_date,plist_goods_unit_price,plist_goods_total_price from purchaselists where plist_mem_phone=@mem_phone and plist_id=@search_id order by plist_date desc";
            var result = new List<PurchaseList>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@mem_phone", mem_phone));
                cmd.Parameters.Add(new MySqlParameter("@search_id", search_id));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new PurchaseList()
                        {
                            plist_id=search_id,
                            goods_id = reader.GetString("plist_goods_id"),
                            goods_name = reader.GetString("plist_goods_name"),
                            goods_num = reader.GetInt32("plist_goods_num"),
                            date = reader.GetDateTime("plist_date"),
                            unit_price = reader.GetFloat("plist_goods_unit_price"),
                            total_price = reader.GetFloat("plist_goods_total_price"),
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

        public List<Goods> GetGoodsList()
        {
            string sql = "select goods_id,goods_name,goods_price,goods_detail,goods_img_path,goods_small_img_path from goods";
            var result = new List<Goods>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new Goods()
                        {
                            goods_id = reader.GetString("goods_id"),
                            goods_name = reader.GetString("goods_name"),
                            goods_price = reader.GetFloat("goods_price"),
                            goods_details = reader.GetString("goods_detail"),
                            goods_img_path = reader.GetString("goods_img_path"),
                            goods_small_img_path=reader.GetString("goods_small_img_path"),
                        });
                    }
                    reader.Close();
                }
                catch  { }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public Goods GetGoods (string goods_id)
        {
            string sql = "select goods_name,goods_price,goods_detail,goods_img_path,goods_small_img_path from goods where goods_id=@goods_id";
            var result = new Goods();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@goods_id", goods_id));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result=new Goods()
                        {                     
                            goods_id = goods_id,
                            goods_name=reader.GetString("goods_name"),
                            goods_price=reader.GetFloat("goods_price"),
                            goods_details=reader.GetString("goods_detail"),
                            goods_img_path=reader.GetString("goods_img_path"),
                            goods_small_img_path=reader.GetString("goods_small_img_path"),
                        };
                    }
                    reader.Close();
                }
                catch  { }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public bool AddWishList(string mem_phone,string goods_id)
        {
            bool flag = true;
            string sql = "insert into wishlists set wish_mem_phone=@mem_phone , wish_goods_id=@goods_id ";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@mem_phone", mem_phone));
                cmd.Parameters.Add(new MySqlParameter("@goods_id", goods_id));
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

        public bool AddPurchaseLists(string plist_id,string mem_phone,string goods_id,int num,DateTime date_time)
        {
            bool flag = true;
            string sql = "insert into purchaselists set plist_id=@plist_id , plist_mem_phone=@mem_phone , plist_goods_id=@goods_id ,plist_goods_name=@goods_name , plist_date=@date_time,plist_goods_num=@num,plist_goods_unit_price=@unit_price,plist_goods_total_price=@total_price";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@plist_id", plist_id));
                cmd.Parameters.Add(new MySqlParameter("@mem_phone", mem_phone));
                cmd.Parameters.Add(new MySqlParameter("@goods_id", goods_id));
                cmd.Parameters.Add(new MySqlParameter("@goods_name", GetGoods(goods_id).goods_name));
                cmd.Parameters.Add(new MySqlParameter("@date_time", date_time));
                cmd.Parameters.Add(new MySqlParameter("@num", num));
                cmd.Parameters.Add(new MySqlParameter("@unit_price", GetGoods(goods_id).goods_price));
                cmd.Parameters.Add(new MySqlParameter("@total_price", GetGoods(goods_id).goods_price * num));
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

        public bool AddPurchaseCar(string mem_phone,string goods_id,int num)
        {
            bool flag = true;
            string sql = "insert into purchasecars set pcar_mem_phone=@mem_phone , pcar_goods_id=@goods_id ,pcar_goods_num=@num";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@mem_phone", mem_phone));
                cmd.Parameters.Add(new MySqlParameter("@goods_id", goods_id));
                cmd.Parameters.Add(new MySqlParameter("@num", num));
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

        public void UpdatePurchaseCar(string mem_phone,string goods_id,int num)
        {
            string sql = "update purchasecars set pcar_goods_num=@num where pcar_mem_phone=@mem_phone and pcar_goods_id=@goods_id";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@mem_phone", mem_phone));
                cmd.Parameters.Add(new MySqlParameter("@goods_id", goods_id));
                cmd.Parameters.Add(new MySqlParameter("@num", num));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch {}
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeletePurchaseCar(string mem_phone,string goods_id)
        {
            string sql = "delete from purchasecars where pcar_mem_phone=@mem_phone and pcar_goods_id=@goods_id";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@mem_phone", mem_phone));
                cmd.Parameters.Add(new MySqlParameter("@goods_id", goods_id));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch  { }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeleteWishList(string mem_phone, string goods_id)
        {
            string sql = "delete from wishlists where wish_mem_phone=@mem_phone and wish_goods_id=@goods_id";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add(new MySqlParameter("@mem_phone", mem_phone));
                cmd.Parameters.Add(new MySqlParameter("@goods_id", goods_id));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch  { }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
