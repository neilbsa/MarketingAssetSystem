using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoReservationWeb.Models
{
    public class OrderProductMaster : IChangeTracker
    {
        [ForeignKey("ProductOrderDetailsId")]
        public Guid Id { get; set; }

        public Guid ProductMasterId { get; set; }


        [ForeignKey("ProductMasterId")]
        public virtual ProductMaster ProductDetail { get; set; }

        public virtual OrderDetails ProductOrderDetailsId { get; set; }

     
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastDateModified { get; set; }
        public string CreateUser { get; set; }
        public string LastModifiedUser { get; set; }
        public bool IsDeleted { get; set; }
    }
}
