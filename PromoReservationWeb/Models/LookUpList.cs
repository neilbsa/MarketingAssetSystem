using System;

namespace PromoReservationWeb.Models
{
    public class LookUpList : IBaseEntity
    {
        public Guid Id { get;set; }

        public string StringDesc1 { get; set; }
        public string StringDesc2 { get; set; }
        public string StringDesc3 { get; set; }

        public double DoubleDesc1 { get; set; }
        public double DoubleDesc2 { get; set; }
        public double DoubleDesc3 { get; set; }

       public Guid? TransactionId { get; set; }



        public bool IsDeleted { get;set; }
    }



}
