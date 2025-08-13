using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyNeoAcademy.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;





//namespace MyNeoAcademy.DataAccess.Context
//{
//    public class MyNeoAcademyContext : IdentityDbContext<AppUser, AppRole, int,
//                        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
//                        IdentityRoleClaim<int>, IdentityUserToken<int>>
//    {
//        public MyNeoAcademyContext(DbContextOptions<MyNeoAcademyContext> options)
//            : base(options) { }

//        // DbSets
//        public DbSet<AppUser> AppUsers { get; set; }
//        public DbSet<AppRole> AppRoles { get; set; }
//        public DbSet<AppUserRole> AppUserRoles { get; set; }
//        public DbSet<About> Abouts { get; set; }
//        public DbSet<AboutDetail> AboutDetails { get; set; }
//        public DbSet<AboutFeature> AboutFeatures { get; set; }
//        public DbSet<Author> Authors { get; set; }
//        public DbSet<Blog> Blogs { get; set; }
//        public DbSet<BlogTag> BlogTags { get; set; }
//        public DbSet<Category> Categories { get; set; }
//        public DbSet<Comment> Comments { get; set; }
//        public DbSet<Contact> Contacts { get; set; }
//        public DbSet<Course> Courses { get; set; }
//        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
//        public DbSet<CourseLike> CourseLikes { get; set; }
//        public DbSet<Instructor> Instructors { get; set; }
//        public DbSet<Newsletter> Newsletters { get; set; }
//        public DbSet<RecentBlogPost> RecentBlogPosts { get; set; }
//        public DbSet<Slider> Sliders { get; set; }
//        public DbSet<Statistic> Statistics { get; set; }
//        public DbSet<Tag> Tags { get; set; }
//        public DbSet<Testimonial> Testimonials { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);

//            // AppUser - AppRole many-to-many
//            modelBuilder.Entity<AppUserRole>(entity =>
//            {
//                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

//                entity.HasOne(ur => ur.User)
//                      .WithMany(u => u.UserRoles)
//                      .HasForeignKey(ur => ur.UserId);

//                entity.HasOne(ur => ur.Role)
//                      .WithMany(r => r.UserRoles)
//                      .HasForeignKey(ur => ur.RoleId);
//            });

//            // AppUser - Author (1:1)
//            modelBuilder.Entity<AppUser>()
//                .HasOne(u => u.Author)
//                .WithOne(a => a.AppUser)
//                .HasForeignKey<Author>(a => a.AppUserID)
//                .OnDelete(DeleteBehavior.Cascade);

//            // AppUser - Instructor (1:1)
//            modelBuilder.Entity<AppUser>()
//                .HasOne(u => u.Instructor)
//                .WithOne(i => i.AppUser)
//                .HasForeignKey<Instructor>(i => i.AppUserID)
//                .OnDelete(DeleteBehavior.Cascade);

//            // AppUser - Comment (1:N)
//            modelBuilder.Entity<Comment>()
//                .HasOne(c => c.AppUser)
//                .WithMany(u => u.Comments)
//                .HasForeignKey(c => c.AppUserID)
//                .OnDelete(DeleteBehavior.SetNull);

//            // AppUser - Testimonial (1:N)
//            modelBuilder.Entity<Testimonial>()
//                .HasOne(t => t.AppUser)
//                .WithMany(u => u.Testimonials)
//                .HasForeignKey(t => t.AppUserID)
//                .OnDelete(DeleteBehavior.SetNull);

//            // AppUser - CourseLike (1:N) & Course - CourseLike (1:N)
//            modelBuilder.Entity<CourseLike>()
//                .HasKey(cl => new { cl.CourseID, cl.AppUserID });

//            modelBuilder.Entity<CourseLike>()
//                .HasOne(cl => cl.AppUser)
//                .WithMany(au => au.CourseLikes)
//                .HasForeignKey(cl => cl.AppUserID)
//                .OnDelete(DeleteBehavior.Cascade);

//            modelBuilder.Entity<CourseLike>()
//                .HasOne(cl => cl.Course)
//                .WithMany(c => c.CourseLikes)
//                .HasForeignKey(cl => cl.CourseID)
//                .OnDelete(DeleteBehavior.Cascade);

//            // AppUser - CourseEnrollment (1:N) & Course - CourseEnrollment (1:N)
//            modelBuilder.Entity<CourseEnrollment>()
//                .HasKey(ce => new { ce.CourseID, ce.AppUserID });

//            modelBuilder.Entity<CourseEnrollment>()
//                .HasOne(ce => ce.AppUser)
//                .WithMany(au => au.CourseEnrollments)
//                .HasForeignKey(ce => ce.AppUserID)
//                .OnDelete(DeleteBehavior.Cascade);

//            modelBuilder.Entity<CourseEnrollment>()
//                .HasOne(ce => ce.Course)
//                .WithMany(c => c.CourseEnrollments)
//                .HasForeignKey(ce => ce.CourseID)
//                .OnDelete(DeleteBehavior.Cascade);

