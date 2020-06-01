using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using StackOverHead.Auth.Infra.Context;
using StackOverHead.CrossCutting.Models;
using StackOverHead.Question.Infra.Context;

namespace StackOverHead.CrossCutting.Extensions
{
    public static class DBContextsExtensions
    {
        public static string BuildConnectionString(this IConfiguration configuration)
        {
            var appSettings = GetAppSettings(configuration);
            var builder = new NpgsqlConnectionStringBuilder();
            builder.Database = appSettings.DBName;
            builder.Host = appSettings.DBHost;
            builder.Username = appSettings.DBUsuario;
            builder.Password = appSettings.DBSenha;
            var connectionString = builder.ConnectionString;
            return connectionString;
        }

        private static AppSettings GetAppSettings(IConfiguration configuration)
        {
            var appSettings = new AppSettings();
            appSettings.DBHost = configuration["DBHOST"] ?? configuration["DatabaseConfig:DatabaseHost"];
            var dbPort = configuration["DBHOST"] ?? configuration["DatabaseConfig:DatabasePort"];
            if (string.IsNullOrEmpty(dbPort))
            {
                int.TryParse(dbPort, out var dbPortNumber);
                appSettings.DBPort = dbPortNumber;
            }
            appSettings.DBUsuario = configuration["USERID"] ?? configuration["DatabaseConfig:UserID"];
            appSettings.DBSenha = configuration["USERPASS"] ?? configuration["DatabaseConfig:UserPass"];
            appSettings.DBName = configuration["DBNAME"] ?? configuration["DatabaseConfig:DatabaseName"];
            return appSettings;
        }

        public static IServiceCollection AddPostgres(this IServiceCollection services,
            string migrationAssembly, string connectionString)
        {
            services.AddDbContext<StackOverHeadQuestionDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseNpgsql(connectionString, m =>
                        m.MigrationsAssembly(migrationAssembly))
            );

            services.AddDbContext<StackOverHeadAuthDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseNpgsql(connectionString, m =>
                        m.MigrationsAssembly(migrationAssembly))
            );

            return services;
        }

        public static IServiceCollection AddMemoryDatabase(this IServiceCollection services)
        {
            services.AddDbContext<StackOverHeadQuestionDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .EnableSensitiveDataLogging()
            );
            services.AddDbContext<StackOverHeadAuthDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .EnableSensitiveDataLogging()
            );
            return services;
        }

        public static IHost InitializeDataBase(this IHost host)
        {
            var serviceScopeFactory = host.Services;

            using var scope = serviceScopeFactory.CreateScope();
            var services = scope.ServiceProvider;

            StackOverHeadQuestionDbContext stockDB = services
                .GetRequiredService<StackOverHeadQuestionDbContext>();
            stockDB.Database.Migrate();

            StackOverHeadAuthDbContext authDB = services
               .GetRequiredService<StackOverHeadAuthDbContext>();
            authDB.Database.Migrate();

            return host;
        }
    }


}
