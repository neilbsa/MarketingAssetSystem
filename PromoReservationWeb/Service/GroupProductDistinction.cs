using PromoReservationWeb.Data;
using PromoReservationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingAssetSystem.Service
{
    public class GroupProductDistinctionService : ServiceCoreContract<GroupProductDistinction>, IGroupProductDistinctionService
    {
        public GroupProductDistinctionService(PromoOrderingContext cont) : base(cont)
        {

        }
    }



    public interface IGroupProductDistinctionService : IServiceCoreContract<GroupProductDistinction>
    {

    }




    public class SystemReferenceGeneratorService : ServiceCoreContract<LookUpList>, ISystemReferenceGeneratorService
    {

   
        public SystemReferenceGeneratorService(PromoOrderingContext cont) : base(cont)
        {
          
        }


        public async Task<string> GenerateStockInReferenceAsync()
        {
            string ReferenceType = "StockIn";
            double currCount = 0;
            if (!IsExist(x => x.StringDesc1 == ReferenceType))
            {
                LookUpList mod = new LookUpList()
                {
                    StringDesc1 = ReferenceType,
                    DoubleDesc1 = 1
                };
                currCount = 1;
           await AddEntityAsync(mod);
            }
            else
            {
                var count = GetDetails(x => ReferenceType.Equals(x.StringDesc1));
                currCount = count.DoubleDesc1 + 1;
                count.DoubleDesc1 = currCount;
                UpdateEntityAsync(count, count);
            };          
            return String.Format("{0}{1:00000}", "SI", currCount);
        }





    }



    public interface ISystemReferenceGeneratorService : IServiceCoreContract<LookUpList>
    {
        Task<string> GenerateStockInReferenceAsync();
    }
}
