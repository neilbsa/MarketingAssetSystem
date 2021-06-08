using PromoReservationWeb.Data;
using PromoReservationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoReservationWeb.Service
{
    public class ProductGroupService : ServiceCoreContract<ProductGroup>, IProductGroup
    {
        public PromoOrderingContext context { get; set; }
        public ProductGroupService(PromoOrderingContext cont) : base(cont)
        {
            context = cont;
        }
    }

    public interface IProductGroup : IServiceCoreContract<ProductGroup>
    {

    }
}
