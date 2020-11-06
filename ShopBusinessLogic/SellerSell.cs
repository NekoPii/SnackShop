using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopModel;

namespace ShopBusinessLogic
{
    public class SellerSell
    {
        private ShopRepository.MySQL.Seller_ShopRp rp = new ShopRepository.MySQL.Seller_ShopRp();

        public List<Goods> getAllSellGoods(string phone)
        {
            return rp.Get_AllSellGoods(phone);
        }

        public List<PurchaseList> getAllSellList(string phone)
        {
            return rp.GetSellList(phone);
        }

        public List<PurchaseList> getPerGoodsSellList(string phone,int goods_id)
        {
            return rp.GetSellListPerGoods(phone, goods_id);
        }

        public Goods getSellGoods(string phone,int goods_id)
        {
            return rp.Get_SellGoods(phone, goods_id);
        }

        public int getAddGoodsId()
        {
            return rp.GetAddGoodsId();
        }

        public bool addGoods(string phone,string name,string tag,float price,string details,int stock,string img_path)
        {
            return rp.Add_Goods(phone, name,tag, price, details, stock,img_path);
        }

        public bool addImg(int goods_id,string img_path)
        {
            return rp.Add_Img(goods_id, img_path);
        }

        public bool modifyGoods(string phone,int goods_id, string name,string tag, float price, string details, int stock)
        {
            return rp.Modify_Goods(phone,goods_id, name,tag, price, details, stock);
        }

        public bool isInGoodsList(int goods_id)
        {
            return rp.IsInGoodsList(goods_id);
        }

        public void deleteGoods(int goods_id)
        {
            rp.Delete_Goods(goods_id);
        }

        public void reduceStock(int goods_id,int num)
        {
            rp.Reduce_Stock(goods_id, num);
        }

        public void addVolume(int goods_id,int num)
        {
            rp.Add_Volume(goods_id, num);
        }

        public float getAllIncome(string phone)
        {
            return rp.GetAllIncome(phone);
        }

        public float getIncomeByMonth(string phone,DateTime now)
        {
            return rp.GetIncomeByMonth(phone, now);
        }
    }
}
