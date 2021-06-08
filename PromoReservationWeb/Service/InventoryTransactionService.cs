using MarketingAssetSystem.Models;
using PromoReservationWeb.Data;
using PromoReservationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingAssetSystem.Service
{
    public class InventoryTransactionService<IInventoryTransaction> :
    ServiceCoreContract<IInventoryTransaction>,
    IInventoryTransactionService<IInventoryTransaction> where IInventoryTransaction : InventoryTransaction,new()
    {
        public InventoryTransactionService(PromoOrderingContext cont) : base(cont)
        {

        }

        public async Task UpdateDetailsProcess(IInventoryTransaction newEntity)
        {
            var oldent = await GetDetails(newEntity.Id);

            oldent.ExternalEntityId = newEntity.ExternalEntityId != null ? newEntity.ExternalEntityId : oldent.ExternalEntityId;
            oldent.Reference = newEntity.Reference != null ? newEntity.Reference : oldent.Reference;
            oldent.WarehouseId = newEntity.WarehouseId != null ? newEntity.WarehouseId : oldent.WarehouseId;
            oldent.Status = newEntity.Status != null ? newEntity.Status : oldent.Status;

            var existingInvDetails = oldent?.InventoryDetails?.Select(x => x.Id).ToArray();
            var newInvDetails = newEntity.InventoryDetails.Select(x => x.Id).ToArray();
            //update InventoryDetails
            foreach (InventoryDetail item in newEntity.InventoryDetails)
            {
                if (existingInvDetails.Contains(item.Id))
                {
                    //update
                    var current = oldent.InventoryDetails.Where(x => x.Id == item.Id).First();
                    current.ProductMasterId = item.ProductMasterId;
                    current.Quantity = item.Quantity;

                }
                else
                {
                    //add
                    oldent.InventoryDetails.Add(item);
                }
            }

            foreach (InventoryDetail item in oldent.InventoryDetails)
            {
                if (!newInvDetails.Contains(item.Id))
                {
                    var current = oldent.InventoryDetails.Where(x => x.Id == item.Id).First();

                    current.IsDeleted = true;
                }
            }

            UpdateEntityAsync(oldent);
        }

     

    }


    public interface IInventoryTransactionService<IInventoryTransaction> : IServiceCoreContract<IInventoryTransaction> where IInventoryTransaction : InventoryTransaction
    {
        Task UpdateDetailsProcess(IInventoryTransaction newEntity);
    }


}
