using System.ComponentModel.DataAnnotations.Schema;

namespace PromoReservationWeb.Models
{
    public class ProductMasterImageDetail : FileRepositoryModel
    {
        [ForeignKey("TransactionId")]
        public virtual ProductMaster ProductMasterDetails { get; set; }
    }

}
