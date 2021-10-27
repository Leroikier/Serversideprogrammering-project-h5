using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.Database.Models;
using WebApplication1.Codes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddTransient<HashingExample>();
builder.Services.AddSingleton<HashingExample>();

var DefaultConnection =
    builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TestDatabaseContext>(
    options => options.UseSqlServer(DefaultConnection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();