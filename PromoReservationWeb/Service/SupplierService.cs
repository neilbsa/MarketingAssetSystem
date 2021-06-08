using MarketingAssetSystem.Models;
using PromoReservationWeb.Data;
using PromoReservationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingAssetSystem.Service
{
    public class SupplierService :  ServiceCoreContract<Supplier>, ISupplierService
    {
        public PromoOrderingContext context { get; set; }
        public SupplierService(PromoOrderingContext cont) : base(cont)
        {
            context = cont;
        }
    }

    public interface ISupplierService : IServiceCoreContract<Supplier>
    {

    }
}
