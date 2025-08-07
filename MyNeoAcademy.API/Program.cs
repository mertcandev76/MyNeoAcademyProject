using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using MyNeoAcademy.Entity.Entities;
using MyNeoAcademy.DataAccess.Context;
using MyNeoAcademy.Application.Abstract;
using MyNeoAcademy.Application.Validators;
using MyNeoAcademy.Business.Concrete;
using MyNeoAcademy.Business.DependencyResolvers;
using MyNeoAcademy.DataAccess.Abstract;
using MyNeoAcademy.DataAccess.Repositories;
using MyNeoAcademy.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MyNeoAcademy.Application.Mapping.User;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------
// 🔹 CORS
// ------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUIOrigin",
        policy => policy
            .WithOrigins("https://localhost:7283")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// ------------------------------
// 🔹 JSON ve FluentValidation Ayarları
// ------------------------------
builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(CreateAboutFeatureValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// ------------------------------
// 🔹 AutoMapper
// ------------------------------
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddAutoMapper(cfg =>
//{
//    cfg.AddMaps(typeof(AppUserMappingProfile).Assembly); // mapping klasörünü tarar
//});

// ------------------------------
// 🔹 DbContext
// ------------------------------
builder.Services.AddDbContext<MyNeoAcademyContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

// ------------------------------
// 🔐 Identity & JWT Authentication
// ------------------------------
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<MyNeoAcademyContext>()
    .AddDefaultTokenProviders();

// 🔐 JWT Ayarları
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

// ------------------------------
// 🔹 Swagger + JWT Desteği
// ------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MyNeoAcademy API", Version = "v1" });

    // JWT token desteği
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Bearer {token} şeklinde giriniz"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// ------------------------------
// 🔹 Katmanlar & Servisler
// ------------------------------
builder.Services.AddDependencyResolvers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IFileService, FileService>();

// ------------------------------
// UYGULAMA PIPELINE'I
// ------------------------------
var app = builder.Build();

app.UseStaticFiles();
app.UseCors("AllowUIOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // 🔐 Token doğrulama
app.UseAuthorization();  // 🔒 Yetki kontrol

app.MapControllers();
app.Run();
