using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Negrea_Georgiana_MasterProiect.Models;

namespace Negrea_Georgiana_MasterProiect.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) :base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Boot> Boots { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<SoldBoot> SoldBoots { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable("Customer");
        modelBuilder.Entity<Order>().ToTable("Order");
        modelBuilder.Entity<Boot>().ToTable("Boot");
            modelBuilder.Entity<Seller>().ToTable("Seller");
            modelBuilder.Entity<SoldBoot>().ToTable("SoldBoot");
            modelBuilder.Entity<SoldBoot>()
            .HasKey(c => new { c.BootID, c.SellerID });//configureaza cheia primara compusa
    }
}
}

