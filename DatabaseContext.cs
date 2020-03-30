using Lab5.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab5
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            String connectionString = "Server=localhost;" +
                                      "Port=5432;" +
                                      "Username=postgres;" +
                                      "Password=2x0x0x0;" +
                                      "Database=constructioncompany;";

            optionsBuilder.UseNpgsql(connectionString);
        }

        public DbSet<Brigade> Brigades { get; set; }

        public DbSet<MaterialsSet> MaterialsSets { get; set; }

        public DbSet<MaterialsType> MaterialsTypes { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Work> Works { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<WorkType> WorkTypes { get; set; }
    }
}
