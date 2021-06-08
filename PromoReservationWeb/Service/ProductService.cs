
using Microsoft.EntityFrameworkCore;
using PromoReservationWeb.Data;
using PromoReservationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoReservationWeb.Service
{
    public class ProductService : ServiceCoreContract<ProductMaster>, IProductMaster
    {

        public PromoOrderingContext context { get; set; }
        public ProductService(PromoOrderingContext cont) : base(cont)
        {
            context = cont;
        }

        //public override async Task<ProductMaster> GetDetails(Guid Id)
        //{
        //    var item = await base.GetDetails(Id);
        //    if(item != null)
        //    {
        //        context.Entry(item).State = EntityState.Detached;
        //    }
         
        //    return item;
        //}


    }

    public interface IProductMaster : IServiceCoreContract<ProductMaster>
    {

    }


}
