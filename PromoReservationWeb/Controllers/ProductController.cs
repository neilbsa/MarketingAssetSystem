using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketingAssetSystem.Models;
using MarketingAssetSystem.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PromoReservationWeb.Models;
using PromoReservationWeb.Service;



namespace PromoReservationWeb.Controllers
{

    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller
    {
        private IProductGroup _prodGroup;
        private IProductMaster _productMaster;
        private IGroupProductDistinctionService _lookupList;
        private ISupplierService _supplier;
        public ProductController(
            IProductGroup prodGroup,
            IProductMaster productMaster, 
            IGroupProductDistinctionService lookupList, 
            ISupplierService supp)
        {
            _prodGroup = prodGroup;
            _productMaster = productMaster;
            _lookupList = lookupList;
            _supplier = supp;
        }

        public async Task<IActionResult> Edit(Guid Id)
        {
            var Entity = await _prodGroup.GetDetails(Id);
            Entity.Products = Entity.Products.Where(x => x.IsDeleted == false).ToList();

            return View(Entity);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductGroup ent)
        {

            if (ent.Products != null && ent.Products.Count() > 0)
            {
                foreach (var item in ent.Products)
                {

                    var currentItem = await _productMaster.GetDetails(item.Id);
                    if (currentItem != null && currentItem.IsDeleted == false)
                    {
                        if (item.ImageDetail != null && item.ImageDetail.ImageModel != null)
                        {

                            if (currentItem.ImageDetail == null)
                            {
                                ProductMasterImageDetail image = new ProductMasterImageDetail()
                                {

                                    byteContent = item.ImageDetail.convertFileToByte(),
                                    FileName = item.ImageDetail.ImageModel.FileName,
                                    contentLenght = item.ImageDetail.ImageModel.Length,
                                    contentType = item.ImageDetail.ImageModel.ContentType

                                };
                                currentItem.ImageDetail = image;
                            }
                            else
                            {
                                currentItem.ImageDetail.byteContent = item.ImageDetail.convertFileToByte();
                                currentItem.ImageDetail.FileName = item.ImageDetail.ImageModel.FileName;
                                currentItem.ImageDetail.contentLenght = item.ImageDetail.ImageModel.Length;
                                currentItem.ImageDetail.contentType = item.ImageDetail.ImageModel.ContentType;

                            }
                        }
                        await _productMaster.UpdateEntityAsync(currentItem, item);
                    }
                    else
                    {

                        ProductMasterImageDetail imageModel = new ProductMasterImageDetail();

                        if (item.ImageDetail != null && item.ImageDetail.ImageModel != null)
                        {
                            imageModel.byteContent = item.ImageDetail.convertFileToByte();
                            imageModel.FileName = item.ImageDetail.ImageModel.FileName;
                            imageModel.contentLenght = item.ImageDetail.ImageModel.Length;
                            imageModel.contentType = item.ImageDetail.ImageModel.ContentType;
                        }
                        item.GroupId = ent.Id;
                        item.ImageDetail = imageModel;
                        ProductMaster mod = new ProductMaster()
                        {
                        };
                        mod = item;
                        await _productMaster.AddEntityAsync(mod);
                    }
                }




                if (ent.Distinctions != null && ent.Distinctions.Count > 0)
                {
                    foreach (var dis in ent.Distinctions)
                    {
                        var item = await _lookupList.GetDetails(dis.Id);
                        if (item != null && item.IsDeleted == false)
                        {
                            //update
                            dis.TransactionId = ent.Id;
                            await _lookupList.UpdateEntityAsync(item, dis);
                        }
                        else
                        {
                            //add

                            dis.TransactionId = ent.Id;
                            await _lookupList.AddEntityAsync(dis);
                        }
                    }
                }






                var forDelete = await _productMaster.GetListAsync(x => x.IsDeleted != true && x.GroupId == ent.Id);
                foreach (var item in forDelete)
                {
                    if (!ent.Products.Select(x => x.Id).Contains(item.Id))
                    {
                        item.IsDeleted = true;
                        await _productMaster.UpdateEntityAsync(item, item);
                    }
                }



                var distinctionDelete = await _lookupList.GetListAsync(x => x.IsDeleted != true && x.TransactionId == ent.Id);

                if (ent.Distinctions == null)
                {
                    foreach (var forDeleteDis in distinctionDelete)
                    {

                        forDeleteDis.IsDeleted = true;
                        _lookupList.UpdateEntityAsync(forDeleteDis, forDeleteDis);

                    }
                }
                else
                {
                    foreach (var forDeleteDis in distinctionDelete)
                    {
                        if (!ent.Distinctions.Select(x => x.Id).Contains(forDeleteDis.Id))
                        {
                            forDeleteDis.IsDeleted = true;
                            _lookupList.UpdateEntityAsync(forDeleteDis, forDeleteDis);
                        }
                    }
                }







            }

            //currentState = ent;

            await _prodGroup.UpdateEntityAsync(ent, ent);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            var groups = await _prodGroup.GetAllAsync();


            return View(groups);
        }

        [HttpPost]
        public async Task<string[]> GetSupplier()
        {
            return (from d in await _supplier.GetAllAsync()
                    select d.EntityCode).ToArray();
        }
        [HttpPost]
        public  IActionResult GetSupplierDetails(string entityCode)
        {
            var supp = _supplier.GetDetails(x => x.EntityCode == entityCode);
            if(supp != null)
            {
                return Ok(
                            new {
                                    supp.CompanyName,
                                    supp.ContactNumber,
                                    supp.ContactPerson,
                                    supp.EmailAddress,
                                    supp.Address,
                                    supp.Id
                            }
                         );
            }

            return BadRequest("No Supplier found");
                   
        }
        [HttpPost]
        public async Task<string[]> GetProducts()
        {
            var result = await _productMaster.GetAllAsync();
            return result.Where(x => x.IsDeleted == false).Select(x => x.PartCode).ToArray();
        }
        [HttpPost]
        public IActionResult GetProductDetail(string PartCode)
        {
            var result = _productMaster.GetDetails(x => x.PartCode == PartCode);
            if (result != null)
            {
                var currentResult = new { Id = result.Id, Description = result.Description, Model = result.Model };
                return Ok(currentResult);
            }
            else
            {
               return BadRequest("No Partnumber Found");
            }
        }
      
        public IActionResult AddNewItem()
        {
            ProductMaster mod = new ProductMaster();

            return View("ProductGroupItem", mod);
        }
        public IActionResult GroupDistinctions()
        {
            return View("GroupDistinctionsCreate");
        }
        public IActionResult AddNewItemCreate()
        {
            ProductMaster mod = new ProductMaster();

            return View("ProductGroupItemCreate", mod);
        }
    
        public IActionResult Create()
        {
            ProductGroup mod = new ProductGroup();

            return View(mod);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductGroup mod)
        {


            if (mod.Products != null)
            {
                foreach (var item in mod.Products)
                {
                    if (item.ImageDetail.ImageModel != null)
                    {
                        item.ImageDetail.byteContent = item.ImageDetail.convertFileToByte();
                        item.ImageDetail.contentType = item.ImageDetail.ImageModel.ContentType;
                        item.ImageDetail.FileName = item.ImageDetail.ImageModel.FileName;
                        item.ImageDetail.contentLenght = item.ImageDetail.ImageModel.Length;
                    }

                }
            }




            await _prodGroup.AddEntityAsync(mod);





            return RedirectToAction("Index");
        }
    }
}