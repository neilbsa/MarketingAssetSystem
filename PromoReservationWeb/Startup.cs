using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromoReservationWeb.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PromoReservationWeb.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.Facebook;
using PromoReservationWeb.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Server.IIS;
using MarketingAssetSystem.Service;

namespace PromoReservationWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IProductMaster, ProductService>();
            services.AddScoped<IProductGroup, ProductGroupService>();
            services.AddScoped<ISystemReferenceGeneratorService, SystemReferenceGeneratorService>();
            // services.AddScoped(typeof(IInventoryTransactionService<>),typeof(InventoryTransactionService<>))();
            services.AddScoped(typeof(IInventoryTransactionService<>), typeof(InventoryTransactionService<>));
            services.AddScoped<IGroupProductDistinctionService, GroupProductDistinctionService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddTransient<IEmailSender, SystemEmailSender>();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<IISOptions>(options =>
            {
                options.AutomaticAuthentication = false;
                options.ForwardClientCertificate = false;
               
            });

            //services.AddTransient<IClaimsTransformation, ClaimsTransformer>();
            //services.AddAuthentication(IISServerDefaults.AuthenticationScheme);

            services.AddDbContext<PromoOrderingContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationAccountModel, ApplicationRoles>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<PromoOrderingContext>()
                .AddDefaultTokenProviders();





            services.AddDistributedMemoryCache();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
              //  options.CookieHttpOnly = true;
            });


          

            //services.AddAuthentication().AddFacebook(option =>
            //{


            //    var clientId = Environment.GetEnvironmentVariable("Authentication:Facebook:AppId", EnvironmentVariableTarget.Machine);
            //    var ClientSecret = Environment.GetEnvironmentVariable("Authentication:Facebook:AppSecret", EnvironmentVariableTarget.Machine);

            //    //var clientId = Configuration["Authentication:Facebook:AppId"];
            //    //var ClientSecret = Configuration["Authentication:Facebook:AppSecret"];



            //    option.AppId = clientId;
            //    option.AppSecret = ClientSecret;
            //    //option.Scope.Add("user_birthday");
            //    option.Scope.Add("public_profile");

            //    option.Fields.Add("birthday");
            //    option.Fields.Add("picture");
            //    option.Fields.Add("name");
            //    option.Fields.Add("email");
            //    option.Fields.Add("gender");

            //});

            //services.AddAuthentication().AddGoogle(configOptions =>
            //{
            //    var clientId = Environment.GetEnvironmentVariable("Authentication:Google:ClientId", EnvironmentVariableTarget.Machine);
            //    var ClientSecret = Environment.GetEnvironmentVariable("Authentication:Google:ClientSecret", EnvironmentVariableTarget.Machine);

            //    //var clientId = Configuration["Authentication:Google:ClientId"];
            //    //var ClientSecret = Configuration["Authentication:Google:ClientSecret"];


            //    configOptions.ClientId = clientId;
            //    configOptions.ClientSecret = ClientSecret;

            //});


   




            services.AddMemoryCache(memoryCacheOption => { memoryCacheOption.ExpirationScanFrequency = TimeSpan.FromHours(2); });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddRazorPagesOptions(options =>
            {
                options.AllowAreas = true;
                options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
            }).AddJsonOptions(options => { options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; });
         
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });


     



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, PromoOrderingContext cont, UserManager<ApplicationAccountModel> userManager, RoleManager<ApplicationRoles> roleManager)
        {





            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

           // app.UseFacebookAuthentication(new FacebookOptions() { });
            app.UseAuthentication();
            DatabaseSeeder seed = new DatabaseSeeder(userManager,roleManager);
            seed.Initialize(cont);




            app.UseSession();




            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
