using Microsoft.EntityFrameworkCore;
using WebApplication1.Areas.TodoList.Models;
using WebApplication1.Codes;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Data;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddTransient<HashingExample>();
builder.Services.AddSingleton<HashingExample>();

var defaultConnection =
    builder.Configuration.GetConnectionString("DefaultConnection");

var IdentitydefaultConnection =
    builder.Configuration.GetConnectionString("WebApplication1ContextConnection");

builder.Services.AddDbContext<WebApplication1Context>(options =>
    options.UseSqlServer(IdentitydefaultConnection));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<WebApplication1Context>();

builder.Services.AddDbContext<TestDatabaseContext>(
    options => options.UseSqlServer(defaultConnection));

builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticateUser", policy => {
        policy.RequireAuthenticatedUser();
    });
});

builder.Services.AddDataProtection();

builder.Services.AddDataProtection()
.UseCryptographicAlgorithms(
    new AuthenticatedEncryptorConfiguration()
    {
        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
    });
                                        


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
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
