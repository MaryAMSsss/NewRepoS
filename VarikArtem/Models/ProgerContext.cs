using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VarikArtem.Models
{
    public partial class ProgerContext : DbContext
    {
        public ProgerContext()
        {
        }

        public ProgerContext(DbContextOptions<ProgerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Level> Levels { get; set; } = null!;
        public virtual DbSet<Programmer> Programmers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Proger;Trusted_Connection=True; TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .HasColumnName("company_name");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.ToTable("Level");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Programmer>(entity =>
            {
                entity.ToTable("Programmer");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Experience).HasColumnName("experience");

                entity.Property(e => e.IdCompany).HasColumnName("id_company");

                entity.Property(e => e.IdLevel).HasColumnName("id_level");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdCompanyNavigation)
                    .WithMany(p => p.Programmers)
                    .HasForeignKey(d => d.IdCompany)
                    .HasConstraintName("FK_Programmer_Company");

                entity.HasOne(d => d.IdLevelNavigation)
                    .WithMany(p => p.Programmers)
                    .HasForeignKey(d => d.IdLevel)
                    .HasConstraintName("FK_Programmer_Level");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IdProgrammer).HasColumnName("id_programmer");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.HasOne(d => d.IdProgrammerNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdProgrammer)
                    .HasConstraintName("FK_User_Programmer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
