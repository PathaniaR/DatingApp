using DatingApi.Data;
using DatingApi.Helpers;
using DatingApi.Interfaces;
using DatingApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DatingApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config)
        {
          services.AddScoped<ITokenService,TokenService>();
          services.AddScoped<IUserRepository,UserRepository>();
          services.AddAutoMapper(typeof(AutomapperProfiles).Assembly);
            services.AddDbContext<DatingDataContext>(options =>{
                options.UseSqlServer(config.GetConnectionString("AppConnection"));
            });

            return services;
        }
    }
}