using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PromoReservationWeb.Models
{
    public class OrderDetails : IBaseEntity
    {
        public Guid Id { get; set; }
        
        public Guid OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        public virtual OrderHeaderDetails OrderHeaderDetails { get; set; }


        public Guid ProductMasterId { get; set; }
        [ForeignKey("ProductMasterId")]
        public virtual ProductMaster ProductMasterDetails { get; set; }
        public int Hour { get; set; }
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
    }
}
