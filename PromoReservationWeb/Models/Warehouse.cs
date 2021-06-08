using System;

namespace PromoReservationWeb.Models
{
    public class Warehouse : IChangeTracker
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }

        public string WarehouseCode { get; set; }
        public string WarehouseDescription { get; set; }
    

        public DateTime CreatedDate { get; set; }
        public DateTime LastDateModified { get; set; }
        public string CreateUser { get; set; }
        public string LastModifiedUser { get; set; }

    }

}
