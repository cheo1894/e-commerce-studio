using System.Text;
using Backend.Modules.Auth.Application.Interfaces;
using Backend.Modules.Auth.Application.Services;
using Backend.Modules.Auth.Domain.Interfaces;
using Backend.Modules.Auth.Infrastructure.Persistence.Repositories;
using Backend.Modules.BFF.Application.Interfaces;
using Backend.Modules.BFF.Application.Services;
using Backend.Modules.BFF.Infrastructure.Middlewares;
using Backend.Modules.Auth.Infrastructure.Persistence;
using Backend.Modules.Auth.Infrastructure.Services;
using Backend.Modules.Authl.Infrastructure.Options;
using Backend.Modules.BFF.Infrastructure.Cache;
using Backend.Modules.ProductCatalog.Application.Interfaces;
using Backend.Modules.ProductCatalog.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Backend.Modules.Auth.Domain.Entities;
using Backend.Modules.ProductCatalog.Domain.entities;
using Backend.Modules.ProductCatalog.Application.Services;
using Backend.Modules.ProductCatalog.Domain.Interfaces;
using Backend.Modules.ProductCatalog.Infrastructure.Persistence.Repositories;
// using Microsoft.OpenApi;



var builder = WebApplication.CreateBuilder(args);

//BFF
builder.Services.AddMemoryCache();

//Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDev", policy => policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
});

// Add services to the container.

builder.Services.AddControllers();

//JWT Config

builder.Services.Configure<JWTSetting>(builder.Configuration.GetSection("JwtSetting"));

builder.Services.AddScoped<ITokenService, JWTService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSetting:Issuer"],
        ValidAudience = builder.Configuration["JwtSetting:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JwtSetting:Key"])
        )

    };
});

builder.Services.AddAuthorization();



//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Intermediate Services
builder.Services.AddScoped<IuserNameProvider, AuthUserNameProvider>();


//Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UsersService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProductCatalogService, ProductCatalogService>();
builder.Services.AddScoped<ITokenCache, MemoryTokenCache>();
builder.Services.AddScoped<ITokenProxyService, TokenProxyServices>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokeService>();
builder.Services.AddScoped<ICategoryService, CategoryServices>();
//Repositories
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IProductCatalogRepository, ProductCatalogRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//Db Connections=========================================================================
builder.Services.AddDbContextFactory<AuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});

builder.Services.AddDbContext<ProductCatalogDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});
//=========================================================================================

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("LocalDev");
app.UseMiddleware<SesionTokenMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
