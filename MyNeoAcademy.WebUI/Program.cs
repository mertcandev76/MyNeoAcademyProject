using FluentValidation;
using MyNeoAcademy.DTO.DTOs.BlogDTOs;
using MyNeoAcademy.DTO.DTOs.BlogCategoryDTOs;
using MyNeoAcademy.DTO.DTOs.ContactDTOs;
using MyNeoAcademy.WebUI.Validators.BlogCategoryValidator;
using MyNeoAcademy.WebUI.Validators.BlogValidator;
using MyNeoAcademy.WebUI.Validators.ContactValidator;
using MyNeoAcademy.WebUI.Validators.AboutValidator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllersWithViews();

// Validator'ı manuel olarak kaydet

//-->🔁 CreateAboutValidator herhangi bir validator'ı temsil ediyor. Aynı assembly'de oldukları için onun üzerinden tüm validator'lar taranır.
builder.Services.AddValidatorsFromAssemblyContaining<CreateAboutValidator>();

//builder.Services.AddScoped<IValidator<CreateContactDTO>, CreateContactValidator>();
//builder.Services.AddScoped<IValidator<UpdateContactDTO>, UpdateContactValidator>();


//builder.Services.AddScoped<IValidator<CreateBlogDTO>, CreateBlogDTOValidator>();
//builder.Services.AddScoped<IValidator<UpdateBlogDTO>, UpdateBlogDTOValidator>();

//builder.Services.AddScoped<IValidator<CreateBlogCategoryDTO>, CreateBlogCategoryValidator>();
//builder.Services.AddScoped<IValidator<UpdateBlogCategoryDTO>, UpdateBlogCategoryValidator>();


//-->Manuel
//HttpClient servis kaydı

builder.Services.AddHttpClient("MyApiClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7230/api/");
    // Gerekirse default headers vs. buraya da eklenebilir.
    //client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


//-->Manuel
//Route tanımları

app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );


app.Run();
