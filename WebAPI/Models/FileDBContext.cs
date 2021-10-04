using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace WebAPI.Models
{
    public partial class FileDBContext : DbContext
    {
        public FileDBContext()
        {
        }

        public FileDBContext(DbContextOptions<FileDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AadharCard> AadharCard { get; set; }
        public virtual DbSet<PanCard> PanCard { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
       public virtual DbSet<SalarySlip> SalarySlip { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectsV13;Initial Catalog=VehicleLoanDB;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            

            modelBuilder.Entity<AadharCard>(entity =>
            {
                entity.Property(e => e.FileExtension).HasMaxLength(50);

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FilePath).HasMaxLength(500);

                entity.Property(e => e.MimeType).HasMaxLength(50);
            });
            modelBuilder.Entity<PanCard>(entity =>
            {
                entity.Property(e => e.FileExtension).HasMaxLength(50);

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FilePath).HasMaxLength(500);

                entity.Property(e => e.MimeType).HasMaxLength(50);
            });
            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.FileExtension).HasMaxLength(50);

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FilePath).HasMaxLength(500);

                entity.Property(e => e.MimeType).HasMaxLength(50);
            });
            modelBuilder.Entity<SalarySlip>(entity =>
            {
                entity.Property(e => e.FileExtension).HasMaxLength(50);

                entity.Property(e => e.FileName).HasMaxLength(500);

                entity.Property(e => e.FilePath).HasMaxLength(500);

                entity.Property(e => e.MimeType).HasMaxLength(50);
            });
           
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
