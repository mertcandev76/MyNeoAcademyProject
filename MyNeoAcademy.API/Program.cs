using Microsoft.EntityFrameworkCore;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Business.Concrete;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Context;
using MyNeoAcademy.DataAccess.Repositories;
using MyNeoAcademy.Business.DependencyResolvers;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using MyNeoAcademy.Infrastructure.Services;
using MyNeoAcademy.Application.Validators; 

var builder = WebApplication.CreateBuilder(args);

// --- CORS servislerini ekle ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUIOrigin",
        policy => policy
            .WithOrigins("https://localhost:7283") // UI projenin adresi
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// 🔹 AutoMapper – Tüm profilleri tara (Mapping klasöründeki profiller dahil)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// 🔹 FluentValidation – Tüm validator sınıflarını tara
builder.Services.AddValidatorsFromAssembly(typeof(CreateAboutFeatureValidator).Assembly); // Application katmanı validator'ları
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());              // API katmanı validator'ları (varsa)

// 🔹 FluentValidation AutoValidation (ModelState otomatik dolar)
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// 🔹 JSON Serileştirme Ayarları
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// 🔹 DbContext – SQL Server bağlantısı
builder.Services.AddDbContext<MyNeoAcademyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

// 🔹 Katman bağımlılıkları (Business, DAL)
builder.Services.AddDependencyResolvers();

// HttpContext erişimi için
builder.Services.AddHttpContextAccessor();

// 🔹 Dosya işlemleri için servis
builder.Services.AddScoped<IFileService, FileService>();

// 🔹 Swagger (API dokümantasyonu)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseStaticFiles();

// CORS middleware'i aktif et (mutlaka UseRouting öncesinde olmalı)
app.UseCors("AllowUIOrigin");

// 🔹 Geliştirme ortamında Swagger aç
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
