using System.Diagnostics;
using System.Text;

using Data;
using Data.Repositories;

using LearningASP.AutoMapperProfiles;
using LearningASP.NewFolder;
using LearningASP.Options;
using LearningASP.Services;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("UserDb");

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.Configure<AppConfiguration>(builder.Configuration);
builder.Services.Configure<UserControllerConfiguration>(builder.Configuration.GetSection("UserController"));
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, RepositoryUserService>();
builder.Services.AddScoped<IUserRepository, DbUserRepository>();

builder.Services.AddTransient<CustomMiddleware>();

builder.Services.AddAutoMapper(typeof(UserMappingProfile));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtConfiguration = builder.Configuration.Get<JwtConfiguration>();

        if (jwtConfiguration != null)
        {
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfiguration.Issuer,
                ValidAudience = jwtConfiguration.Audience,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey))
            };
        }
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<CustomMiddleware>();

// Custom middleware
// app.Use(async (context, next) =>
// {
//     Debug.WriteLine("Middleware 1");
//
//     await next.Invoke();
// });

// todo: add authentication
// todo: add e2e tests

app.MapControllers();

app.Run();