using AppUserDetails;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Interfaces;
using Core.Helpers;
using EncashmentDetails;
using EndOfMonthReportDetails;
using ExpensesReportDetails;
using FluentValidation.AspNetCore;
using GeneralSettingDetails;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MoneyOrderDetails;
using ProjectDetails;
using SettingFinanceOperationDetails;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using TrolleyDetails;
using TrolleyTypeDetails;

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
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Finance Operation", Version = "v1" });

    // Dosya yükleme işlemi için filter
    options.OperationFilter<FileUploadOperation>();

    // JWT Authentication Security Definition
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standart Authorization başlığı kullanarak Bearer şeması (\"Bearer {token}\")",
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

// JsonOptions yerine JsonSerializerOptions kullanarak yapılandırma
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter("yyyy-MM-dd"));
    });

builder.Services.AddAppUserServices();
builder.Services.AddProjectServices();
builder.Services.AddEncashmentServices();
builder.Services.AddExpensesReportServices();
builder.Services.AddEndOfMonthReportServices();
builder.Services.AddMoneyOrderServices();
builder.Services.AddSettingFinanceOperationServices();
builder.Services.AddTrolleyServices();
builder.Services.AddTrolleyTypeServices();
builder.Services.AddGeneralSettingServices();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(
   builder => builder.RegisterModule(new AutoFacBusiness()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseFileServer(new FileServerOptions
    {
        FileProvider = new PhysicalFileProvider(@"c:/FinanceOperationFiles"),
        RequestPath = new PathString("/uploads"),
        EnableDirectoryBrowsing = false
    });
}
else
{
    app.UseFileServer(new FileServerOptions
    {
        FileProvider = new PhysicalFileProvider("/mnt/media_files/financeoperation"),
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