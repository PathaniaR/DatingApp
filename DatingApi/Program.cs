using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DatingApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
          var host=  CreateHostBuilder(args).Build();
          using var scope=host.Services.CreateScope();
          var Services=scope.ServiceProvider;
          try
          {
              var context = Services.GetRequiredService<DatingDataContext>();
              await context.Database.MigrateAsync();
              await Seed.SeedUsers(context);
          }
          catch (System.Exception ex)
          {
              var logger= Services.GetRequiredService<ILogger<Program>>();
              logger.LogError(ex,"An Error occured during migration");
              throw;
          }

          await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
