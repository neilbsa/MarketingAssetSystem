using PromoReservationWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MarketingAssetSystem.Models
{
    public class InventoryTransaction : IChangeTracker
    {
        [Key]
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public string Reference { get; set; }
        [Required]
        public Guid WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }
        public Guid? ExternalEntityId { get; set; }
        //[ForeignKey("ExternalEntityId")]
        //public virtual ExternalEntity ExternalEntityDetails { get; set; }
        public string Status { get; set; }
        public virtual ICollection<InventoryDetail> InventoryDetails { get; set; }
        public DateTime CreatedDate { get;set; }
        public DateTime LastDateModified { get;set; }
        public string CreateUser { get;set; }
        public string LastModifiedUser { get;set; }
   
    }


    public class StockInTransaction : InventoryTransaction
    {
        [ForeignKey("ExternalEntityId")]
        public virtual Supplier ExternalEntityDetails { get; set; }
    }
    public class StockOutTransaction : InventoryTransaction
    {
        //[ForeignKey("ExternalEntityId")]
        //public virtual CustomerDetail CustomerDetail { get; set; }
    }

    public class Supplier : ExternalEntity
    {

    }

    public class ExternalEntity : IBaseEntity
    {

        public Guid Id { get;set; }
        [Required]
        public string EntityCode { get; set; }

        public bool IsDeleted { get;set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string ContactPerson { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        [Required]
        public string Address { get; set; }
        //public Guid InvTransactionId { get; set; }
        ////[ForeignKey("InvTransactionId")]
        //public virtual ICollection<InventoryTransaction> Transactions { get; set; }
    }




    public class InventoryDetail : IBaseEntity
    {
        public Guid Id { get;set; }
        public bool IsDeleted { get;set; }
        public Guid InventoryTransactionId { get; set; }
        [ForeignKey("InventoryTransactionId")]
        public virtual InventoryTransaction InventoryTransactionDetails { get; set; }
        public Guid ProductMasterId { get; set; }
        [ForeignKey("ProductMasterId")]
        public virtual ProductMaster ProductDetail { get; set; }
        public double Quantity { get; set; }
    }



}
