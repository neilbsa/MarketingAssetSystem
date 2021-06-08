using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketingAssetSystem.Models;
using MarketingAssetSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace MarketingAssetSystem.Controllers
{
    public abstract class InventoryTransactionController<ITransactionEntity> : Controller where ITransactionEntity : InventoryTransaction,new()
    {
        public IInventoryTransactionService<ITransactionEntity> _inv { get; set; }
        public ISystemReferenceGeneratorService _reference { get; set; }
        public ISupplierService _supp { get; set; }

        internal abstract Task onCreateProcess(ITransactionEntity ent);
        internal abstract void ValidateModelState(ITransactionEntity ent);
        internal abstract Task UpdateExtraProcess(ITransactionEntity ent);


        public InventoryTransactionController(
            IInventoryTransactionService<ITransactionEntity> inv, 
            ISystemReferenceGeneratorService refere, ISupplierService supp
          )
        {
            _reference = refere;
            _inv = inv;
            _supp = supp;


        }
        public async Task<IActionResult> Index()
        {
            var lists = await _inv.GetAllAsync();
            return View(lists);
        }
        //public async Task<IActionResult> GetSupplierDetails()
        //{
        //    var suppliers =await _supp.GetAllAsync();
        //    if(suppliers != null)
        //    {
        //        return Ok(from d in suppliers
        //                  select new { code = d.EntityCode });
        //    }

        //    return BadRequest("No Supplier ")


        //}
        public async  Task<IActionResult> Edit(Guid id)
        {
            var mod = await _inv.GetDetails(id);
            return View(mod);
        }
        [HttpPost]
        public IActionResult Edit(ITransactionEntity ent)
        {            
            if (!ModelState.IsValid)
            {
                return View(ent);
            }
            UpdateExtraProcess(ent);
            _inv.UpdateDetailsProcess(ent);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Create()
        {
            ITransactionEntity mod = new ITransactionEntity();
            await onCreateProcess(mod);
            return View(mod);
        }
        public virtual void CreateExtraProcess(ITransactionEntity ent)
        {

        }
        [HttpPost]
        public async Task<IActionResult> Create(ITransactionEntity ent)
        {
          
            ValidateModelState(ent);
            if (!ModelState.IsValid)
            {
                return View(ent);
            }
            CreateExtraProcess(ent);
            await _inv.AddEntityAsync(ent);
            return RedirectToAction("Index");
        }
        public IActionResult Details(Guid Id)
        {
            var ent = _inv.GetDetails(Id);
            return View(ent);
        }       
        public IActionResult AddNewItem() {
            return View();
        }
    }
}