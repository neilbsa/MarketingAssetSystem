using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketingAssetSystem.Service;
using Microsoft.AspNetCore.Mvc;
using PromoReservationWeb.Models;

namespace MarketingAssetSystem.Controllers
{
    public class WarehouseController : Controller
    {
        public IWarehouseService _warehouse { get; set; }
        public WarehouseController(IWarehouseService wh)
        {
            _warehouse = wh;
        }

        public async Task<IActionResult> Index()
        {
            var warehouses = await _warehouse.GetAllAsync(); 
            return View(warehouses);
        }

        public async Task<IActionResult> Details(Guid Id)
        {
            var warehouse = await _warehouse.GetDetails(Id);
            return View(warehouse);
        }

        [HttpPost]
        public async Task<IActionResult> GetWarehouses()
        {
            var wh = await _warehouse.GetAllAsync();
            if (wh != null)
            {
                return Ok((from d in wh
                          select d.WarehouseCode
                          ).ToArray());
            }
            return BadRequest("No Warehouse found");
        }

        [HttpPost]
        public IActionResult GetWarehouseDetails(string whCode)
        {
            var wh = _warehouse.GetDetails(x => x.WarehouseCode == whCode);
            if(wh != null)
            {
                return Ok(new { wh.Id, wh.WarehouseDescription });
            }
            return BadRequest("No Warehouse is found");
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Warehouse wh)
        {
            if (ModelState.IsValid)
            {
             var d = await  _warehouse.AddEntityAsync(wh);
            }
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Edit(Guid Id)
        {
            var forEdit = await _warehouse.GetDetails(Id);
            return View(forEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Warehouse ent)
        {
            if (ModelState.IsValid)
            {
                _warehouse.UpdateEntityAsync(ent, ent);
            }


            return RedirectToAction("Index");
        }



    }
}