using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Areas.Database.Models
{
    public partial class TestDatabaseContext : DbContext
    {
        public TestDatabaseContext()
        {
        }

        public TestDatabaseContext(DbContextOptions<TestDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Login> Logins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Login");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Salt)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.User)
                    .HasMaxLength(50)
                    .HasColumnName("user");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
