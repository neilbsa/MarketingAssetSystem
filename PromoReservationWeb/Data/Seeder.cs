using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PromoReservationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoReservationWeb.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<ApplicationAccountModel> _userManager;
        private readonly RoleManager<ApplicationRoles> _roleManager;
        public DatabaseSeeder(UserManager<ApplicationAccountModel> userManager, RoleManager<ApplicationRoles> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async void Initialize(PromoOrderingContext context)
        {         
            

                context.Database.EnsureCreated();

                //if (!context.ProductGroups.Any())
                //{
                //    var model = new ProductGroup()
                //    {
                //        GroupName = "Volvo Wheelers",
                //        ProductDescription = "Description for Volvo wheelers",
                //        Products = new List<ProductMaster>() {
                //         new ProductMaster(){ Model="L60F", SerialCode="10041-, 514011-,506011-", Description="Volvo Wheelers Kit 1", PartCode="WL60F" },
                //         new ProductMaster(){ Model="L60F1", SerialCode="20042-, 414012-,406012-", Description="Volvo Wheelers Kit 2", PartCode="WL60F1" },
                //         new ProductMaster(){ Model="L60F2", SerialCode="30043-, 314013-,306013-", Description="Volvo Wheelers Kit 3", PartCode="WL60F2" },
                //         new ProductMaster(){ Model="L60F3", SerialCode="40044-, 214014-,206014-", Description="Volvo Wheelers Kit 4", PartCode="WL60F3" },
                //         new ProductMaster(){ Model="L60F4", SerialCode="50045-, 114015-,106015-", Description="Volvo Wheelers Kit 5", PartCode="WL60F4" },
                //     }

                //    };
                //    context.Add(model);
                //    context.SaveChanges();



                //    var model2 = new ProductGroup()
                //    {
                //        GroupName = "Volvo Wheelers Test1",
                //        ProductDescription = "Description for Volvo wheelers",
                //        Products = new List<ProductMaster>() {
                //         new ProductMaster(){ Model="TEST1L60F", SerialCode="10041-, 514011-,506011-", Description="Test1Volvo Wheelers Kit 1", PartCode="WL60F" },
                //         new ProductMaster(){ Model="Test1L60F1", SerialCode="20042-, 414012-,406012-", Description="Test1Volvo Wheelers Kit 2", PartCode="WL60F1" },
                //         new ProductMaster(){ Model="Test1L60F2", SerialCode="30043-, 314013-,306013-", Description="Test1Volvo Wheelers Kit 3", PartCode="WL60F2" },
                //         new ProductMaster(){ Model="Test1L60F3", SerialCode="40044-, 214014-,206014-", Description="Test1Volvo Wheelers Kit 4", PartCode="WL60F3" },
                //         new ProductMaster(){ Model="Test1L60F4", SerialCode="50045-, 114015-,106015-", Description="Test1Volvo Wheelers Kit 5", PartCode="WL60F4" },
                //     }

                //    };
                //    context.Add(model2);
                //    context.SaveChanges();


                //}





                if (!context.Roles.Any())
                {
                    var Admin = await _roleManager.RoleExistsAsync("Administrator");
                    if (!Admin)
                    {
                        var result = await _roleManager.CreateAsync(new ApplicationRoles() { Name = "Administrator" });
                        if (result.Succeeded)
                        {
                            //done
                        }
                    }

                }


                if (!_userManager.Users.Any())
                {
                    var user = new ApplicationAccountModel()
                    {
                        Email = "admin@civicmdsg.com.ph",
                        UserName = "admin@civicmdsg.com.ph",
                        LockoutEnabled = false,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        TwoFactorEnabled = false
                    };
                    var result = await _userManager.CreateAsync(user, "j04AD1140");




                var user2 = new ApplicationAccountModel()
                {
                    Email = "marketingadmin@civicmdsg.com.ph",
                    UserName = "marketingadmin@civicmdsg.com.ph",
                    LockoutEnabled = false,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false
                };
                var result2 = await _userManager.CreateAsync(user2, "P0rkAdob0!@#$");







                if (result.Succeeded)
                    {
                        var addToRole = await _userManager.AddToRoleAsync(user, "Administrator");
                    }




            

                if (result.Succeeded)
                {
                    var addToRole = await _userManager.AddToRoleAsync(user2, "Administrator");
                }


            }


            }



          
        
    }
}