//            // About - AboutFeature (1:N)
//            modelBuilder.Entity<AboutFeature>()
//                .HasOne(af => af.About)
//                .WithMany(a => a.Features)
//                .HasForeignKey(af => af.AboutID)
//                .OnDelete(DeleteBehavior.Cascade);

//            // Blog - Author (1:N)
//            modelBuilder.Entity<Blog>()
//                .HasOne(b => b.Author)
//                .WithMany(a => a.Blogs)
//                .HasForeignKey(b => b.AuthorID)
//                .OnDelete(DeleteBehavior.SetNull);

//            // Blog - Category (1:N)
//            modelBuilder.Entity<Blog>()
//                .HasOne(b => b.Category)
//                .WithMany(c => c.Blogs)
//                .HasForeignKey(b => b.CategoryID)
//                .OnDelete(DeleteBehavior.SetNull);

//            // BlogTag Config
//            modelBuilder.Entity<BlogTag>().HasKey(bt => bt.BlogTagID);

//            modelBuilder.Entity<BlogTag>()
//                .HasOne(bt => bt.Blog)
//                .WithMany(b => b.BlogTags)
//                .HasForeignKey(bt => bt.BlogID)
//                .OnDelete(DeleteBehavior.Cascade);

//            modelBuilder.Entity<BlogTag>()
//                .HasOne(bt => bt.Tag)
//                .WithMany(t => t.BlogTags)
//                .HasForeignKey(bt => bt.TagID)
//                .OnDelete(DeleteBehavior.Cascade);

//            // Course - Category (1:N)
//            modelBuilder.Entity<Course>()
//                .HasOne(c => c.Category)
//                .WithMany(cat => cat.Courses)
//                .HasForeignKey(c => c.CategoryID)
//                .OnDelete(DeleteBehavior.SetNull);

//            // Course - Instructor (1:N)
//            modelBuilder.Entity<Course>()
//                .HasOne(c => c.Instructor)
//                .WithMany(i => i.Courses)
//                .HasForeignKey(c => c.InstructorID)
//                .OnDelete(DeleteBehavior.SetNull);

//            modelBuilder.Entity<Course>()
//                .Property(c => c.Price)
//                .HasColumnType("decimal(18,2)");

//            // Comment - Blog (1:N)
//            modelBuilder.Entity<Comment>()
//                .HasOne(c => c.Blog)
//                .WithMany(b => b.Comments)
//                .HasForeignKey(c => c.BlogID)
//                .OnDelete(DeleteBehavior.Cascade);

//            // Primary Key Configurations
//            modelBuilder.Entity<About>().HasKey(a => a.AboutID);
//            modelBuilder.Entity<AboutDetail>().HasKey(ad => ad.AboutDetailID);
//            modelBuilder.Entity<Author>().HasKey(a => a.AuthorID);
//            modelBuilder.Entity<Blog>().HasKey(b => b.BlogID);
//            modelBuilder.Entity<Category>().HasKey(c => c.CategoryID);
//            modelBuilder.Entity<Comment>().HasKey(c => c.CommentID);
//            modelBuilder.Entity<Contact>().HasKey(c => c.ContactID);
//            modelBuilder.Entity<Course>().HasKey(c => c.CourseID);
//            modelBuilder.Entity<Instructor>().HasKey(i => i.InstructorID);
//            modelBuilder.Entity<Newsletter>().HasKey(n => n.NewsletterID);
//            modelBuilder.Entity<RecentBlogPost>().HasKey(r => r.RecentBlogPostID);
//            modelBuilder.Entity<Slider>().HasKey(s => s.SliderID);
//            modelBuilder.Entity<Statistic>().HasKey(s => s.StatisticID);
//            modelBuilder.Entity<Tag>().HasKey(t => t.TagID);
//            modelBuilder.Entity<Testimonial>().HasKey(t => t.TestimonialID);

//            // Property Configurations
//            modelBuilder.Entity<RecentBlogPost>()
//                .Property(r => r.CompactTitle)
//                .IsRequired()
//                .HasMaxLength(200);

//            modelBuilder.Entity<Newsletter>()
//                .Property(n => n.Email)
//                .IsRequired();

//            modelBuilder.Entity<Tag>()
//                .Property(t => t.Name)
//                .IsRequired();

//            modelBuilder.Entity<Testimonial>()
//                .Property(t => t.FullName)
//                .IsRequired();
//        }
//    }
//}

namespace MyNeoAcademy.DataAccess.Context
{
    public class MyNeoAcademyContext : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public MyNeoAcademyContext(DbContextOptions<MyNeoAcademyContext> options)
            : base(options) { }

        // DbSets
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
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
        public DbSet<CourseEnrollment> CourseEnrollments { get; set; }
        public DbSet<CourseLike> CourseLikes { get; set; }
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

