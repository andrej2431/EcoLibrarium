
using EcoLibrariumServer.Data;
using EcoLibrariumServer.Models.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("EcoLibrarium"));


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<ApiContext>()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddRoleManager<RoleManager<IdentityRole>>();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});


builder.Services.AddControllers();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }


    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string password = "24312431";

    var user = new ApplicationUser
    {
        Id = Guid.NewGuid().ToString(),
        UserName = "andrej",
        Email = "andrej2431@gmail.com",
        SecurityStamp = Guid.NewGuid().ToString()
    };

    await userManager.CreateAsync(user, password);
    await userManager.AddToRoleAsync(user, "Admin");

    var auser = await userManager.FindByEmailAsync("andrej2431@gmail.com");
    if (auser == null)
    {
        return;
    }

}


app.Run();
