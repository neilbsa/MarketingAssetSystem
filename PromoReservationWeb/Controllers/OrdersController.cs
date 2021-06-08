using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using PromoReservationWeb.Models;
using PromoReservationWeb.Service;

namespace PromoReservationWeb.Controllers
{

    [Authorize]
    public class OrdersController : Controller
    {



        public IProductMaster _productMasterService { get; set; }
        public IOrderServices _orderServices { get; set; }
        public IMemoryCache _memCaching { get; set; }
        public IEmailSender _emailSender { get; set; }
        public UserManager<ApplicationAccountModel> _accountsManager { get; set; }
        private const string CacheList = "OrderedList";
        public OrdersController(IProductMaster productServices,
            IMemoryCache mem, UserManager<ApplicationAccountModel> accountManager,
            IOrderServices order,
            IEmailSender emailSender
            )
        {
            _emailSender = emailSender;
            _productMasterService = productServices;
            _memCaching = mem;
            _orderServices = order;
            _accountsManager = accountManager;
        }







        private List<ProductMasterWithHour> GetCacheList()
        {
            var cacheList = new List<ProductMasterWithHour>();
            string CurrentList;
            CurrentList = HttpContext.Session.GetString(CacheList);

            byte[] me = new byte[100];


            List<ProductMasterWithHour> itm = new List<ProductMasterWithHour>();
            if (CurrentList != null)
            {
                cacheList = JsonConvert.DeserializeObject<List<ProductMasterWithHour>>(HttpContext.Session.GetString(CacheList));
            }
            else
            {

                HttpContext.Session.SetString(CacheList, JsonConvert.SerializeObject(cacheList));

            }
            //  cacheList = ConvertToObject<List<ProductMasterWithHour>>();         

            return cacheList;
        }



        public async Task<IActionResult> ProceedCheckout()
        {
            var cacheList = new List<ProductMasterWithHour>();
            cacheList = GetCacheList();
            if (cacheList.Count() > 0)
            {
                if (cacheList != null)
                {
                    var map = (from c in cacheList
                               join d in await _productMasterService.GetListAsync(x => cacheList.Select(j => j.Id).Contains(x.Id))
                              on c.Id equals d.Id
                               select new ProductMasterWithHour()
                               {
                                   Hours = c.Hours,
                                   Model = d.Model,
                                   Description = d.Description,
                                   PartCode = d.PartCode,
                                   SerialCode = d.SerialCode,
                                   Quantity = c.Quantity
                               }).ToList();
                    cacheList = map;
                }
                else
                {
                   
                }
            }
            ViewBag.cartCount = cacheList == null ? 0 : cacheList.Count();
            return View(cacheList);
        }



        public async Task<IActionResult> PlaceOrder()
        {
            ApplicationAccountModel user = new ApplicationAccountModel();
            user = await _accountsManager.FindByNameAsync(User.Identity.Name);
            OrderHeaderDetails mod = new OrderHeaderDetails();
            mod.CompanyName = user.CompanyName;
            mod.CustomerAddress = user.CompanyAddress;
            mod.CustomerName = user.Name;
            mod.FirstName = user.GivenName;
            mod.Surname = user.Surname;
            mod.ContactNumber = user.MobileNumber;
            mod.Email = user.Email;


            var cacheList = new List<ProductMasterWithHour>();
            var OrderList = new List<OrderDetails>();
            cacheList = GetCacheList();
            if (cacheList.Count() > 0)
            {
                if (cacheList != null)
                {
                    var map = (from c in cacheList
                               join d in await _productMasterService.GetListAsync(x => cacheList.Select(j => j.Id).Contains(x.Id))
                              on c.Id equals d.Id
                               select new OrderDetails()
                               {
                                   Hour = c.Hours,
                                   Quantity = c.Quantity,
                                   ProductMasterId = c.Id
                               }).ToList();
                    OrderList = map;
                }
            }

            mod.OrderLineItems = OrderList;


            var Item = await _orderServices.AddEntityAsync(mod);


            if (Item != null)
            {
                Item.ConfirmationCode = _orderServices.GenerateConfirmationId();
                await _orderServices.UpdateEntityAsync(Item, Item);
            }

            string bodyEmail = GenerateBody(Item);
            //SEND EMAIL
            if (Item.Email != null)
            {
               await _emailSender.SendEmailAsync(Item.Email, null, bodyEmail);
            }

            //delete all cachelist
            HttpContext.Session.SetString(CacheList, JsonConvert.SerializeObject(new List<ProductMasterWithHour>()));

            return View(Item);

        }

