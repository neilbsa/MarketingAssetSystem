using System.ComponentModel.DataAnnotations.Schema;

namespace PromoReservationWeb.Models
{
    public class GroupProductDistinction : LookUpList
    {
        [ForeignKey("TransactionId")]
        public virtual ProductGroup ProductGroupDetails { get; set; }
    }



}
