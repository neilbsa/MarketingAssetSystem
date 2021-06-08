using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

using PromoReservationWeb.Data;
using PromoReservationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoReservationWeb.Service
{
    public class OrderServices : ServiceCoreContract<OrderHeaderDetails>, IOrderServices
    {
        public PromoOrderingContext context { get; set; }

        public OrderServices(PromoOrderingContext cont) : base(cont)
        {
            context = cont;
        }


        private Random random = new Random();
        public string GenerateConfirmationId()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }




    }

    public interface IOrderServices : IServiceCoreContract<OrderHeaderDetails>
    {
        string GenerateConfirmationId();
    }
}
