using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PromoReservationWeb.Models
{

    public class ProductMaster : IChangeTracker
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string SerialCode { get; set; }
        public string Description { get; set; }
        public string PartCode { get; set; }
        public Guid GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual ProductGroup GroupDetails { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }
        public double ReorderLevel { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime LastDateModified { get; set; }
        public string CreateUser { get; set; }
        public string LastModifiedUser { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ProductMasterImageDetail ImageDetail { get; set; }
    }

}
