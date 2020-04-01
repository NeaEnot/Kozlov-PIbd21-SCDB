using Lab5.Models;
using Microsoft.EntityFrameworkCore;
using System;

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
                                      "Database=constructioncompany;";

            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("seq_brigade").IncrementsBy(1);
            modelBuilder.HasSequence<int>("seq_materials").IncrementsBy(1);
            modelBuilder.HasSequence<int>("seq_materialstype").IncrementsBy(1);
            modelBuilder.HasSequence<int>("seq_order").IncrementsBy(1);
            modelBuilder.HasSequence<int>("seq_position").IncrementsBy(1);
            modelBuilder.HasSequence<int>("seq_work").IncrementsBy(1);
            modelBuilder.HasSequence<int>("seq_worker").IncrementsBy(1);
            modelBuilder.HasSequence<int>("seq_worktype").IncrementsBy(1);

            modelBuilder.Entity<Brigade>(entity => { entity.Property(e => e.Id).UseHiLo("seq_brigade"); });
            modelBuilder.Entity<MaterialsSet>(entity => { entity.Property(e => e.Id).UseHiLo("seq_materials"); });
            modelBuilder.Entity<MaterialsType>(entity => { entity.Property(e => e.Id).UseHiLo("seq_materialstype"); });
            modelBuilder.Entity<Order>(entity => { entity.Property(e => e.Id).UseHiLo("seq_order"); });
            modelBuilder.Entity<Position>(entity => { entity.Property(e => e.Id).UseHiLo("seq_position"); });
            modelBuilder.Entity<Work>(entity => { entity.Property(e => e.Id).UseHiLo("seq_work"); });
            modelBuilder.Entity<Worker>(entity => { entity.Property(e => e.Id).UseHiLo("seq_worker"); });
            modelBuilder.Entity<WorkType>(entity => { entity.Property(e => e.Id).UseHiLo("seq_worktype"); });
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
