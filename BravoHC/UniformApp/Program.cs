using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Interfaces;
using Core.Helpers;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using UniformDetails;
using UniformConditionDetails;
using TransactionPageDetails;
using DCStockDetails;
using BGSStockRequestDetails;
using StoreStockRequestDetails;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
})
    .AddFluentValidation(v => v.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Swagger için security filter ekleyelim
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Uniform App", Version = "v1" });

    // Dosya yükleme i?lemi için filter
    options.OperationFilter<FileUploadOperation>();

    // JWT Authentication Security Definition
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standart Authorization ba?l??? kullanarak Bearer ?emas? (\"Bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    // Swagger'da tüm endpointlere güvenlik gereksinimi ekleme
    options.OperationFilter<SecurityRequirementsOperationFilter>();
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

builder.Services.Configure<FileSettings>(builder.Configuration.GetSection("FileSettings"));

// JsonOptions yerine JsonSerializerOptions kullanarak yap?land?rma
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter("yyyy-MM-dd"));
    });

builder.Services.AddBGSStockRequestServices();
builder.Services.AddStoreStockRequestServices();
builder.Services.AddDCStockServices();
builder.Services.AddTransactionPageServices();
builder.Services.AddUniformConditionServices();
builder.Services.AddUniformServices();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(
   builder => builder.RegisterModule(new AutoFacBusiness()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseFileServer(new FileServerOptions
    {
        FileProvider = new PhysicalFileProvider(@"c:/UniformAppFiles"),
        RequestPath = new PathString("/uploads"),
        EnableDirectoryBrowsing = false
    });
}
else
{
    app.UseFileServer(new FileServerOptions
    {
        FileProvider = new PhysicalFileProvider("/mnt/media_files/uniformapp"),
        RequestPath = new PathString("/uploads"),
        EnableDirectoryBrowsing = false
    });
}

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