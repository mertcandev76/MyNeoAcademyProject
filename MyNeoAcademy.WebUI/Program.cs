using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using MyNeoAcademy.Application.Validators;
using MyNeoAcademy.Application.Validators.Auth;
using MyNeoAcademy.WebUI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// 🔐 Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Auth/Login/Index";
        opt.LogoutPath = "/Auth/Login/Logout";
        opt.AccessDeniedPath = "/Auth/Login/AccessDenied";
    });

// ✅ FluentValidation: Tüm validatorları tarat
builder.Services.AddFluentValidationAutoValidation();           // ModelState otomatik dolar
builder.Services.AddFluentValidationClientsideAdapters();       // Client-side validation
builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>(); //validator buradan yüklenecek

// 🔗 API Servislerini ekle
builder.Services.AddApiServices("https://localhost:7230/api/");

// ** IHttpContextAccessor servis eklemesi buraya **
builder.Services.AddHttpContextAccessor();

// MVC servisleri
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ------------------------------
// PIPELINE
// ------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Üretimde güvenlik için
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // ⬅️ Authentication önce olmalı
app.UseAuthorization();

// Areas route
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Varsayılan route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Özel örnek route (isteğe bağlı)
app.MapControllerRoute(
    name: "blogdetail",
    pattern: "Blog/Detail/{id?}",
    defaults: new { controller = "BlogDetail", action = "Detail" });

app.Run();











