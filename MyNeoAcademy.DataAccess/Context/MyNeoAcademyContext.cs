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
            public DbSet<Slider> Sliders { get; set; }
            public DbSet<Statistic> Statistics { get; set; }
            public DbSet<Tag> Tags { get; set; }
            public DbSet<Testimonial> Testimonials { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // About
                modelBuilder.Entity<About>(entity =>
                {
                    entity.HasKey(a => a.AboutID);

                    entity.Property(a => a.Subtitle)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.Property(a => a.Title)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.Property(a => a.Description)
                        .IsRequired(false);

                    entity.Property(a => a.ButtonText)
                        .HasMaxLength(100)
                        .IsRequired(false);

                    entity.Property(a => a.ButtonLink)
                        .HasMaxLength(200)
                        .IsRequired(false);

                    entity.Property(a => a.ImageFrontUrl)
                        .HasMaxLength(500)
                        .IsRequired(false);

                    entity.Property(a => a.ImageBackUrl)
                        .HasMaxLength(500)
                        .IsRequired(false);

                    // About - AboutFeature 1 - N
                    entity.HasMany(a => a.Features)
                        .WithOne(f => f.About)
                        .HasForeignKey(f => f.AboutID)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                // AboutDetail
                modelBuilder.Entity<AboutDetail>(entity =>
                {
                    entity.HasKey(ad => ad.AboutDetailID);

                    entity.Property(ad => ad.Title)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.Property(ad => ad.Paragraph1)
                        .IsRequired(false);

                    entity.Property(ad => ad.Paragraph2)
                        .IsRequired(false);
                });

                // AboutFeature
                modelBuilder.Entity<AboutFeature>(entity =>
                {
                    entity.HasKey(af => af.AboutFeatureID);

                    entity.Property(af => af.IconClass)
                        .HasMaxLength(100)
                        .IsRequired(false);

                    entity.Property(af => af.Text)
                        .HasMaxLength(500)
                        .IsRequired(false);

                    entity.HasOne(af => af.About)
                        .WithMany(a => a.Features)
                        .HasForeignKey(af => af.AboutID)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                // Author
                modelBuilder.Entity<Author>(entity =>
                {
                    entity.HasKey(a => a.AuthorID);

                    entity.Property(a => a.Name)
                        .HasMaxLength(150)
                        .IsRequired();

                    entity.Property(a => a.Bio)
                        .IsRequired(false);

                    entity.Property(a => a.ImageUrl)
                        .HasMaxLength(500)
                        .IsRequired(false);

                    entity.Property(a => a.FacebookUrl)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.Property(a => a.TwitterUrl)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.Property(a => a.WebsiteUrl)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.HasMany(a => a.Blogs)
                        .WithOne(b => b.Author)
                        .HasForeignKey(b => b.AuthorID)
                        .OnDelete(DeleteBehavior.SetNull);
                });

                // Blog
                modelBuilder.Entity<Blog>(entity =>
                {
                    entity.HasKey(b => b.BlogID);

                    entity.Property(b => b.Title)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.Property(b => b.ShortDescription)
                        .HasMaxLength(500)
                        .IsRequired(false);

                    entity.Property(b => b.Content)
                        .IsRequired(false);

                    entity.Property(b => b.ImageUrl)
                        .HasMaxLength(500)
                        .IsRequired(false);

                    entity.Property(b => b.PublishDate)
                        .IsRequired();

                    entity.HasOne(b => b.Author)
                        .WithMany(a => a.Blogs)
                        .HasForeignKey(b => b.AuthorID)
                        .OnDelete(DeleteBehavior.SetNull);

                    entity.HasOne(b => b.Category)
                        .WithMany(c => c.Blogs)
                        .HasForeignKey(b => b.CategoryID)
                        .OnDelete(DeleteBehavior.SetNull);

                    entity.HasMany(b => b.Comments)
                        .WithOne(c => c.Blog)
                        .HasForeignKey(c => c.BlogID)
                        .OnDelete(DeleteBehavior.Cascade);

                    entity.HasMany(b => b.BlogTags)
                        .WithOne(bt => bt.Blog)
                        .HasForeignKey(bt => bt.BlogID)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                // BlogTag (Many to Many Blog - Tag)
                modelBuilder.Entity<BlogTag>(entity =>
                {
                    entity.HasKey(bt => bt.BlogTagID);

                    entity.HasOne(bt => bt.Blog)
                        .WithMany(b => b.BlogTags)
                        .HasForeignKey(bt => bt.BlogID)
                        .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne(bt => bt.Tag)
                        .WithMany(t => t.BlogTags)
                        .HasForeignKey(bt => bt.TagID)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                // Category
                modelBuilder.Entity<Category>(entity =>
                {
                    entity.HasKey(c => c.CategoryID);

                    entity.Property(c => c.Name)
                        .HasMaxLength(150)
                        .IsRequired(false);

                    entity.Property(c => c.Description)
                        .IsRequired(false);

                    entity.Property(c => c.IconClass)
                        .HasMaxLength(100)
                        .IsRequired(false);

                    entity.HasMany(c => c.Courses)
                        .WithOne(crs => crs.Category)
                        .HasForeignKey(crs => crs.CategoryID)
                        .OnDelete(DeleteBehavior.Cascade);

                    entity.HasMany(c => c.Blogs)
                        .WithOne(b => b.Category)
                        .HasForeignKey(b => b.CategoryID)
                        .OnDelete(DeleteBehavior.SetNull);
                });

                // Comment
                modelBuilder.Entity<Comment>(entity =>
                {
                    entity.HasKey(c => c.CommentID);

                    entity.Property(c => c.UserName)
                        .HasMaxLength(100)
                        .IsRequired();

                    entity.Property(c => c.Email)
                        .HasMaxLength(150)
                        .IsRequired(false);

                    entity.Property(c => c.Content)
                        .IsRequired(false);

                    entity.Property(c => c.CreatedDate)
                        .IsRequired();

                    entity.HasOne(c => c.Blog)
                        .WithMany(b => b.Comments)
                        .HasForeignKey(c => c.BlogID)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                // Contact
                modelBuilder.Entity<Contact>(entity =>
                {
                    entity.HasKey(c => c.ContactID);

                    entity.Property(c => c.Name)
                        .HasMaxLength(150)
                        .IsRequired();

                    entity.Property(c => c.Email)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.Property(c => c.Subject)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.Property(c => c.Message)
                        .IsRequired(false);

                    entity.Property(c => c.CreatedDate)
                        .IsRequired();
                });

                // Course
                modelBuilder.Entity<Course>(entity =>
                {
                    entity.HasKey(c => c.CourseID);

                    entity.Property(c => c.Title)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.Property(c => c.Description)
                        .IsRequired(false);

                    entity.Property(c => c.ImageUrl)
                        .HasMaxLength(500)
                        .IsRequired(false);

                    entity.Property(c => c.Rating)
                        .IsRequired();

                    entity.Property(c => c.ReviewCount)
                        .IsRequired();

                    entity.Property(c => c.StudentCount)
                        .IsRequired();

                    entity.Property(c => c.LikeCount)
                        .IsRequired();

                    entity.Property(c => c.Price)
                        .HasColumnType("decimal(18,2)")
                        .IsRequired(false);

                    entity.HasOne(c => c.Category)
                        .WithMany(cat => cat.Courses)
                        .HasForeignKey(c => c.CategoryID)
                        .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne(c => c.Instructor)
                        .WithMany(i => i.Courses)
                        .HasForeignKey(c => c.InstructorID)
                        .OnDelete(DeleteBehavior.SetNull);
                });

                // Instructor
                modelBuilder.Entity<Instructor>(entity =>
                {
                    entity.HasKey(i => i.InstructorID);

                    entity.Property(i => i.FullName)
                        .HasMaxLength(150)
                        .IsRequired();

                    entity.Property(i => i.Title)
                        .HasMaxLength(100)
                        .IsRequired(false);

                    entity.Property(i => i.Bio)
                        .IsRequired(false);

                    entity.Property(i => i.ImageUrl)
                        .HasMaxLength(500)
                        .IsRequired(false);

                    entity.Property(i => i.FacebookUrl)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.Property(i => i.TwitterUrl)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.Property(i => i.WebsiteUrl)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.HasMany(i => i.Courses)
                        .WithOne(c => c.Instructor)
                        .HasForeignKey(c => c.InstructorID)
                        .OnDelete(DeleteBehavior.SetNull);
                });

                // Slider
                modelBuilder.Entity<Slider>(entity =>
                {
                    entity.HasKey(s => s.SliderID);

                    entity.Property(s => s.SubTitle)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.Property(s => s.Title)
                        .HasMaxLength(250)
                        .IsRequired(false);

                    entity.Property(s => s.ButtonUrl)
                        .HasMaxLength(500)
                        .IsRequired(false);

                    entity.Property(s => s.ButtonText)
                        .HasMaxLength(100)
                        .IsRequired(false);

                    entity.Property(s => s.ImageUrl)
                        .HasMaxLength(500)
                        .IsRequired(false);
                });

                // Statistic
                modelBuilder.Entity<Statistic>(entity =>
                {
                    entity.HasKey(st => st.StatisticID);

                    entity.Property(st => st.SvgBase64)
                        .IsRequired(false);

                    entity.Property(st => st.ColorClass)
                        .HasMaxLength(100)
                        .IsRequired(false);

                    entity.Property(st => st.Count)
                        .IsRequired();

                    entity.Property(st => st.Label)
                        .HasMaxLength(150)
                        .IsRequired(false);
                });

                // Tag
                modelBuilder.Entity<Tag>(entity =>
                {
                    entity.HasKey(t => t.TagID);

                    entity.Property(t => t.Name)
                        .HasMaxLength(150)
                        .IsRequired();

                    entity.HasMany(t => t.BlogTags)
                        .WithOne(bt => bt.Tag)
                        .HasForeignKey(bt => bt.TagID)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                // Testimonial
                modelBuilder.Entity<Testimonial>(entity =>
                {
                    entity.HasKey(t => t.TestimonialID);

                    entity.Property(t => t.FullName)
                        .HasMaxLength(150)
                        .IsRequired();

                    entity.Property(t => t.Title)
                        .HasMaxLength(150)
                        .IsRequired(false);

                    entity.Property(t => t.ImageUrl)
                        .HasMaxLength(500)
                        .IsRequired(false);

                    entity.Property(t => t.Content)
                        .IsRequired(false);

                    entity.Property(t => t.Rating)
                        .IsRequired();
                });
            }
        }
    }

