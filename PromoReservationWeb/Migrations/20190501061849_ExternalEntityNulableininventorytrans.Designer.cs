﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PromoReservationWeb.Data;

namespace MarketingAssetSystem.Migrations
{
    [DbContext(typeof(PromoOrderingContext))]
    [Migration("20190501061849_ExternalEntityNulableininventorytrans")]
    partial class ExternalEntityNulableininventorytrans
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MarketingAssetSystem.Models.ExternalEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("CompanyName")
                        .IsRequired();

                    b.Property<string>("ContactNumber")
                        .IsRequired();

                    b.Property<string>("ContactPerson")
                        .IsRequired();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("EmailAddress");

                    b.Property<string>("EntityCode")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("ExternalEntities");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ExternalEntity");
                });

            modelBuilder.Entity("MarketingAssetSystem.Models.InventoryDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("InventoryTransactionId");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("ProductMasterId");

                    b.Property<double>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("InventoryTransactionId");

                    b.HasIndex("ProductMasterId");

                    b.ToTable("InventoryDetail");
                });

            modelBuilder.Entity("MarketingAssetSystem.Models.InventoryTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreateUser");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<Guid?>("ExternalEntityId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastDateModified");

                    b.Property<string>("LastModifiedUser");

                    b.Property<string>("Reference")
                        .IsRequired();

                    b.Property<string>("Status");

                    b.Property<Guid>("WarehouseId");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("InventoryTransaction");

                    b.HasDiscriminator<string>("Discriminator").HasValue("InventoryTransaction");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("PromoReservationWeb.Models.ApplicationAccountModel", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("CompanyAddress");

                    b.Property<string>("CompanyName");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime?>("DateOfBirth");

                    b.Property<string>("Designation");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("GivenName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("PromoReservationWeb.Models.ApplicationRoles", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("PromoReservationWeb.Models.FileRepositoryModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreateUser");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("FileName");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastDateModified");

                    b.Property<string>("LastModifiedUser");

                    b.Property<Guid>("TransactionId");

                    b.Property<byte[]>("byteContent");

                    b.Property<decimal>("contentLenght");

                    b.Property<string>("contentType");

                    b.HasKey("Id");

                    b.ToTable("FileRepository");

                    b.HasDiscriminator<string>("Discriminator").HasValue("FileRepositoryModel");
                });

            modelBuilder.Entity("PromoReservationWeb.Models.LookUpList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<double>("DoubleDesc1");

                    b.Property<double>("DoubleDesc2");

                    b.Property<double>("DoubleDesc3");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("StringDesc1");

                    b.Property<string>("StringDesc2");

                    b.Property<string>("StringDesc3");

                    b.Property<Guid?>("TransactionId");

                    b.HasKey("Id");

                    b.ToTable("LookupLists");

                    b.HasDiscriminator<string>("Discriminator").HasValue("LookUpList");
                });

            modelBuilder.Entity("PromoReservationWeb.Models.OrderDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Hour");

                    b.Property<bool>("IsDeleted");

                    b.Property<Guid>("OrderHeaderId");

                    b.Property<Guid>("ProductMasterId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderHeaderId");

                    b.HasIndex("ProductMasterId");

                    b.ToTable("OrderLineItemDetails");
                });

            modelBuilder.Entity("PromoReservationWeb.Models.OrderHeaderDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CompanyName");

                    b.Property<string>("ConfirmationCode");

                    b.Property<string>("ContactNumber");

                    b.Property<string>("CreateUser");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("CustomerAddress");

                    b.Property<string>("CustomerName");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastDateModified");

                    b.Property<string>("LastModifiedUser");

                    b.Property<string>("Remarks");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("PromoReservationWeb.Models.OrderProductMaster", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<string>("CreateUser");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastDateModified");

                    b.Property<string>("LastModifiedUser");

                    b.Property<Guid>("ProductMasterId");

                    b.Property<string>("Remarks");

                    b.HasKey("Id");

                    b.HasIndex("ProductMasterId");

                    b.ToTable("OrderProductMaster");
                });

            modelBuilder.Entity("PromoReservationWeb.Models.ProductGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GroupName");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ProductDescription");

                    b.HasKey("Id");

                    b.ToTable("ProductGroups");
                });

            modelBuilder.Entity("PromoReservationWeb.Models.ProductMaster", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreateUser");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<Guid>("GroupId");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastDateModified");

                    b.Property<string>("LastModifiedUser");

                    b.Property<string>("Model");

                    b.Property<string>("PartCode");

                    b.Property<double>("ReorderLevel");

                    b.Property<string>("SerialCode");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("ProductMaster");
                });

            modelBuilder.Entity("PromoReservationWeb.Models.Warehouse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreateUser");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastDateModified");

                    b.Property<string>("LastModifiedUser");

                    b.Property<Guid?>("ProductMasterId");

                    b.Property<string>("WarehouseCode");

                    b.Property<string>("WarehouseDescription");

                    b.HasKey("Id");

                    b.HasIndex("ProductMasterId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("MarketingAssetSystem.Models.Supplier", b =>
                {
                    b.HasBaseType("MarketingAssetSystem.Models.ExternalEntity");

                    b.HasDiscriminator().HasValue("Supplier");
                });

            modelBuilder.Entity("MarketingAssetSystem.Models.StockInTransaction", b =>
                {
                    b.HasBaseType("MarketingAssetSystem.Models.InventoryTransaction");

                    b.HasIndex("ExternalEntityId");

                    b.HasDiscriminator().HasValue("StockInTransaction");
                });

            modelBuilder.Entity("MarketingAssetSystem.Models.StockOutTransaction", b =>
                {
                    b.HasBaseType("MarketingAssetSystem.Models.InventoryTransaction");

                    b.HasDiscriminator().HasValue("StockOutTransaction");
                });

            modelBuilder.Entity("PromoReservationWeb.Models.ProductMasterImageDetail", b =>
                {
                    b.HasBaseType("PromoReservationWeb.Models.FileRepositoryModel");

                    b.HasIndex("TransactionId")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("ProductMasterImageDetail");
                });

            modelBuilder.Entity("PromoReservationWeb.Models.GroupProductDistinction", b =>
                {
                    b.HasBaseType("PromoReservationWeb.Models.LookUpList");

                    b.HasIndex("TransactionId");

                    b.HasDiscriminator().HasValue("GroupProductDistinction");
                });

            modelBuilder.Entity("MarketingAssetSystem.Models.InventoryDetail", b =>
                {
                    b.HasOne("MarketingAssetSystem.Models.InventoryTransaction", "InventoryTransactionDetails")
                        .WithMany("InventoryDetails")
                        .HasForeignKey("InventoryTransactionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PromoReservationWeb.Models.ProductMaster", "ProductDetail")
                        .WithMany()
                        .HasForeignKey("ProductMasterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MarketingAssetSystem.Models.InventoryTransaction", b =>
                {
                    b.HasOne("PromoReservationWeb.Models.Warehouse", "Warehouse")
                        .WithMany()
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("PromoReservationWeb.Models.ApplicationRoles")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("PromoReservationWeb.Models.ApplicationAccountModel")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("PromoReservationWeb.Models.ApplicationAccountModel")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("PromoReservationWeb.Models.ApplicationRoles")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PromoReservationWeb.Models.ApplicationAccountModel")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("PromoReservationWeb.Models.ApplicationAccountModel")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PromoReservationWeb.Models.OrderDetails", b =>
                {
                    b.HasOne("PromoReservationWeb.Models.OrderHeaderDetails", "OrderHeaderDetails")
                        .WithMany("OrderLineItems")
                        .HasForeignKey("OrderHeaderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PromoReservationWeb.Models.ProductMaster", "ProductMasterDetails")
                        .WithMany()
                        .HasForeignKey("ProductMasterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PromoReservationWeb.Models.OrderProductMaster", b =>
                {
                    b.HasOne("PromoReservationWeb.Models.OrderDetails", "ProductOrderDetailsId")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PromoReservationWeb.Models.ProductMaster", "ProductDetail")
                        .WithMany()
                        .HasForeignKey("ProductMasterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PromoReservationWeb.Models.ProductMaster", b =>
                {
                    b.HasOne("PromoReservationWeb.Models.ProductGroup", "GroupDetails")
                        .WithMany("Products")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PromoReservationWeb.Models.Warehouse", b =>
                {
                    b.HasOne("PromoReservationWeb.Models.ProductMaster")
                        .WithMany("Warehouses")
                        .HasForeignKey("ProductMasterId");
                });

            modelBuilder.Entity("MarketingAssetSystem.Models.StockInTransaction", b =>
                {
                    b.HasOne("MarketingAssetSystem.Models.Supplier", "ExternalEntityDetails")
                        .WithMany()
                        .HasForeignKey("ExternalEntityId");
                });

            modelBuilder.Entity("PromoReservationWeb.Models.ProductMasterImageDetail", b =>
                {
                    b.HasOne("PromoReservationWeb.Models.ProductMaster", "ProductMasterDetails")
                        .WithOne("ImageDetail")
                        .HasForeignKey("PromoReservationWeb.Models.ProductMasterImageDetail", "TransactionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PromoReservationWeb.Models.GroupProductDistinction", b =>
                {
                    b.HasOne("PromoReservationWeb.Models.ProductGroup", "ProductGroupDetails")
                        .WithMany("Distinctions")
                        .HasForeignKey("TransactionId");
                });
#pragma warning restore 612, 618
        }
    }
}
