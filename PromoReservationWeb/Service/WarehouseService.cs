using PromoReservationWeb.Data;
using PromoReservationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingAssetSystem.Service
{
    public class WarehouseService : ServiceCoreContract<Warehouse>, IWarehouseService
    {
        public WarehouseService(PromoOrderingContext cont) : base(cont)
        {

        }
    }

  

    public interface IWarehouseService : IServiceCoreContract<Warehouse>
    {
     
    }
}
