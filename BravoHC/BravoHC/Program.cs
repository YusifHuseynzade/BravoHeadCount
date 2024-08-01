﻿using AppUserDetails;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Interfaces;
using DepartmentDetails;
using FluentValidation.AspNetCore;
using FunctionalAreaDetails;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PositionDetails;
using ProjectDetails;
using SubSubSectionDetails;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
})
    .AddFluentValidation(v => v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Bravo Head Count ", Version = "v1" });

    // File upload operation filter

    // Security definition
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"Bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

});



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

builder.Services.AddScoped<IApplicationDbContext, AppDbContext>();
builder.Services.AddHttpContextAccessor();

//builder.Services.AddSingleton<InputHasher>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// JsonOptions yerine JsonSerializerOptions kullanarak yapılandırma
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter("yyyy-MM-dd"));
    });

builder.Services.AddAppUserServices();
builder.Services.AddFunctionalAreaServices();
builder.Services.AddDepartmentServices();
builder.Services.AddSectionServices();
builder.Services.AddSubSectionServices();
builder.Services.AddPositionServices();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(
   builder => builder.RegisterModule(new AutoFacBusiness()));

var app = builder.Build();

#region Middleware
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
//app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());
//app.UseSerilogRequestLogging();
app.UseHttpLogging();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();
app.UseCors("AllowFrontend");
app.Run();
#endregion