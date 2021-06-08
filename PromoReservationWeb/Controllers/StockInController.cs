using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketingAssetSystem.Models;
using MarketingAssetSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace MarketingAssetSystem.Controllers
{
    public class StockInController : InventoryTransactionController<StockInTransaction>
    {
        private ISystemReferenceGeneratorService _gen { get; set; }
        public StockInController(IInventoryTransactionService<StockInTransaction> inv, ISystemReferenceGeneratorService gen, ISupplierService sup ) :base(inv, gen, sup)
        {
            _gen = gen;
        }
        internal override async Task onCreateProcess(StockInTransaction ent)
        {
            ent.Reference = await _gen.GenerateStockInReferenceAsync();        
        }
        public override void CreateExtraProcess(StockInTransaction ent)
        {
            ent.Status = "Open";
            if(_supp.IsExist(x=>x.Id == ent.ExternalEntityId))
            {
                ent.ExternalEntityId = ent.ExternalEntityDetails.Id;
                ent.ExternalEntityDetails = null;
            }

            if (ent.InventoryDetails != null)
            {
                foreach (var item in ent.InventoryDetails)
                {
                    item.ProductDetail = null;
                }
            }
        }
        internal override void ValidateModelState(StockInTransaction ent)
        {
          
        }
        internal override async Task UpdateExtraProcess(StockInTransaction ent)
        {
           if(_supp.IsExist(x=>x.Id == ent.ExternalEntityDetails.Id))
            {
                ent.ExternalEntityId = ent.ExternalEntityDetails.Id;
            }
            else
            {
              var d = await _supp.AddEntityAsync(ent.ExternalEntityDetails);
              ent.ExternalEntityId = d.Id;
            }

            if (ent.InventoryDetails != null)
            {
                foreach (var item in ent.InventoryDetails)
                {
                    item.ProductDetail = null;
                }
            }
        }
    }
}