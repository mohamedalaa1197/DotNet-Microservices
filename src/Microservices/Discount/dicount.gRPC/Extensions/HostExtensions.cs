using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System.Threading;

namespace dicount.gRPC.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<T>(this IHost host, int? retry = 0)
        {
            int AvaliableRetry = retry.Value;
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configurations = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<T>>();

                try
                {
                    logger.LogInformation("Migrating the postgreSQL database");
                    using var connection = new NpgsqlConnection(
                        configurations.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon(
		                                        ID SERIAL PRIMARY KEY         NOT NULL,
		                                        ProductName     VARCHAR(24) NOT NULL,
		                                        Description     TEXT,
		                                        Amount          INT
	                                                            )";

                    command.ExecuteNonQuery();

                    command.CommandText =
                        @"INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('IPhone X', 'IPhone Discount', 150)";
                    command.ExecuteNonQuery();

                    command.CommandText =
                        @"INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Samsung 10', 'Samsung Discount', 100)";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Finish migration to postgreSQL database");
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An Error ocurred while migrating postgrSQL");

                    if (AvaliableRetry < 50)
                    {
                        AvaliableRetry++;
                        Thread.Sleep(2000);
                        MigrateDatabase<T>(host, AvaliableRetry);
                    }
                }
            }

            return host;
        }
    }
}