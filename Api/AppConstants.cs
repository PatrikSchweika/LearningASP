using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LearningASP;

public static class AppConstants
{
    public const string UserDbConnectionString = "UserDb";
    public const string JwtSectionName = "JwtSettings";
    public const string AuthenticationScheme = JwtBearerDefaults.AuthenticationScheme;
}