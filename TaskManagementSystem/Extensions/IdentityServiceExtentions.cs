using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManagementSystem.core.Entities;
using TaskManagementSystem.core.Services.Contract;
using TaskManagementSystem.Core.Entities.Identity;
using TaskManagementSystem.Repository.Identity;
using TaskManagementSystem.Services;

namespace TaskManagementSystem.api.Extensions
{
    public static class IdentityServiceExtentions
    {
        public static IServiceCollection AddIdentityService (this IServiceCollection services , IConfiguration configuration)
        {
            services.AddIdentity<User, IdentityRole>()
              .AddEntityFrameworkStores<UserDbContext>()
            .AddDefaultTokenProviders();
            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:ValidAudience"],
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:ValidIssuer"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromDays(double.Parse(configuration["JWT:expireDay"]))
                };
            });
           
            return services;
        }
    }
}
