using System;
using System.Collections.Generic;

namespace PromoReservationWeb.Models
{
    public class ProductGroup : IBaseEntity
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public string ProductDescription { get; set; }
        public virtual ICollection<ProductMaster> Products { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<GroupProductDistinction> Distinctions {get;set;}
    }



}
