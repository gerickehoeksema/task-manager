using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskManager.Application.Interfaces;
using TaskManager.Infrastucture.Configuration;
using TaskManager.Infrastucture.Identity;
using TaskManager.Infrastucture.Persistance;
using TaskManager.Infrastucture.Services;

namespace TaskManager.Infrastucture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase(databaseName: "TaskManager"));

            services.AddTransient(provider => (IApplicationDbContext)provider.GetService<ApplicationDbContext>());
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var jwtConfig = new JwtConfiguration();
            configuration.Bind("Jwt", jwtConfig);
            services.AddSingleton<IJwtConfiguration>(jwtConfig);

            services.AddTransient<IDateTimeService, DateTimeService>();

            // Adding Authentication  
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret))
                };
            });

            return services;
        }
    }
}
