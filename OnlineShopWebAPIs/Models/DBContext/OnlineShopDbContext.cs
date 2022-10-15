using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OnlineShopWebAPIs.Models.DBContext
{
    public class OnlineShopDbContext :DbContext
    {
        public OnlineShopDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Tb_Products { get; set; }
        public DbSet<Category> Tb_Categories { get; set; }
        public DbSet<Review> Tb_Reviews { get; set; }
        public DbSet<ProductImage> Tb_ProductImages { get; set; }

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


        } 
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
