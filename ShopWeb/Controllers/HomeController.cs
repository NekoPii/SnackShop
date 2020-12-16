using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using ShopWeb.Models;
using PagedList;

namespace ShopWeb.Controllers
{
    public class HomeController : Controller
    {
        public static int getNum(int now, int[] nums, int minVal, int maxVal, Random rand)
        {
            for (int i = 0; i < nums.Length; ++i)
            {
                if (now == nums[i])
                {
                    now = rand.Next(minVal, maxVal);
                    getNum(now, nums, minVal, maxVal, rand);
                }
            }
            return now;
        }

        [HttpGet]
        public ActionResult Index(int? page)
        {
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            string search_tag = this.Request.QueryString["search_tag"];
            string search_price = this.Request.QueryString["search_price"];
            Regex regex1 = new Regex("^[0-9]*[1-9][0-9]*$");
            Regex regex2 = new Regex("^\\d+_\\d+$");
            if (search_price != null && !regex1.IsMatch(search_price) && !regex2.IsMatch(search_price) && search_price != "all") return Redirect("/Error");
            var total_goods_list = memberPurchase.getGoodsList().Select(goods_info => new MemberPurchaseCarViewModel()
            {
                goods_id = goods_info.goods_id,
                goods_name = goods_info.goods_name,
                goods_img_path = goods_info.goods_img_path,
                unit_price = goods_info.goods_price,
                sell_stock = goods_info.goods_stock,
                sell_volume = goods_info.goods_volume,
                seller_phone = goods_info.seller_phone,
            }).ToList();
            List<MemberPurchaseCarViewModel> rand_goods_list = new List<MemberPurchaseCarViewModel>();
            //轮播图显示
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks));
            int[] randNum = new int[22];
            if (total_goods_list.Count <= 10) rand_goods_list = total_goods_list;
            else
            {
                for (int i = 0; i < 22; ++i)
                {
                    int tmp = rand.Next(0, total_goods_list.Count);
                    randNum[i] = getNum(tmp, randNum, 0, total_goods_list.Count, rand);
                    if (i < 10) rand_goods_list.Add(total_goods_list[randNum[i]]);
                }
            }
            //购买记录推荐
            List<MemberPurchaseCarViewModel> ai_goods_list = new List<MemberPurchaseCarViewModel>();
            if (Session["mem_phone"] == null)
            {
                for (int i = 10; i < 16; ++i)
                {
                    ai_goods_list.Add(total_goods_list[randNum[i]]);
                }
            }
            else
            {
                string mem_phone = Session["mem_phone"].ToString();
                Hashtable tagTable = new Hashtable();
                var aiTagList = memberPurchase.getPurchaseTag_ai(mem_phone);
                for (int i = 0; i < aiTagList.Count; ++i)
                {
                    if (!tagTable.ContainsKey(aiTagList[i].tag)) tagTable.Add(aiTagList[i].tag, 1);
                    else tagTable[aiTagList[i].tag] = Convert.ToInt32(tagTable[aiTagList[i].tag]) + 1;
                }

                ICollection keys = tagTable.Keys;
                foreach (string now_tag in keys)
                {
                    var nowAiList = memberPurchase.getGoodsList(now_tag, Convert.ToInt32(tagTable[now_tag])).Select(goods_info => new MemberPurchaseCarViewModel()
                    {
                        goods_id = goods_info.goods_id,
                        goods_name = goods_info.goods_name,
                        goods_img_path = goods_info.goods_img_path,
                        unit_price = goods_info.goods_price,
                        sell_stock = goods_info.goods_stock,
                        sell_volume = goods_info.goods_volume,
                        seller_phone = goods_info.seller_phone,
                    }).ToList();
                    ai_goods_list.AddRange(nowAiList);
                }
                int now = ai_goods_list.Count, index = 10;
                for (int i = 0; i < 6 - now && index < 22;)
                {
                    bool flag = true;
                    for (int j = 0; j < ai_goods_list.Count; ++j)
                    {
                        if (ai_goods_list[j].goods_id == total_goods_list[randNum[index]].goods_id)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        ai_goods_list.Add(total_goods_list[randNum[index]]);
                        ++i;
                    }
                    index++;
                }
            }

