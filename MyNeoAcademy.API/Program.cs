using Microsoft.EntityFrameworkCore;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.Business.Concrete;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Context;
using MyNeoAcademy.DataAccess.Repositories;
using MyNeoAcademy.API.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// 🔹 AutoMapper – Tüm profilleri tara (Mapping klasöründeki profiller dahil)
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// 🔹 FluentValidation – Tüm validator sınıflarını tara
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // API katmanı içindeki validator'lar
builder.Services.AddValidatorsFromAssemblyContaining<MyNeoAcademy.DTO.Validators.SliderValidator.CreateSliderValidator>();
// DTO tarafındaki validator'lar

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

// 🔹 Swagger (API dokümantasyonu)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseStaticFiles();

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