        private string GenerateBody(OrderHeaderDetails item)
        {
            string body = $@"
                <p><span style=""font-family: 'Arial',sans-serif;"">Dear { (item.CustomerName == null ? String.Format("{0} {1}",item.FirstName,item.Surname) : "")},</span></p>
                    <p><span style=""font-family: 'Arial',sans-serif;"">&nbsp;</span></p>
                    <p><span style=""font-family: 'Arial',sans-serif;"">Thank you for participating in our Genuine Volvo Filter Kit Promo! </span></p>
                    <p><span style=""font-family: 'Arial',sans-serif;"">We have received your order with order#: {item.ConfirmationCode.ToUpper()}. Your order will be forwarded to our sales team for processing.</span></p>
                    <p><span style=""font-family: 'Arial',sans-serif;"">&nbsp;</span></p>
                    <p><span style=""font-family: 'Arial',sans-serif;"">Kindly see your order details below:</span></p>
                    <p>&nbsp;</p>
                    <table style=""border-collapse: collapse; width: 100%; height: 17px;"" border=""1"">
                        <thead>

                     <tr style=""height: 17px;"">
                                        <td style=""width: 16.6667%; text-align: center; height: 17px;"">Model</td>
                                        <td style=""width: 16.6667%; text-align: center; height: 17px;"">Description</td>
                                        <td style=""width: 16.6667%; text-align: center; height: 17px;"">SerialCode</td>
                                        <td style=""width: 16.6667%; text-align: center; height: 17px;"">PartCode</td>
                                        <td style=""width: 16.6667%; height: 17px; text-align: center;"">Hours</td>
                                        <td style=""width: 16.6667%; height: 17px; text-align: center;"">Quantity</td>
                            </tr>  
                        </thead>

                    <tbody>
                                        



                    ";

            if(item.OrderLineItems != null)
            {
                foreach(var items in item.OrderLineItems)
                {
                    string d = $@"
<tr>
                                            <td style=""width: 16.6667%; text-align: center;"">{items.ProductMasterDetails.Model}</td>
                                            <td style=""width: 16.6667%; text-align: center;"">{items.ProductMasterDetails.Description}</td>
                                            <td style=""width: 16.6667%; text-align: center;"">{items.ProductMasterDetails.SerialCode}</td>
                                            <td style=""width: 16.6667%; text-align: center;"">{items.ProductMasterDetails.PartCode}</td>
                                            <td style=""width: 16.6667%; text-align: center;"">{items.Hour}</td>
                                            <td style=""width: 16.6667%; text-align: center;"">{items.Quantity}</td>
                                    </tr>
                            ";

                    body += d;

                }
                body += "</tbody></table>";
            }


            body += "<p>&nbsp;</p>";
            body += "<p>&nbsp;</p>";

            body += $@"
                    
                <p><span style=""font-family: 'Arial',sans-serif; color: #222222; background: white;"">For any questions/clarifications, please contact us at (632) 8924-2261, or send us an email at </span><span style=""font-family: 'Arial',sans-serif; background: white;""><a href=""mailto:info@civicmdsg.com.ph"">info@civicmdsg.com.ph</a></span> <span style=""font-family: 'Arial',sans-serif; color: #222222;""><br /><br /><br /></span></p>
                <p><span style=""font-family: 'Arial',sans-serif; color: #222222; background: white;"">Yours truly,</span></p>
                <p><span style=""font-family: 'Arial',sans-serif; color: #222222; background: white;"">Civic Merchandising, Inc. / Topspot Heavy Equipment, Inc.</span></p>

                    ";


            return body;
        }
    }
}