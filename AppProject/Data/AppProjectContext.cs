using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppProject.Models;
using AppProject.ViewModel;

namespace AppProject.Models
{
    public class AppProjectContext : DbContext
    {

        public AppProjectContext (DbContextOptions<AppProjectContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //one to one
            modelBuilder.Entity<Customer>()
           .HasOne<Mart>(s => s.Mart)
           .WithOne(ad => ad.Customer)
           .HasForeignKey<Mart>(ad => ad.Id);

            //one to many
            modelBuilder.Entity<ConnectTable>()
                .HasKey(t => new { t.ProductesId, t.ColorId, t.SizeId, t.MartId });

            modelBuilder.Entity<ConnectTable>()
                .HasOne(pt => pt.Productes)
                .WithMany(p => p.Details)
                .HasForeignKey(pt => pt.ProductesId);

            modelBuilder.Entity<ConnectTable>()
                .HasOne(pt => pt.Color)
                .WithMany(t => t.Details)
                .HasForeignKey(pt => pt.ColorId);

            modelBuilder.Entity<ConnectTable>()
              .HasOne(pt => pt.Size)
              .WithMany(t => t.Details)
              .HasForeignKey(pt => pt.SizeId);

            modelBuilder.Entity<ConnectTable>()
              .HasOne(pt => pt.Mart)
              .WithMany(t => t.Details)
              .HasForeignKey(pt => pt.MartId);

    

        }


        public DbSet<AppProject.Models.SubCategory> SubCategory { get; set; }

        public DbSet<AppProject.Models.Customer> Customer { get; set; }

        public DbSet<AppProject.Models.Mart> Mart { get; set; }

        public DbSet<AppProject.Models.ConnectTable> ConnectTable { get; set; }

        public DbSet<AppProject.Models.Sizes> Sizes { get; set; }

        public DbSet<AppProject.Models.Colors> Colors { get; set; }

        public DbSet<AppProject.Models.Productes> Productes { get; set; }


        public DbSet<AppProject.Models.Categories> Categories { get; set; }



    }
}
