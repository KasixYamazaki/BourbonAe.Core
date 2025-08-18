using BourbonAe.Core.Data;
using BourbonAe.Core.Presentation.Filters;
using BourbonAe.Core.Services.Auth;
using BourbonAe.Core.Services.Compression;
using BourbonAe.Core.Services.Export;
using BourbonAe.Core.Services.Features.AEKB0040;
using BourbonAe.Core.Services.Features.AEMM0010;
using BourbonAe.Core.Services.Features.AESJ1110;
using BourbonAe.Core.Services.Features.AEST0010;
using BourbonAe.Core.Services.Features.AEST0020;
using BourbonAe.Core.Services.Html;
using BourbonAe.Core.Services.Logging;
using BourbonAe.Core.Services.Reporting;
using BourbonAe.Core.Services.Time;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

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

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(o =>
{
    o.LoginPath = "/Account/Login";
    o.LogoutPath = "/Account/Logout";
    o.AccessDeniedPath = "/Account/Login";
    o.SlidingExpiration = true;
    o.ExpireTimeSpan = TimeSpan.FromHours(8);
});

// DbContext 登録（既存の ApplicationDbContext を使用）
builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ApplicationDbContext を IAppDb として利用（AuthService の最小依存）
builder.Services.AddScoped<IAppDb>(sp => sp.GetRequiredService<ApplicationDbContext>());
builder.Services.AddScoped<DbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

// 認証サービス
builder.Services.AddScoped<IAuthService, AuthService>();

// 帳票サービス
builder.Services.AddScoped<IReportExportService, ReportExportService>();

// 画面の機能サービス
builder.Services.AddScoped<IAesj1110Service, Aesj1110Service>();
builder.Services.AddScoped<IAemm0010Service, Aemm0010Service>();
builder.Services.AddScoped<IAekb0040Service, Aekb0040Service>();
builder.Services.AddScoped<IAest0010Service, Aest0010Service>();
builder.Services.AddScoped<IAest0020Service, Aest0020Service>();

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

app.UseAuthentication();
app.UseAuthorization();

// Endpoints configuration: map controller routes. The default route points
// to HomeController.Index.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();