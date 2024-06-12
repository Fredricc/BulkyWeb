using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

IdentityBuilder identityBuilder = builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddDefaultUI(UIFramework.Bootstrap5)
    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddIdentityCore<IdentityUser>()
//    .AddRoles<IdentityRole>()
//    .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<IdentityUser, IdentityRole>>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders()
//    .AddDefaultUI();


builder.Services.AddScoped < IUnitOfWork, UnitOfWork>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