            // Identity config
            modelBuilder.Entity<AppUserRole>(entity =>
            {
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });

                entity.HasOne(ur => ur.User)
                      .WithMany(u => u.UserRoles)
                      .HasForeignKey(ur => ur.UserId);

                entity.HasOne(ur => ur.Role)
                      .WithMany(r => r.UserRoles)
                      .HasForeignKey(ur => ur.RoleId);
            });

            // 1:1 Relationships
            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Author)
                .WithOne(a => a.AppUser)
                .HasForeignKey<Author>(a => a.AppUserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>()
                .HasOne(u => u.Instructor)
                .WithOne(i => i.AppUser)
                .HasForeignKey<Instructor>(i => i.AppUserID)
                .OnDelete(DeleteBehavior.Cascade);

            // 1:N Relationships
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.AppUser)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.AppUserID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Testimonial>()
                .HasOne(t => t.AppUser)
                .WithMany(u => u.Testimonials)
                .HasForeignKey(t => t.AppUserID)
                .OnDelete(DeleteBehavior.SetNull);

            // CourseLike
            modelBuilder.Entity<CourseLike>()
                .HasKey(cl => cl.Id);

            modelBuilder.Entity<CourseLike>()
                .HasIndex(cl => new { cl.CourseID, cl.AppUserID })
                .IsUnique();

            modelBuilder.Entity<CourseLike>()
                .HasOne(cl => cl.AppUser)
                .WithMany(u => u.CourseLikes)
                .HasForeignKey(cl => cl.AppUserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseLike>()
                .HasOne(cl => cl.Course)
                .WithMany(c => c.CourseLikes)
                .HasForeignKey(cl => cl.CourseID)
                .OnDelete(DeleteBehavior.Cascade);

            // CourseEnrollment
            modelBuilder.Entity<CourseEnrollment>()
                .HasKey(ce => ce.Id);

            modelBuilder.Entity<CourseEnrollment>()
                .HasIndex(ce => new { ce.CourseID, ce.AppUserID })
                .IsUnique();

            modelBuilder.Entity<CourseEnrollment>()
                .HasOne(ce => ce.AppUser)
                .WithMany(u => u.CourseEnrollments)
                .HasForeignKey(ce => ce.AppUserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CourseEnrollment>()
                .HasOne(ce => ce.Course)
                .WithMany(c => c.CourseEnrollments)
                .HasForeignKey(ce => ce.CourseID)
                .OnDelete(DeleteBehavior.Cascade);

            // About - AboutFeature
            modelBuilder.Entity<AboutFeature>()
                .HasOne(af => af.About)
                .WithMany(a => a.Features)
                .HasForeignKey(af => af.AboutID)
                .OnDelete(DeleteBehavior.Cascade);

            // Blog relations
            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Blogs)
                .HasForeignKey(b => b.AuthorID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Blog>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Blogs)
                .HasForeignKey(b => b.CategoryID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<BlogTag>().HasKey(bt => bt.BlogTagID);

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

            // Course relations
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Courses)
                .HasForeignKey(c => c.CategoryID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Course>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");

            // Comment - Blog
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Blog)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BlogID)
                .OnDelete(DeleteBehavior.Cascade);

            // Primary Keys
            modelBuilder.Entity<About>().HasKey(a => a.AboutID);
            modelBuilder.Entity<AboutDetail>().HasKey(ad => ad.AboutDetailID);
            modelBuilder.Entity<Author>().HasKey(a => a.AuthorID);
            modelBuilder.Entity<Blog>().HasKey(b => b.BlogID);
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryID);
            modelBuilder.Entity<Comment>().HasKey(c => c.CommentID);
            modelBuilder.Entity<Contact>().HasKey(c => c.ContactID);
            modelBuilder.Entity<Course>().HasKey(c => c.CourseID);
            modelBuilder.Entity<Instructor>().HasKey(i => i.InstructorID);
            modelBuilder.Entity<Newsletter>().HasKey(n => n.NewsletterID);
            modelBuilder.Entity<RecentBlogPost>().HasKey(r => r.RecentBlogPostID);
            modelBuilder.Entity<Slider>().HasKey(s => s.SliderID);
            modelBuilder.Entity<Statistic>().HasKey(s => s.StatisticID);
            modelBuilder.Entity<Tag>().HasKey(t => t.TagID);
            modelBuilder.Entity<Testimonial>().HasKey(t => t.TestimonialID);

            // Property Configs
            modelBuilder.Entity<RecentBlogPost>()
                .Property(r => r.CompactTitle)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Newsletter>()
                .Property(n => n.Email)
                .IsRequired();

            modelBuilder.Entity<Tag>()
                .Property(t => t.Name)
                .IsRequired();

            modelBuilder.Entity<Testimonial>()
                .Property(t => t.FullName)
                .IsRequired();
        }
    }
}
