using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using YorozuyaServer.config;
//using YorozuyaServer.Data;
using YorozuyaServer.server;
using YorozuyaServer.server.impl;
using YorozuyaServer.utils;

var builder = WebApplication.CreateBuilder(args);

var mySqlConnStr = builder.Configuration.GetConnectionString("DefautConnection");

// 添加服务
builder.Services.AddControllers();

builder.Services.AddSingleton(new RedisUtil());
builder.Services.AddSingleton(new JwtUtil());
//builder.Services.AddDbContext<DbConfig>(ServiceLifetime.Singleton);
//连接MySQL
builder.Services.AddDbContext<DbConfig>(options =>
options.UseMySql(mySqlConnStr, ServerVersion.AutoDetect(mySqlConnStr)));

builder.Services.AddSingleton<UserService, UserServiceImpl>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yorozuya",
            ValidAudience = "yorozuya_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC7JF5L1WS8b9S7Hd0De6h2djrV")),
            LifetimeValidator = (before, expires, token, param) =>
            {
                return expires > DateTime.UtcNow;
            }
        };
        
    });

builder.Services.AddTransient<DbConfig>(_ =>
{
    var optionsBuilder = new DbContextOptionsBuilder<DbConfig>();
    optionsBuilder.UseMySql(mySqlConnStr, ServerVersion.AutoDetect(mySqlConnStr));
    return new DbConfig(optionsBuilder.Options);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
}

app.MapControllers();

app.Run();