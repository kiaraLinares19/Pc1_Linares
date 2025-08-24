using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Pc1_Linares.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Cultura es-PE (S/ y formatos locales)
var supportedCultures = new[] { new CultureInfo("es-PE") };
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("es-PE"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Productos}/{action=Index}/{id?}");

app.Run();
