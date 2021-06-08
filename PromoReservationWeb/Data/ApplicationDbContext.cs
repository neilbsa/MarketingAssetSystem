using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PromoReservationWeb.Models;
using Microsoft.EntityFrameworkCore.Proxies;
using MarketingAssetSystem.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PromoReservationWeb.Data
{
    public class PromoOrderingContext : IdentityDbContext<ApplicationAccountModel,ApplicationRoles,string>
    {




        public DbSet<OrderHeaderDetails> Orders { get; set; }
        public DbSet<ProductGroup> ProductGroups { get; set; }
        public DbSet<OrderProductMaster> OrderProductMaster { get; set; }
        public DbSet<ProductMaster> ProductMaster { get; set; }
        public DbSet<OrderDetails> OrderLineItemDetails { get; set; }
        public DbSet<FileRepositoryModel> FileRepository { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<LookUpList> LookupLists { get; set; }
        public DbSet<StockInTransaction> StockIntransactions { get; set; }
        //public DbSet<SupplierDetail> SupplierDetails { get; set; }
        public DbSet<StockOutTransaction> StockOutTransactions { get; set; }
        public DbSet<ExternalEntity> ExternalEntities { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PromoOrderingContext(DbContextOptions<PromoOrderingContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        public Task AddChangeAuditLog(IEnumerable<PropertyEntry> ent)
        {
            string StateSaver = JsonConvert.SerializeObject(ent, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            Debug.WriteLine(StateSaver);

            return null;
        }
        private void AddDateStamp()
        {
            var ent = ChangeTracker.Entries<IChangeTracker>();

            string user = _httpContextAccessor?.HttpContext?.User.Identity.Name;
            foreach (var item in ent)
            {
               if(item.State == EntityState.Added)
                {
                    item.Entity.CreatedDate = DateTime.Now;
                    item.Entity.CreateUser = user == null ? "Administrator" : user;
                }
                else if(item.State == EntityState.Modified)
                {

                  //  var entss = item.Properties;
                 //   AddChangeAuditLog(item.Properties);
             
                    item.Entity.LastDateModified = DateTime.Now;
                    item.Entity.LastModifiedUser = user == null ? "Administrator" : user;
            
                }
            }
          
        }

        public override int SaveChanges()
        {
            AddDateStamp();
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.EnableSensitiveDataLogging();
            base.OnConfiguring(optionsBuilder);
        }



        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            AddDateStamp();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

    }
}
