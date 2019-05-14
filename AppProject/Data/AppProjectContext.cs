using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppProject.Models;

namespace AppProject.Models
{
    public class AppProjectContext : DbContext
    {

        public AppProjectContext (DbContextOptions<AppProjectContext> options)
            : base(options)
        {
        }

        public DbSet<AppProject.Models.Categories> Categories { get; set; }

        public DbSet<AppProject.Models.SubCategory> SubCategory { get; set; }

        public DbSet<AppProject.Models.Productes> Productes { get; set; }

        public DbSet<AppProject.Models.Customer> Customer { get; set; }

        public DbSet<AppProject.Models.Mart> Mart { get; set; }

        public DbSet<AppProject.Models.Colors> Colors { get; set; }

        public DbSet<AppProject.Models.Sizes> Sizes { get; set; }

        public DbSet<AppProject.Models.ConectTable> ConectTable { get; set; }

    }
}
