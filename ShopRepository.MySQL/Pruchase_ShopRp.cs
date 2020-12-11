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
                            goods_id = reader.GetInt32("pcar_goods_id"),
                            goods_num = reader.GetInt32("pcar_goods_num"),
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

        public PurchaseCar GetPurchaseCarItem(string mem_phone, int goods_id)
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
                catch { }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public List<GoodsTag> GetAllTags()
        {
            string sql = "select * from goods_tag";
            var result = new List<GoodsTag>();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(new GoodsTag()
                        {
                            tag = reader.GetString("tag"),
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
                            goods_id = reader.GetInt32("wish_goods_id"),
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

        public List<GoodsImg> GetGoodsImgPath(int goods_id)
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
                            goods_id = goods_id,
                            img_path = reader.GetString("img_path"),
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
                            plist_id = reader.GetString("plist_id"),
                            goods_id = reader.GetInt32("plist_goods_id"),
                            goods_name = reader.GetString("plist_goods_name"),
                            goods_num = reader.GetInt32("plist_goods_num"),
                            date = reader.GetDateTime("plist_date"),
                            unit_price = reader.GetDecimal("plist_goods_unit_price"),
                            total_price = reader.GetDecimal("plist_goods_total_price"),
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

        public List<GoodsTag> GetPurchaseListsTag_ai(string mem_phone)
        {
            string sql = "select plist_goods_id,goods_tag from purchaselists join goods where plist_mem_phone=@mem_phone and plist_goods_id=goods_id order by plist_date desc";
            var result = new List<GoodsTag>();
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
                        if (result.Count >= 6) break;//得到最新的6类tag
                        result.Add(new GoodsTag()
                        {
                            tag=reader.GetString("goods_tag"),
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

        public List<PurchaseList> GetPurchaseListsBySearchId(string mem_phone, string search_id)
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
                            plist_id = search_id,
                            goods_id = reader.GetInt32("plist_goods_id"),
                            goods_name = reader.GetString("plist_goods_name"),
                            goods_num = reader.GetInt32("plist_goods_num"),
                            date = reader.GetDateTime("plist_date"),
                            unit_price = reader.GetDecimal("plist_goods_unit_price"),
                            total_price = reader.GetDecimal("plist_goods_total_price"),
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
            string sql = "select * from goods join sell_goods where goods_id=sell_goods_id";
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
                            goods_id = reader.GetInt32("goods_id"),
                            goods_name = reader.GetString("goods_name"),
                            goods_price = reader.GetDecimal("goods_price"),
                            goods_details = reader.GetString("goods_detail"),
                            goods_img_path = reader.GetString("goods_img_path"),
                            goods_stock = reader.GetInt32("sell_stock"),
                            goods_volume = reader.GetInt32("sell_volume"),
                            seller_phone = reader.GetString("seller_phone"),
                            goods_tag = reader.GetString("goods_tag"),
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

        public List<Goods> GetGoodsList(string input_name)
        {
            var result = new List<Goods>();
            char[] chs = { ' ',',','，' };
            string[] input_names = input_name.Split(chs, options: StringSplitOptions.RemoveEmptyEntries);
            string sql_total = "";
            for (int i = 0; i < input_names.Length; ++i)
            {
                string sql = "select * from goods join sell_goods where goods_id=sell_goods_id and (goods_name like concat('%',@now"+i.ToString()+",'%') or goods_tag like concat('%',@now"+i.ToString()+",'%') )";
                if (string.IsNullOrEmpty(sql_total)) sql_total = sql;
                else sql_total += " union " + sql;

            }
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {

                MySqlCommand cmd = new MySqlCommand(sql_total, conn);
                for (int i = 0; i < input_names.Length; ++i)
                {
                    cmd.Parameters.Add(new MySqlParameter("@now"+i.ToString(), input_names[i]));
                }
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        var now_goods = new Goods()
                        {
                            goods_id = reader.GetInt32("goods_id"),
                            goods_name = reader.GetString("goods_name"),
                            goods_price = reader.GetDecimal("goods_price"),
                            goods_details = reader.GetString("goods_detail"),
                            goods_img_path = reader.GetString("goods_img_path"),
                            goods_stock = reader.GetInt32("sell_stock"),
                            goods_volume = reader.GetInt32("sell_volume"),
                            seller_phone = reader.GetString("seller_phone"),
                            goods_tag = reader.GetString("goods_tag"),
                        };
                        result.Add(now_goods);
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

        public List<Goods> GetGoodsList(string tag,string price_interval)
        {
            var result = new List<Goods>();
            int _index = price_interval.IndexOf('_');
            string sql;
            int low_price=0, high_price=0;
            if(tag=="all")
            {
                if (_index == -1)
                {
                    low_price = Convert.ToInt32(price_interval);
                    sql = "select * from goods join sell_goods where goods_id=sell_goods_id and goods_price>=@low_price order by goods_price";
                }
                else
                {
                    low_price = Convert.ToInt32(price_interval.Substring(0, _index));
                    high_price = Convert.ToInt32(price_interval.Substring(_index + 1, price_interval.Length - _index - 1));
                    sql = "select * from goods join sell_goods where goods_id=sell_goods_id and goods_price>=@low_price and goods_price<=@high_price order by goods_price";
                }
            }
            else if(price_interval=="all")
            {
                    sql = "select * from goods join sell_goods where goods_id=sell_goods_id and goods_tag=@tag";
            }
            else
            {
                if (_index == -1)
                {
                    low_price = Convert.ToInt32(price_interval);
                    sql = "select * from goods join sell_goods where goods_id=sell_goods_id and goods_tag=@tag and goods_price>=@low_price order by goods_price";
                }
                else
                {
                    low_price = Convert.ToInt32(price_interval.Substring(0, _index));
                    high_price = Convert.ToInt32(price_interval.Substring(_index + 1, price_interval.Length - _index - 1));
                    sql = "select * from goods join sell_goods where goods_id=sell_goods_id and goods_tag=@tag and goods_price>=@low_price and goods_price<=@high_price order by goods_price";
                }
            }     

            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if (tag == "all")
                {
                    cmd.Parameters.Add(new MySqlParameter("@low_price", low_price));
                    if (_index != -1) cmd.Parameters.Add(new MySqlParameter("@high_price", high_price));
                }
                else if(price_interval == "all")
                {
                    cmd.Parameters.Add(new MySqlParameter("@tag", tag));
                }
                else
                {
                    cmd.Parameters.Add(new MySqlParameter("@tag", tag));
                    cmd.Parameters.Add(new MySqlParameter("@low_price", low_price));
                    if (_index != -1) cmd.Parameters.Add(new MySqlParameter("@high_price", high_price));
                }
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        var now_goods = new Goods()
                        {
                            goods_id = reader.GetInt32("goods_id"),
                            goods_name = reader.GetString("goods_name"),
                            goods_price = reader.GetDecimal("goods_price"),
                            goods_details = reader.GetString("goods_detail"),
                            goods_img_path = reader.GetString("goods_img_path"),
                            goods_stock = reader.GetInt32("sell_stock"),
                            goods_volume = reader.GetInt32("sell_volume"),
                            seller_phone = reader.GetString("seller_phone"),
                            goods_tag = reader.GetString("goods_tag"),
                        };
                        result.Add(now_goods);
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

        public List<Goods> GetGoodsList(string tag,int num)
        {
            var result = new List<Goods>();
            string sql;
            if(tag=="all") sql = "select * from goods join sell_goods where goods_id=sell_goods_id order by rand() limit @num";
            else sql = "select * from goods join sell_goods where goods_id=sell_goods_id and goods_tag=@tag order by rand() limit @num";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                if(tag!="all") cmd.Parameters.Add(new MySqlParameter("@tag", tag));
                cmd.Parameters.Add(new MySqlParameter("@num", num));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        var now_goods = new Goods()
                        {
                            goods_id = reader.GetInt32("goods_id"),
                            goods_name = reader.GetString("goods_name"),
                            goods_price = reader.GetDecimal("goods_price"),
                            goods_details = reader.GetString("goods_detail"),
                            goods_img_path = reader.GetString("goods_img_path"),
                            goods_stock = reader.GetInt32("sell_stock"),
                            goods_volume = reader.GetInt32("sell_volume"),
                            seller_phone = reader.GetString("seller_phone"),
                            goods_tag = reader.GetString("goods_tag"),
                        };
                        result.Add(now_goods);
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

        public Goods GetGoods(int goods_id)
        {
            string sql = "select * from goods join sell_goods where goods_id=@goods_id and goods_id=sell_goods_id";
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
                        result = new Goods()
                        {
                            goods_id = goods_id,
                            goods_name = reader.GetString("goods_name"),
                            goods_price = reader.GetDecimal("goods_price"),
                            goods_details = reader.GetString("goods_detail"),
                            goods_img_path = reader.GetString("goods_img_path"),
                            goods_stock = reader.GetInt32("sell_stock"),
                            goods_volume = reader.GetInt32("sell_volume"),
                            seller_phone = reader.GetString("seller_phone"),
                            goods_tag = reader.GetString("goods_tag"),
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

        public bool AddWishList(string mem_phone, int goods_id)
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

        public bool AddPurchaseLists(string plist_id, string mem_phone, int goods_id, int num, DateTime date_time, string seller_phone)
        {
            bool flag = true;
            string sql = "insert into purchaselists set plist_id=@plist_id , plist_mem_phone=@mem_phone , plist_goods_id=@goods_id ,plist_goods_name=@goods_name , plist_date=@date_time,plist_goods_num=@num,plist_goods_unit_price=@unit_price,plist_goods_total_price=@total_price,plist_seller_phone=@seller_phone";
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
                cmd.Parameters.Add(new MySqlParameter("@seller_phone", seller_phone));
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

        public bool AddPurchaseCar(string mem_phone, int goods_id, int num)
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

        public void UpdatePurchaseCar(string mem_phone, int goods_id, int num)
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
                catch { }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeletePurchaseCar(string mem_phone, int goods_id)
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
                catch { }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeleteWishList(string mem_phone, int goods_id)
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
                catch { }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
