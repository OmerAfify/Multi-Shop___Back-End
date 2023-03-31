using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;

namespace OnlineShopWebAPIs.BusinessLogic.DBContext
{
    public class OnlineShopDbContext : IdentityDbContext<IdentityUserContext>
    {
        public OnlineShopDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Tb_Products { get; set; }
        public DbSet<Category> Tb_Categories { get; set; }
        public DbSet<Review> Tb_Reviews { get; set; }
        public DbSet<ProductImage> Tb_ProductImages { get; set; }


        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<CartItem> CartItems { get; set; }



        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedItem> OrderedItems { get; set; }
        public DbSet<OrderDeliveryMethods> DeliveryMethods { get; set; }
        public DbSet<OrderStatus> OrderStatus { get; set; }
        public DbSet<Address> Address { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

       
            //Product Table Config
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.productName).HasMaxLength(100);
                entity.Property(e => e.description).HasMaxLength(250);
            });

            //Category Table Config
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.categoryName).HasMaxLength(100);
                entity.Property(e => e.categoryDescription).HasMaxLength(250);
            });


            //Review Table Config
            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.reviewDescription).HasMaxLength(250);
            });


            //Orders Config
            modelBuilder.Entity<Order>().OwnsOne(sa => sa.ShippingAddress, a => { a.WithOwner(); });
            modelBuilder.Entity<Order>().Property(p => p.SubTotal).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().Property(p => p.Total).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().HasMany(o => o.OrderedItems).WithOne().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderedItem>().OwnsOne(i => i.ProductItemOrdered, pio => { pio.WithOwner(); });
            modelBuilder.Entity<OrderedItem>().Property(p => p.TotalPrice).HasColumnType("decimal(18,2)");


            //shoppingCart config

            modelBuilder.Entity<CartItem>().HasKey(i => new { i.productId,i.ShoppingCartId} );


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
