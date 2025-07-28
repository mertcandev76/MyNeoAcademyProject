using MyNeoAcademy.WebUI.ApiServices.Abstract;
using MyNeoAcademy.WebUI.ApiServices.Concrete;

namespace MyNeoAcademy.WebUI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, string baseApiUrl)
        {
            services.AddHttpClient("MyApiClient", client =>
            {
                client.BaseAddress = new Uri(baseApiUrl);
            });

            services.AddScoped<INewsletterApiService, NewsletterApiService>();
            services.AddScoped<ITestimonialApiService, TestimonialApiService>();
            services.AddScoped<ISliderApiService, SliderApiService>();
            services.AddScoped<ICourseApiService, CourseApiService>();
            services.AddScoped<IInstructorApiService, InstructorApiService>();
            services.AddScoped<ICommentApiService, CommentApiService>();
            services.AddScoped<IBlogTagApiService, BlogTagApiService>();
            services.AddScoped<ITagApiService, TagApiService>();
            services.AddScoped<IBlogApiService, BlogApiService>();
            services.AddScoped<ICategoryApiService, CategoryApiService>();
            services.AddScoped<IAuthorApiService, AuthorApiService>();
            services.AddScoped<IAboutApiService, AboutApiService>();
            services.AddScoped<IAboutFeatureApiService, AboutFeatureApiService>();

            return services;
        }
    }
}
