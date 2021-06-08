using System;
using System.Collections.Generic;

namespace PromoReservationWeb.Models
{
    public class OrderHeaderDetails : IChangeTracker
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CompanyName { get; set; }
        public string ContactNumber { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string ConfirmationCode { get; set; }
        public virtual ICollection<OrderDetails> OrderLineItems { get; set; }
        public string Remarks { get; set; }
        public DateTime LastDateModified { get; set; }
        public string CreateUser { get; set; }
        public string LastModifiedUser { get; set; }
        public bool IsDeleted { get; set; }
    }
}
