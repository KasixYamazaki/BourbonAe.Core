using Microsoft.AspNetCore.Mvc;
using Serilog;
using BourbonAe.Core.Presentation.Filters;
using BourbonAe.Core.Services.Logging;
using BourbonAe.Core.Services.Compression;
using BourbonAe.Core.Services.Export;
using BourbonAe.Core.Services.Html;
using BourbonAe.Core.Services.Time;

// Entry point for the BourbonAe.Core ASP.NET Core MVC application.
// This file sets up the web host, configures services, and defines the HTTP request pipeline.

var builder = WebApplication.CreateBuilder(args);

// 標準ロギング最小構成
builder.Services.AddLogging();

// Configure Serilog for logging (replacement for log4net).  Serilog reads its
// configuration from appsettings.json and writes logs to the console by default.
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

// Utilities
builder.Services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));
builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddSingleton<IExcelExporter, ExcelExporter>();
builder.Services.AddSingleton<IPdfService, PdfService>();   // QuestPDF を使う場合
builder.Services.AddSingleton<IHtmlParserService, HtmlParserService>();
builder.Services.AddSingleton<IZipService, ZipService>();

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<AppViewDataFilter>();   // 旧マスターのPage_Load相当
});

// Enable runtime compilation of Razor views so changes to .cshtml files are
// reflected without recompiling the application.
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

// Example of registering a DbContext for SQL Server. Replace `ApplicationDbContext`
// with your actual DbContext class when you implement data access. The
// connection string name "DefaultConnection" is defined in appsettings.json.
/*
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Endpoints configuration: map controller routes. The default route points
// to HomeController.Index.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();