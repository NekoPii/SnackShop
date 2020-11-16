using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopModel;

namespace ShopBusinessLogic
{
    public class MemberPurchase
    {
        private ShopRepository.MySQL.Pruchase_ShopRp rp = new ShopRepository.MySQL.Pruchase_ShopRp();

        public List<PurchaseCar> getPurchaseCarList(string mem_phone)
        {
            return rp.GetPurchaseCarLists(mem_phone);
        }

        public PurchaseCar getPurchaseCarItem(string mem_phone,int goods_id)
        {
            return rp.GetPurchaseCarItem(mem_phone, goods_id);
        }

        public List<WishList> getWishLists(string mem_phone)
        {
            return rp.GetWishLists(mem_phone);
        }

        public List<PurchaseList> getPurchaseLists(string mem_phone)
        {
            return rp.GetPurchaseLists(mem_phone);
        }

        public List<PurchaseList> getPurchaseListsBySearchId(string mem_phone,string search_id)
        {
            return rp.GetPurchaseListsBySearchId(mem_phone, search_id);
        }

        public List<Goods> getGoodsList()
        {
            return rp.GetGoodsList();
        }

        public List<Goods> getGoodsList(string input_name)
        {
            return rp.GetGoodsList(input_name);
        }

        public List<GoodsTag> getAllTags()
        {
            return rp.GetAllTags();
        }

        public List<GoodsImg> getGoodsImgs(int goods_id)
        {
            return rp.GetGoodsImgPath(goods_id);
        }

        public Goods getGoods(int goods_id)
        {
            return rp.GetGoods(goods_id);
        }

        public bool addWishList(string mem_phone,int goods_id)
        {
            return rp.AddWishList(mem_phone, goods_id);
        }

        public bool addPurchaseLists(string plist_id,string mem_phone,int goods_id,int num,DateTime dateTime,string seller_phone)
        {
            return rp.AddPurchaseLists(plist_id,mem_phone, goods_id, num, dateTime,seller_phone);
        }

        public bool addPurchaseCar(string mem_phone,int goods_id,int num)
        {
            return rp.AddPurchaseCar(mem_phone, goods_id, num);
        }

        public void updatePurchaseCar(string mem_phone,int goods_id,int num)
        {
            rp.UpdatePurchaseCar(mem_phone, goods_id, num);
        }

        public void deletePurchaseCar(string mem_phone,int goods_id)
        {
            rp.DeletePurchaseCar(mem_phone, goods_id);
        }

        public void deleteWishList(string mem_phone,int goods_id)
        {
            rp.DeleteWishList(mem_phone, goods_id);
        }
    }
}
