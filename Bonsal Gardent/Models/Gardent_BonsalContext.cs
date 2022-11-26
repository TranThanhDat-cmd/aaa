using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bonsal_Gardent.Models
{
    public partial class Gardent_BonsalContext : DbContext
    {
        public Gardent_BonsalContext()
        {
        }

        public Gardent_BonsalContext(DbContextOptions<Gardent_BonsalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccCustomer> AccCustomers { get; set; } = null!;
        public virtual DbSet<AccManager> AccManagers { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Categogy> Categogies { get; set; } = null!;
        public virtual DbSet<CommentProduct> CommentProducts { get; set; } = null!;
        public virtual DbSet<CustomerOrder> CustomerOrders { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Picture> Pictures { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-1CCEOVD\\SQLEXPRESS;Initial Catalog=Gardent_Bonsal;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccCustomer>(entity =>
            {
                entity.ToTable("AccCustomer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccManager>(entity =>
            {
                entity.ToTable("AccManager");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccCustomerId).HasColumnName("AccCustomerID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.AccCustomer)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.AccCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cart__AccCustome__33D4B598");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Cart__ProductID__32E0915F");
            });

            modelBuilder.Entity<Categogy>(entity =>
            {
                entity.ToTable("Categogy");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<CommentProduct>(entity =>
            {
                entity.ToTable("CommentProduct");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccCustomerId).HasColumnName("AccCustomerID");

                entity.Property(e => e.AccManagerId).HasColumnName("AccManagerID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.AccCustomer)
                    .WithMany(p => p.CommentProducts)
                    .HasForeignKey(d => d.AccCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CommentPr__AccCu__3F466844");

                entity.HasOne(d => d.AccManager)
                    .WithMany(p => p.CommentProducts)
                    .HasForeignKey(d => d.AccManagerId)
                    .HasConstraintName("FK__CommentPr__AccMa__3E52440B");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CommentProducts)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__CommentPr__Produ__3D5E1FD2");
            });

            modelBuilder.Entity<CustomerOrder>(entity =>
            {
                entity.ToTable("CustomerOrder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccCustomerId).HasColumnName("AccCustomerID");

                entity.Property(e => e.CreateAtTime).HasColumnType("datetime");

                entity.HasOne(d => d.AccCustomer)
                    .WithMany(p => p.CustomerOrders)
                    .HasForeignKey(d => d.AccCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomerO__AccCu__36B12243");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AccCustomerId).HasColumnName("AccCustomerID");

                entity.Property(e => e.AccManagerId).HasColumnName("AccManagerID");

                entity.HasOne(d => d.AccCustomer)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.AccCustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedback__AccCus__4316F928");

                entity.HasOne(d => d.AccManager)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.AccManagerId)
                    .HasConstraintName("FK__Feedback__AccMan__4222D4EF");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CustomerOrderId).HasColumnName("CustomerOrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.CustomerOrder)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.CustomerOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderDeta__Custo__3A81B327");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderDeta__Produ__398D8EEE");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.ToTable("Picture");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Path)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Pictures)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Picture__Product__300424B4");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Place).HasMaxLength(12);

                entity.Property(e => e.Price)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Categor__2C3393D0");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__TypeID__2D27B809");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
