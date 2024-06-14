using eApoteka.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "proizvodDetails",
    pattern: "ProizvodDetails/{id}",
    defaults: new { controller = "ProizvodDetails", action = "Index" });

app.MapControllerRoute(
    name: "orderDetails",
    pattern: "ProizvodDetails/OrderDetails/{id?}",
    defaults: new { controller = "OrderDetails", action = "Index" });

app.MapControllerRoute(
    name: "showOrders",
    pattern: "PlaceOrder/ShowOrders/{id?}",
    defaults: new { controller = "ShowOrders", action = "Index" });

app.MapControllerRoute(
    name: "login",
    pattern: "Home/Login",
    defaults: new { controller = "Login", action = "Index" });

app.MapControllerRoute(
    name: "register",
    pattern: "Home/Register/",
    defaults: new { controller = "Register", action = "Index" });

app.MapRazorPages();

app.Run();
