using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Project_PRN211.Models
{
    public partial class Project_PRNContext : DbContext
    {
        public Project_PRNContext()
        {
        }

        public Project_PRNContext(DbContextOptions<Project_PRNContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<AddFriend> AddFriends { get; set; } = null!;
        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<BlogCategory> BlogCategories { get; set; } = null!;
        public virtual DbSet<BlogStatus> BlogStatuses { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<IsLike> IsLikes { get; set; } = null!;
        public virtual DbSet<StatusFriend> StatusFriends { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build(); optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Mobile).HasMaxLength(12);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            modelBuilder.Entity<AddFriend>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AddFriend");

                entity.Property(e => e.StatusFid).HasColumnName("StatusFId");

                entity.HasOne(d => d.Friend)
                    .WithMany()
                    .HasForeignKey(d => d.FriendId)
                    .HasConstraintName("FK__AddFriend__Frien__3A81B327");

                entity.HasOne(d => d.StatusF)
                    .WithMany()
                    .HasForeignKey(d => d.StatusFid)
                    .HasConstraintName("FK__AddFriend__Statu__3B75D760");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__AddFriend__UserI__398D8EEE");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blog");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Blog__AccountId__4222D4EF");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Blog__CategoryID__4316F928");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Blog__StatusId__440B1D61");
            });

            modelBuilder.Entity<BlogCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("PK__BlogCate__19093A2B67E83C49");

                entity.ToTable("BlogCategory");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(100);

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BlogStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__BlogStat__C8EE206339EF80A2");

                entity.ToTable("BlogStatus");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CmrepId).HasColumnName("cmrepID");

                entity.Property(e => e.Commentdate).HasColumnType("datetime");

                entity.Property(e => e.Isrep).HasColumnName("isrep");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK__Comment__Account__47DBAE45");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK__Comment__BlogId__46E78A0C");

               
            });

            

            modelBuilder.Entity<IsLike>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Blog)
                    .WithMany()
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK__IsLikes__BlogId__5CD6CB2B");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__IsLikes__UserId__5BE2A6F2");
            });

            modelBuilder.Entity<StatusFriend>(entity =>
            {
                entity.HasKey(e => e.StatusFid)
                    .HasName("PK__StatusFr__0825DB4C3239B30C");

                entity.ToTable("StatusFriend");

                entity.Property(e => e.StatusFid).HasColumnName("StatusFId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
