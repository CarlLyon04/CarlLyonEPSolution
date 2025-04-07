using DataAccess.Repositories;
using DataAccess;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;


// Initializes the app with default settings and configuration
var builder = WebApplication.CreateBuilder(args);

// FOr MVC controllers and views
builder.Services.AddControllersWithViews();

// For Razor Pages
builder.Services.AddRazorPages();

// Use SQL Server with connection string
builder.Services.AddDbContext<PollDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Allows sign-in without confirming email and registers PollDbContext for use with ASP.NET Identity
builder.Services.AddDefaultIdentity<User>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<PollDbContext>();

// Injects PollRepository for IPollRepository
builder.Services.AddScoped<IPollRepository, PollRepository>();

// THE temporary JSON version switch (Did not manage to implement the dynamic switch of DB to JSON)
//builder.Services.AddScoped<IPollRepository, PollRepository>();


// Injects LogVoteRepository for ILogVoteRepository
builder.Services.AddScoped<ILogVoteRepository, LogVoteRepository>();

// Builds the app
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// Enables user authentication
app.UseAuthentication();

// Enables authorization checks
app.UseAuthorization();

// Maps default route to PollController's Index action
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Poll}/{action=Index}/{id?}");

// Maps Razor Pages routes (needed for the Identity UI)
app.MapRazorPages();

// Run
app.Run();
