using Microsoft.EntityFrameworkCore;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.DataAccess.Context
{
    public class MyNeoAcademyContext : DbContext
    {
        // Sadece DbContextOptions alan constructor yeterli
        public MyNeoAcademyContext(DbContextOptions<MyNeoAcademyContext> options)
            : base(options)
        {
        }

        // DbSet'ler
        public DbSet<About> Abouts { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // About
            modelBuilder.Entity<About>(entity =>
            {
                entity.HasKey(e => e.AboutID);
                entity.Property(e => e.Description).IsRequired(false);
                entity.Property(e => e.ImageUrl1).IsRequired(false);
                entity.Property(e => e.ImageUrl2).IsRequired(false);
                entity.Property(e => e.Item1).IsRequired(false);
                entity.Property(e => e.Item2).IsRequired(false);
                entity.Property(e => e.Item3).IsRequired(false);
                entity.Property(e => e.Item4).IsRequired(false);
            });

            // Banner
            modelBuilder.Entity<Banner>(entity =>
            {
                entity.HasKey(e => e.BannerID);
                entity.Property(e => e.Title).IsRequired(false);
                entity.Property(e => e.ImageUrl).IsRequired(false);
            });

            // Blog
            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasKey(e => e.BlogID);

                entity.Property(e => e.Title).IsRequired(false);
                entity.Property(e => e.Content).IsRequired(false);
                entity.Property(e => e.ImageUrl).IsRequired(false);
                entity.Property(e => e.BlogDate).IsRequired();

                // Relation: Blog -> BlogCategory (many-to-one)
                entity.HasOne(e => e.BlogCategory)
                      .WithMany(c => c.Blogs)
                      .HasForeignKey(e => e.BlogCategoryID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // BlogCategory
            modelBuilder.Entity<BlogCategory>(entity =>
            {
                entity.HasKey(e => e.BlogCategoryID);
                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);
                entity.HasMany(e => e.Blogs)
                      .WithOne(b => b.BlogCategory)
                      .HasForeignKey(b => b.BlogCategoryID);
            });

            // Contact
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.ContactID);
                entity.Property(e => e.MapUrl).IsRequired(false);
                entity.Property(e => e.Address).IsRequired(false);
                entity.Property(e => e.Phone).IsRequired(false);
                entity.Property(e => e.Email).IsRequired(false);
            });

            // Course
            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseID);

                entity.Property(e => e.CourseName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.ImageUrl).IsRequired(false);
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)");

                entity.Property(e => e.IsShown)
                      .HasDefaultValue(false);

                entity.HasOne(e => e.CourseCategory)
                      .WithMany(c => c.Courses)
                      .HasForeignKey(e => e.CourseCategoryID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // CourseCategory
            modelBuilder.Entity<CourseCategory>(entity =>
            {
                entity.HasKey(e => e.CourseCategoryID);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Icon).IsRequired(false);
                entity.Property(e => e.Description).IsRequired(false);

                entity.Property(e => e.IsShown)
                      .HasDefaultValue(false);

                entity.HasMany(e => e.Courses)
                      .WithOne(c => c.CourseCategory)
                      .HasForeignKey(c => c.CourseCategoryID);
            });

            // Message
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.MessageID);
                entity.Property(e => e.Name).IsRequired(false);
                entity.Property(e => e.Subject).IsRequired(false);
                entity.Property(e => e.Content).IsRequired(false);
            });

            // SocialMedia
            modelBuilder.Entity<SocialMedia>(entity =>
            {
                entity.HasKey(e => e.SocialMediaID);
                entity.Property(e => e.Icon).IsRequired(false);
                entity.Property(e => e.IconUrl).IsRequired(false);
                entity.Property(e => e.Title).IsRequired(false);
            });

            // Subscriber
            modelBuilder.Entity<Subscriber>(entity =>
            {
                entity.HasKey(e => e.SubscriberID);

                entity.Property(e => e.Email)
                      .IsRequired(false);

                entity.Property(e => e.IsActive)
                      .HasDefaultValue(false);
            });

            // Testimonial
            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.HasKey(e => e.TestimonialID);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Title).IsRequired(false);
                entity.Property(e => e.ImageUrl).IsRequired(false);
                entity.Property(e => e.Comment).IsRequired(false);

                entity.Property(e => e.Star).IsRequired();
            });
        }

    }
}