    using Microsoft.EntityFrameworkCore;
using MyNeoAcademy.Business.Abstract;
using MyNeoAcademy.Business.Concrete;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Context;
using MyNeoAcademy.DataAccess.Repositories;
using System;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using MyNeoAcademy.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//-->-->Manuel
//AutoMapper Servis Kayıt (Dependency Injection) İşlemi
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // ✅ Tüm mapping profillerini tarar



//-->Manuel
//Dependency Injection(Manuel) 
builder.Services.AddDependencyResolvers();


// AppDbContext'i servise ekle
builder.Services.AddDbContext<MyNeoAcademyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});


//-->Manuel
//AddControllers ile JSON Serileştirme Yapılandırması
builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        // JSON çıktılarını camelCase formatında döner (örneğin: FullName → fullName)
        x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        // Nesneler arası döngüsel referansları yoksayar, hata oluşmasını engeller
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Preserve değil
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
