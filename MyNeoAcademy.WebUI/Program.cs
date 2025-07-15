using FluentValidation;
using FluentValidation.AspNetCore;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.DTO.DTOs.ContactDTOs;

using MyNeoAcademy.DTO.Validators.CategoryValidator;
using MyNeoAcademy.DTO.Validators.SliderValidator;

var builder = WebApplication.CreateBuilder(args);


// Add FluentValidation servislerini IServiceCollection'a ekle
builder.Services.AddFluentValidationAutoValidation();  // ModelState otomatik dolar
builder.Services.AddFluentValidationClientsideAdapters();// Client-side validasyon için
builder.Services.AddValidatorsFromAssemblyContaining<CreateSliderValidator>();// Validator sınıflarını tarat



// MVC servislerini ekle (burada sadece AddControllersWithViews çağrılır)
builder.Services.AddControllersWithViews();




// 🔽 HttpClient servisi — API ile iletişim için
builder.Services.AddHttpClient("MyApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7230/api/");
    // İsteğe bağlı olarak default header vs. eklenebilir
    // client.DefaultRequestHeaders.Add("Accept", "application/json");
});

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

app.Run();
