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
            public MyNeoAcademyContext(DbContextOptions<MyNeoAcademyContext> options)
                : base(options)
            {
            }

            public DbSet<About> Abouts { get; set; }
            public DbSet<AboutDetail> AboutDetails { get; set; }
            public DbSet<AboutFeature> AboutFeatures { get; set; }
            public DbSet<Author> Authors { get; set; }
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<BlogTag> BlogTags { get; set; }
            public DbSet<Category> Categories { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<Contact> Contacts { get; set; }
            public DbSet<Course> Courses { get; set; }
            public DbSet<Instructor> Instructors { get; set; }
            public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<RecentBlogPost> RecentBlogPosts { get; set; }
        public DbSet<Slider> Sliders { get; set; }
            public DbSet<Statistic> Statistics { get; set; }
            public DbSet<Tag> Tags { get; set; }
            public DbSet<Testimonial> Testimonials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // About - AboutFeature: One-to-Many
            modelBuilder.Entity<AboutFeature>()
                .HasOne(af => af.About)
                .WithMany(a => a.Features)
                .HasForeignKey(af => af.AboutID)
                .OnDelete(DeleteBehavior.Cascade);

            // AboutDetail: standalone, primary key konfigürasyonu
            modelBuilder.Entity<AboutDetail>()
                .HasKey(ad => ad.AboutDetailID);

            // Author - Blog: One-to-Many (nullable AuthorID)
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Blogs)
                .HasForeignKey(b => b.AuthorID)
                .OnDelete(DeleteBehavior.SetNull);

            // Blog - Category: One-to-Many (nullable CategoryID)
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Blogs)
                .HasForeignKey(b => b.CategoryID)
                .OnDelete(DeleteBehavior.SetNull);

            // BlogTag join entity: key + relations
            modelBuilder.Entity<BlogTag>()
                .HasKey(bt => bt.BlogTagID);

            modelBuilder.Entity<BlogTag>()
                .HasOne(bt => bt.Blog)
                .WithMany(b => b.BlogTags)
                .HasForeignKey(bt => bt.BlogID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BlogTag>()
                .HasOne(bt => bt.Tag)
                .WithMany(t => t.BlogTags)
                .HasForeignKey(bt => bt.TagID)
                .OnDelete(DeleteBehavior.Cascade);

            // RecentBlogPost: standalone
            modelBuilder.Entity<RecentBlogPost>()
                .HasKey(r => r.RecentBlogPostID);

            modelBuilder.Entity<RecentBlogPost>()
                .Property(r => r.CompactTitle)
                .IsRequired()
                .HasMaxLength(200); 

            // Category - Course: One-to-Many (nullable CategoryID)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Courses)
                .HasForeignKey(c => c.CategoryID)
                .OnDelete(DeleteBehavior.SetNull);

            // Course - Instructor: One-to-Many (nullable InstructorID)
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorID)
                .OnDelete(DeleteBehavior.SetNull);

            // İşte buraya ekleyin:
            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            // Comment - Blog: One-to-Many (non-nullable BlogID)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Blog)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BlogID)
                .OnDelete(DeleteBehavior.Cascade);

            // Contact: standalone
            modelBuilder.Entity<Contact>()
                .HasKey(c => c.ContactID);

            // Instructor: standalone + has many courses (configured above)
            modelBuilder.Entity<Instructor>()
                .HasKey(i => i.InstructorID);

            // Newsletter: standalone
            modelBuilder.Entity<Newsletter>()
                .HasKey(n => n.NewsletterID);

            modelBuilder.Entity<Newsletter>()
                .Property(n => n.Email)
                .IsRequired();

            // Slider: standalone
            modelBuilder.Entity<Slider>()
                .HasKey(s => s.SliderID);

            // Statistic: standalone
            modelBuilder.Entity<Statistic>()
                .HasKey(s => s.StatisticID);

            // Tag: standalone + has many BlogTags (configured above)
            modelBuilder.Entity<Tag>()
                .HasKey(t => t.TagID);

            modelBuilder.Entity<Tag>()
                .Property(t => t.Name)
                .IsRequired();

            // Testimonial: standalone
            modelBuilder.Entity<Testimonial>()
                .HasKey(t => t.TestimonialID);

            modelBuilder.Entity<Testimonial>()
                .Property(t => t.FullName)
                .IsRequired();

            // About: standalone + has many AboutFeatures (configured above)
            modelBuilder.Entity<About>()
                .HasKey(a => a.AboutID);

            // Blog: standalone + relations configured above
            modelBuilder.Entity<Blog>()
                .HasKey(b => b.BlogID);

            // Category: standalone + relations configured above
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryID);

            // Course: standalone + relations configured above
            modelBuilder.Entity<Course>()
                .HasKey(c => c.CourseID);

            // Comment: standalone + relation configured above
            modelBuilder.Entity<Comment>()
                .HasKey(c => c.CommentID);

        }

    }
}

