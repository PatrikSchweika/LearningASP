using System.Text;

using Data;
using Data.Repositories;

using LearningASP;
using LearningASP.Model;
using LearningASP.Options;
using LearningASP.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString(AppConstants.UserDbConnectionString);

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.Configure<AppConfiguration>(builder.Configuration);
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection(AppConstants.JwtSectionName));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserService, RepositoryUserService>();
builder.Services.AddScoped<IUserRepository, DbUserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddTransient<IPasswordHasher<User>, UserPasswordHasher>();

// builder.Services.AddTransient<ExceptionHandlerMiddleware>();
// builder.Services.AddAutoMapper(typeof(UserMappingProfile));

builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddProblemDetails();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    // configuration.WriteTo.Http("http://localhost:5044", null);
});

builder.Services.AddAuthentication(AppConstants.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtConfiguration = builder.Configuration
            .GetSection(AppConstants.JwtSectionName)
            .Get<JwtConfiguration>();

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

app.UseSerilogRequestLogging();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
});

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseExceptionHandler();

// app.UseMiddleware<ExceptionHandlerMiddleware>();

// Custom middleware
// app.Use(async (context, next) =>
// {
//     Debug.WriteLine("Middleware 1");
//
//     await next.Invoke();
// });

app.MapControllers();

app.Run();