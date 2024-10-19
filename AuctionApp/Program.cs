using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AuctionApp.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Dependency injection of persistance into sercvice
builder.Services.AddScoped<IAuctionService, AuctionService>();

// ProjectsDb
builder.Services.AddDbContext<AuctionDbContext>(
    options => options.UseMySQL(builder.Configuration.GetConnectionString("AuctionDbConnection")));

// Dependency injection of persistance into sercvice
builder.Services.AddScoped<IAuctionPersistence, MySqlAuctionPersistence>();
/*
// Identity
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("IdentityDbConnection")));
builder.Services.AddDefaultIdentity<AppIdentityUser>(
        options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppIdentityDbContext>();
*/

// Auto mapping of data
builder.Services.AddAutoMapper(typeof(Program));

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