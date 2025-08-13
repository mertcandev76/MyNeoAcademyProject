using Microsoft.Extensions.DependencyInjection;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Business.Concrete;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Repositories;
using MyNeoAcademy.Entity.Entities;
using MyNeoAcademy.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNeoAcademy.Business.DependencyResolvers
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services)
        {
            // Repositories

            services.AddScoped<ICourseLikeRepository, CourseLikeRepository>();
            services.AddScoped<ICourseEnrollmentRepository, CourseEnrollmentRepository>();
            services.AddScoped<ITestimonialRepository, TestimonialRepository>();
            services.AddScoped<IInstructorRepository, InstructorRepository>();
            services.AddScoped<IAboutFeatureRepository, AboutFeatureRepository>();
            services.AddScoped<IAboutRepository, AboutRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IBlogTagRepository, BlogTagRepository>();

            // Services


            services.AddScoped<ICourseLikeService, CourseLikeManager>();
            services.AddScoped<ICourseEnrollmentService, CourseEnrollmentManager>();
            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IRoleService, RoleManagerService>();
            services.AddScoped<IAboutDetailService, AboutDetailManager>();
            services.AddScoped<IRecentBlogPostService, RecentBlogPostManager>();
            services.AddScoped<INewsletterService, NewsletterManager>();
            services.AddScoped<ITestimonialService, TestimonialManager>();
            services.AddScoped<ISliderService, SliderManager>();
            services.AddScoped<IInstructorService, InstructorManager>();
            services.AddScoped<IAboutFeatureService, AboutFeatureManager>();
            services.AddScoped<IAboutService, AboutManager>();
            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ITagService, TagManager>();
            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<IAuthorService, AuthorManager>();
            services.AddScoped<ICourseService, CourseManager>();
            services.AddScoped<IBlogTagService, BlogTagManager>();

            // ✅ Auth & Token Services
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<ITokenService, TokenService>();

            // Generic
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));

            services.AddScoped(typeof(IGenericServiceWithoutUpdate<,,>), typeof(GenericManagerWithoutUpdate<,,>));
            services.AddScoped(typeof(IGenericService<,,,>), typeof(GenericManager<,,,>));

            return services;
        }


    }
}
