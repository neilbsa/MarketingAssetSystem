using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using PromoReservationWeb.Models;
using PromoReservationWeb.Service;

namespace PromoReservationWeb.Controllers
{


   


    public class HomeController : Controller
    {

        public IProductMaster _productMasterService { get; set; }
        public IMemoryCache _memCaching { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CacheList = "OrderedList";


        public HomeController(IProductMaster productServices, IMemoryCache mem , IHttpContextAccessor httpContextAccessor)
        {
            _productMasterService = productServices;
            _memCaching = mem;
            _httpContextAccessor = httpContextAccessor;

        }


        private List<ProductMasterWithHour> GetCacheList()
        {
            var cacheList = new List<ProductMasterWithHour>();
            string CurrentList;
            CurrentList = HttpContext.Session.GetString(CacheList);

            byte[] me =new byte[100];        


            List<ProductMasterWithHour> itm = new List<ProductMasterWithHour>();
            if (CurrentList != null)
            {
                cacheList = JsonConvert.DeserializeObject<List<ProductMasterWithHour>>(HttpContext.Session.GetString(CacheList));
            }
            else
            {               
                HttpContext.Session.SetString(CacheList, JsonConvert.SerializeObject(cacheList));
              //  _httpContextAccessor.HttpContext.User.Identity.Name
            }
            //  cacheList = ConvertToObject<List<ProductMasterWithHour>>();         
           
            return cacheList;
        }



        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            Response.Cookies.Append(key, value, option);
        }



        public void Remove(string key)
        {
            Response.Cookies.Delete(key);
        }


        


        public async Task<IActionResult> MyCart()
        {


            var cacheList = new List<ProductMasterWithHour>();
            cacheList = GetCacheList();



            var map = (from c in cacheList
                       join d in await _productMasterService.GetListAsync(x => cacheList.Select(j => j.Id).Contains(x.Id))
                      on c.Id equals d.Id
                       select new ProductMasterWithHour()
                       {
                           Id=d.Id,
                           Hours = c.Hours,
                           Model = d.Model,
                           Description = d.Description,
                           PartCode = d.PartCode,
                           SerialCode = d.SerialCode,
                           Quantity = c.Quantity
                       }).ToList();




            ViewBag.cartCount = cacheList == null ? 0 : cacheList.Count();
            return View(map);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteToCart(Guid Id)
        {
          //  int cartCount = 0;
            var cacheList = new List<ProductMasterWithHour>();
            var newCacheList = new List<ProductMasterWithHour>();
            cacheList = GetCacheList();
            bool isSuccess = false;
          

            if(cacheList.Count > 0)
            {
                newCacheList = cacheList;
                if (cacheList != null && cacheList.Count() > 0)
                {
                    var forDelete = newCacheList.Where(x => x.Id == Id).FirstOrDefault();
                    if (forDelete != null)
                    {
                        newCacheList.Remove(forDelete);
                    }

                 //   _httpContextAccessor.HttpContext.Session.Set(CacheList, ConvertToArray(newCacheList));
                    HttpContext.Session.SetString(CacheList, JsonConvert.SerializeObject(newCacheList));
                    isSuccess = true;
                }
            }
              
    
            return Json(new {success = isSuccess });
        }

     
        public async Task<IActionResult> Index()
        {
            var items = await _productMasterService.GetListAsync(x=>x.IsDeleted == false);
            int cartCount = 0;
            var cacheList = new List<ProductMasterWithHour>();
            if (!_memCaching.TryGetValue(CacheList, out cacheList))
            {

                cartCount = cacheList == null ? 0 : cacheList.Count();
            }
            else
            {
                cartCount = cacheList.Count();
            }
            ViewBag.cartCount = cartCount;
            return View(items);
        }


        [HttpPost]
        public async Task<IActionResult> AddItemToCookie(Guid ItemId, int hours, int Quantity)
        {
            var cacheList = new List<ProductMasterWithHour>();
            cacheList = GetCacheList();
            bool isSuccess = false;
            try
            {

                string temp;
                if (cacheList.Count() == 0)
                {


                    var newList = new List<ProductMasterWithHour>();
                    var item = await _productMasterService.GetDetails(ItemId);
                    if (item != null)
                    {
                        newList.Add(new ProductMasterWithHour() { Id = item.Id, Hours = hours, Quantity = Quantity });
                      
                        temp = JsonConvert.SerializeObject(newList);
                        HttpContext.Session.SetString(CacheList, temp);
                        isSuccess = true;
                    }

                }
                else
                {
                    var currentList = new List<ProductMasterWithHour>();
                    currentList = cacheList;
                    var item = await _productMasterService.GetDetails(ItemId);
                    if (item != null)
                    {
                        currentList.Add(new ProductMasterWithHour() { Id = item.Id, Hours = hours, Quantity = Quantity });
                        temp = JsonConvert.SerializeObject(currentList);
                        HttpContext.Session.SetString(CacheList, temp);
                        isSuccess = true;
                    }
                }




            }
            catch
            {
                isSuccess = false;
            }
            return Json(new { success = isSuccess });
        }
     

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
