using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApplication1.Areas.TodoList.Models
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

        public virtual DbSet<Login2> Login2s { get; set; } = null!;
        public virtual DbSet<ToDoList> ToDoLists { get; set; } = null!;

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
            modelBuilder.Entity<Login2>(entity =>
            {
                entity.ToTable("Login2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Salt)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.User)
                    .HasMaxLength(50)
                    .HasColumnName("user");
            });

            modelBuilder.Entity<ToDoList>(entity =>
            {
                entity.ToTable("ToDoList");

                entity.Property(e => e.Titel).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
