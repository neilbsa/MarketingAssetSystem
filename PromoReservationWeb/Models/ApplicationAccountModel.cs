using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoReservationWeb.Models
{
    public class ApplicationAccountModel : IdentityUser
    {
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }




      

      
        public string MobileNumber { get; set; }
   
        public string CompanyName { get; set; }
       
        public string CompanyAddress { get; set; }

        public string Designation { get; set; }



    }

    public class ApplicationRoles : IdentityRole
    {
    }
}
