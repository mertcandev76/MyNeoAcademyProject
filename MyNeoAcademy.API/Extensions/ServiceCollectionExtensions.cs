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
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseService, CourseManager>();

            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IBlogService, BlogManager>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            return services;
        }
    }
}
