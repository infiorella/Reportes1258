using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Models;
using QuestPDF.Infrastructure;
using Models.Domain;
using Repositories.Abstract;
using Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DatabaseContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
.AddEntityFrameworkStores<DatabaseContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(op => op.LoginPath = "/UserAuthentication/Login");

builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Home/Login";
});

//Base de datos
builder.Services.AddDbContext<Colegio1258Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"))
);


var app = builder.Build();
//Servicio PDF
QuestPDF.Settings.License = LicenseType.Community;

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Error/Error404";
        await next();
    }

    if (context.Response.StatusCode == 500)
    {
        context.Request.Path = "/Error/Error500";
        await next();
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.Use((context, next) =>
{
    Thread.CurrentPrincipal = context.User;
    return next(context);
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserAuthentication}/{action=Login}/{id?}");

app.Run();

