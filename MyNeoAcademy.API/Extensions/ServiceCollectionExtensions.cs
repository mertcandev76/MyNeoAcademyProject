using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.Business.Concrete;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Repositories;

namespace MyNeoAcademy.API.Extensions
{
    //-->Manuel
    //Dependency Injection(Manuel) 
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection services)
        {
            // Scoped servis kayıtları

            services.AddScoped<IBlogTagRepository, BlogTagRepository>();
            services.AddScoped<IBlogTagService, BlogTagManager>();

            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagManager>();

            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentManager>();

            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogService, BlogManager>();


            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryManager>();


            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAuthorService, AuthorManager>();


            services.AddScoped<IAboutRepository, AboutRepository>();
            services.AddScoped<IAboutService, AboutManager>();


            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseService, CourseManager>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            return services;
        }
    }
}