            var search_goods_list = new List<MemberPurchaseCarViewModel>();
            if ((search_price == null || search_price == "all" || search_price == "") && (search_tag == null || search_tag == "all" || search_tag == ""))
            {
                search_goods_list = memberPurchase.getGoodsList().Select(goods_info => new MemberPurchaseCarViewModel()
                {
                    goods_id = goods_info.goods_id,
                    goods_name = goods_info.goods_name,
                    goods_img_path = goods_info.goods_img_path,
                    unit_price = goods_info.goods_price,
                    sell_stock = goods_info.goods_stock,
                    sell_volume = goods_info.goods_volume,
                    seller_phone = goods_info.seller_phone,
                }).ToList();
            }
            else
            {
                search_goods_list = memberPurchase.getGoodsList(search_tag, search_price).Select(goods_info => new MemberPurchaseCarViewModel()
                {
                    goods_id = goods_info.goods_id,
                    goods_name = goods_info.goods_name,
                    goods_img_path = goods_info.goods_img_path,
                    unit_price = goods_info.goods_price,
                    sell_stock = goods_info.goods_stock,
                    sell_volume = goods_info.goods_volume,
                    seller_phone = goods_info.seller_phone,
                }).ToList();
            }
            int pageNumber = page ?? 1;
            int pageSize = 12;
            var tag_list = memberPurchase.getAllTags().Select(tag => new GoodsTag()
            {
                goods_tag = tag.tag,
            }).ToList();
            int _index;
            string tmp_select_price;
            if (search_price == "all" || search_price == null || search_price == "") tmp_select_price = "";
            else
            {
                _index = search_price.IndexOf('_');
                if (_index == -1)
                {
                    tmp_select_price = "￥" + search_price + " 以上";
                }
                else
                {
                    tmp_select_price = "￥" + search_price.Substring(0, _index) + " ~ ￥" + search_price.Substring(_index + 1, search_price.Length - _index - 1);
                }
            }
            var resView = new PurchaseHomeTotalInfo()
            {
                search_price = search_price,
                search_tag = search_tag,
                select_price_interval = tmp_select_price,
                select_tag = (search_tag == "all" || search_tag == "" || search_tag == null) ? "" : search_tag,
                total_goods_list = total_goods_list,
                goods_tags = tag_list,
                rand_display_goods_list = rand_goods_list,
                ai_display_goods_list = ai_goods_list,
                page_goods_list = (PagedList<MemberPurchaseCarViewModel>)search_goods_list.ToPagedList(pageNumber, pageSize),
            };
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("HomePart1", resView) : View(resView);
        }

        public ActionResult SearchHome()
        {
            return View();
        }
        /*
        public ActionResult Search()
        {
            return View();
        }*/
        /*
        [HttpPost]
        public ActionResult Search(string search_content)
        {
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            var goods_list = memberPurchase.getGoodsList(search_content).Select(goods_info => new MemberPurchaseCarViewModel()
            {
                goods_id = goods_info.goods_id,
                goods_name = goods_info.goods_name,
                goods_img_path = goods_info.goods_img_path,
                unit_price = goods_info.goods_price,
                sell_stock = goods_info.goods_stock,
                sell_volume = goods_info.goods_volume,
                seller_phone = goods_info.seller_phone,
            }).ToList();
            var resView = new PurchaseHomeTotalInfo()
            {
                total_goods_list = goods_list,
                search_name=search_content,
            };
            return View(resView);
        }
        */

        [HttpGet]
        public ActionResult Search(int? page)
        {
            string search_content = this.Request.QueryString["search_content"];
            if (search_content == null) return Redirect("/Error");
            else if (search_content == "" || search_content.Trim() == "") return Redirect("/Home/SearchHome");
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            var goods_list = memberPurchase.getGoodsList(search_content).Select(goods_info => new MemberPurchaseCarViewModel()
            {
                goods_id = goods_info.goods_id,
                goods_name = goods_info.goods_name,
                goods_img_path = goods_info.goods_img_path,
                unit_price = goods_info.goods_price,
                sell_stock = goods_info.goods_stock,
                sell_volume = goods_info.goods_volume,
                seller_phone = goods_info.seller_phone,
            }).ToList();
            int pageNumber = page ?? 1;
            int pageSize = 12;
            var resView = new PurchaseHomeTotalInfo()
            {
                total_goods_list = goods_list,
                search_content = search_content,
                page_goods_list = (PagedList<MemberPurchaseCarViewModel>)goods_list.ToPagedList(pageNumber, pageSize),
            };
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("HomePart2", resView) : View(resView);
        }
    }
}