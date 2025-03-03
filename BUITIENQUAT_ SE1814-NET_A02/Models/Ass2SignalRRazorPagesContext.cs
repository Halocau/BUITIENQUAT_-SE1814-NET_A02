using System;
using System.Collections.Generic;
using Buitienquat_SE1814_NET_A02.Models;
using Microsoft.EntityFrameworkCore;

namespace BUITIENQUAT__SE1814_NET_A02.Models;

public partial class Ass2SignalRRazorPagesContext : DbContext
{
    public Ass2SignalRRazorPagesContext()
    {
    }

    public Ass2SignalRRazorPagesContext(DbContextOptions<Ass2SignalRRazorPagesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("server =(local); database =ASS2_SignalR_RazorPages; uid=sa;pwd=123;Encrypt=false");
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .HasColumnName("CustomerID");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsFixedLength();
        });

        modelBuilder.Entity<EmployeeDetail>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("address");
            entity.Property(e => e.Age)
                .HasMaxLength(50)
                .HasColumnName("age");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.OrderId)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .HasColumnName("CustomerID");
            entity.Property(e => e.TotalPayment).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderId)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ProductId)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Money).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.QuantityPerUnit).HasMaxLength(100);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
