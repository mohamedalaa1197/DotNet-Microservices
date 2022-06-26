using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.API.Extensions
{
    public static class HostExtension
    {
        public static IHost MigratingDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder, int? retry = 0)
            where TContext : DbContext
        {
            int retryforAvaliablity = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();

                try
                {
                    logger.LogInformation("start migrating Database");

                    InvokerSeeder(seeder, context, services);
                }
                catch (SqlException ex)
                {
                    logger.LogError(ex, "an error occured while migrating SQL DB data");
                    if (retryforAvaliablity < 50)
                    {
                        retryforAvaliablity++;
                        Thread.Sleep(2000);
                        MigratingDatabase<TContext>(host, seeder, retryforAvaliablity);
                    }
                }
            }

            return host;
        }

        private static void InvokerSeeder<TContext>(Action<TContext, IServiceProvider> seeder,
                                                        TContext context,
                                                        IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}
