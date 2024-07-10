using System.Text.Json.Serialization;
using EBeauty.Application;
using EBeauty.Infrastructure;
using EBeauty.WebApi.Auth;
using EBeauty.WebApi.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var APP_NAME = "EBeauty.WebApi";

Log.Logger = new LoggerConfiguration()
    .Enrich.WithProperty("Application", APP_NAME)
    .Enrich.WithProperty("MachineName", Environment.MachineName)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Development.local.json");
}

builder.Host.UseSerilog((context, services, configuration) => configuration
    .Enrich.WithProperty("Application", APP_NAME)
    .Enrich.WithProperty("MachineName", Environment.MachineName)
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews(options =>
{
    if (!builder.Environment.IsDevelopment())
    {
        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    }
}).AddJsonOptions(options => 
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddJwtAuthenticationDataProvider(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddValidators();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.CustomSchemaIds(x =>
    {
        var name = x.FullName;
        if (name != null)
        {
            name = name.Replace("+", "_");
        }
        return name;
    });
});

builder.Services.AddAntiforgery(o =>
{
    o.HeaderName = "X-XSRF-TOKEN";
});

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(builder => builder
    .WithOrigins(app.Configuration.GetValue<string>("WebAppBaseUrl") ?? "")
    .WithOrigins(app.Configuration.GetSection("AdditionalCorsOrigins").Get<string[]>() ?? new string[0])
    .WithOrigins((Environment.GetEnvironmentVariable("AdditionalCorsOrigins") ?? "").Split(',').Where(h => !string.IsNullOrEmpty(h)).Select(h => h.Trim()).ToArray())
    .AllowAnyHeader()
    .AllowCredentials()
    .AllowAnyMethod());

app.UseExceptionResultMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
