using Bookflix.Areas.Admin.Models;
using Bookflix.Data;
using Bookflix.Models;
using Bookflix.Models.Context;
using Bookflix.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDbContext<BookflixDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BookflixConnection")));

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddScoped<IRepository<Publisher>, PublisherRepoService>();
builder.Services.AddScoped<IRepository<Book>, BookRepoService>();
builder.Services.AddScoped<IRepository<Author>, AuthorRepoService>();
builder.Services.AddScoped<IRepository<SoldBook>, SoldBookRepoService>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepoService>();
//?????????????????
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddAreaPageRoute("Identity", "/Account/Register", "/Register");
    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/Login");
});

builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = "326458263064-naitolbohbeqgt0sv623ud7upccjad57.apps.googleusercontent.com";
        googleOptions.ClientSecret = "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";
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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

await SeedDatabaseAsync();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


/*app.MapControllerRoute(name:"author",
    pattern: "{area:exists}/{controller=Authors}/{action=Index}/{id?}");*/

/*app.UseEndpoints(endpoints =>
{

endpoints.MapAreaControllerRoute(
    name: "Authors",
    areaName: "Admin",
    pattern: "Author/{controller=Authors}/{action=Index}"
);


    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Authors}/{action=Index}/{id?}"
    );
});
*/

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapRazorPages(); // this one
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

//Initialize Stuff
//await ContextSeed.SeedRolesAsync();


//


async Task SeedDatabaseAsync() //can be placed at the very bottom under app.Run()
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            await ContextSeed.SeedRolesAsync(userManager, roleManager);
            await ContextSeed.SeedSuperAdminAsync(userManager, roleManager);    
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occurred seeding the DB.");
        }
    }
}

app.Run();