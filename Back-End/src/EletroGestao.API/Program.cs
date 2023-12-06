using EletroGestao.API.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>();//para várias de ambiente - coisas utilizadas pela asp.net

builder.Services.AddDatabaseSetup(builder.Configuration);

// AutoMapper Settings
builder.Services.AddAutoMapperSetup();

// ASP.NET Identity Settings & JWT

builder.Services.AddApiConfig(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//// Authorization
//builder.Services.AddAuthSetup(builder.Configuration);

builder.Services.AddSwaggerSetup();

// ASP.NET HttpContext dependency
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// .NET Native DI Abstraction
builder.Services.AddDependencyInjectionSetup();

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseApiConfig(app.Environment);

//if (!builder.Environment.EnvironmentName.Contains("Production"))
app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.MapControllers();

app.Run();