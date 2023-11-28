using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UniGearRentAPI.DatabaseServices;
using UniGearRentAPI.DatabaseServices.Repositories;
using UniGearRentAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IRepository<CarPost>, CarPostRepository>();
builder.Services.AddTransient<IRepository<TrailerPost>, TrailerPostRepository>();
builder.Services.AddDbContext<UniGearRentAPIDbContext>();
AddAuthentication();
AddIdentity();
var app = builder.Build();
if (Environment.GetEnvironmentVariable("Environment") != "Testing")
{
    AddRoles();
    AddAdmin();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void AddAuthentication()
{
    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Environment.GetEnvironmentVariable("ASPNETCORE_VALIDISSUER"),
                ValidAudience = Environment.GetEnvironmentVariable("ASPNETCORE_VALIDAUDIENCE"),
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("ASPNETCORE_ISSUERSIGNINGKEY"))
                )
            };
        });
}
void AddIdentity()
{
    builder.Services
        .AddIdentityCore<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<UniGearRentAPIDbContext>();
}
void AddRoles()
        {
            using var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var tAdmin = CreateAdminRole(roleManager);
            tAdmin.Wait();

            var tUser = CreateUserRole(roleManager);
            tUser.Wait();
        }

        async Task CreateAdminRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(builder.Configuration["Roles:Admin"]));
        }

        async Task CreateUserRole(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(builder.Configuration["Roles:User"]));
        }

        void AddAdmin()
        {
            var tAdmin = CreateAdminIfNotExists();
            tAdmin.Wait();
        }

        async Task CreateAdminIfNotExists()
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var adminInDb = await userManager.FindByEmailAsync(Environment.GetEnvironmentVariable("ASPNETCORE_ADMINEMAIL"));
            if (adminInDb == null)
            {
                var admin = new User { UserName = "Admin", Email = Environment.GetEnvironmentVariable("ASPNETCORE_ADMINEMAIL"), FirstName = "Admin", LastName = "Admin", PhoneNumber = "00000000000"};
                var adminCreated = await userManager.CreateAsync(admin, Environment.GetEnvironmentVariable("ASPNETCORE_ADMINPASSWORD"));

                if (adminCreated.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }