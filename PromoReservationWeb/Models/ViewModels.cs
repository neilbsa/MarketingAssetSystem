using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PromoReservationWeb.Models
{
    public class SigninViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }

    [Serializable]
    public class ProductMasterWithHour
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string SerialCode { get; set; }
        public string Description { get; set; }
        public string PartCode { get; set; }
        public int Hours { get; set; }
        public int Quantity { get; set; }
    }

}
