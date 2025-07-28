    using FluentValidation;
    using FluentValidation.AspNetCore;
    using MyNeoAcademy.Application.Validators;
    using MyNeoAcademy.WebUI.Extensions;

    var builder = WebApplication.CreateBuilder(args);


    // Add FluentValidation servislerini IServiceCollection'a ekle
    builder.Services.AddFluentValidationAutoValidation();  // ModelState otomatik dolar
    builder.Services.AddFluentValidationClientsideAdapters();// Client-side validasyon için
    builder.Services.AddValidatorsFromAssemblyContaining<CreateSliderValidator>();// Validator sınıflarını tarat



    // MVC servislerini ekle (burada sadece AddControllersWithViews çağrılır)


    builder.Services.AddApiServices("https://localhost:7230/api/");

    builder.Services.AddControllersWithViews();


    // ------------------------------
    // UYGULAMA PIPELINE'I
    // ------------------------------
    var app = builder.Build();

    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts(); // Üretimde güvenlik için
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    // 🔽 Varsayılan route
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // 🔽 Areas desteği
    app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    app.MapControllerRoute(
        name: "blogdetail",
        pattern: "Blog/Detail/{id?}",
        defaults: new { controller = "BlogDetail", action = "Detail" });

    app.Run();
