using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestfulBooker.ApiTests.API;
using RestfulBooker.ApiTests.Common;
using RestfulBooker.ApiTests.Core;
using RestfulBooker.ApiTests.Data;
using SolidToken.SpecFlow.DependencyInjection;
using TechTalk.SpecFlow;

namespace RestfulBooker.ApiTests;

[Binding]
public class Startup
{
      [ScenarioDependencies]
      public static IServiceCollection ConfigureServices()
      {
            var services = new ServiceCollection();
            var configurationFilePath = BuildConfiguration();

            services.AddSingleton<IConfiguration>(configurationFilePath);

            ValidateConfiguration(configurationFilePath);

            RegisterApplicationServices(services);

            return services;
      }

      private static IConfiguration BuildConfiguration()
      {
            return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();
      }

      private static void ValidateConfiguration(IConfiguration configuration)
      {
            var baseUrl = configuration["Api:BaseURL"];

            if (string.IsNullOrWhiteSpace(baseUrl))
                  throw new InvalidCastException("URL is missing from configuration file [appsettings.json]");
      }

      private static void RegisterApplicationServices(IServiceCollection services)
      {
            services.AddScoped<ApiTestBase>(serviceProvider =>
            {
                  var config = serviceProvider.GetRequiredService<IConfiguration>();

                  return new ApiTestBase(config["Api:BaseURL"]!);
            });

            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<BookingScenarioContext>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthCredentialsProvider, AuthCredentialsProvider>();

            services.AddSingleton<SqliteService>(serviceProvider =>
            {
                  var config = serviceProvider.GetRequiredService<IConfiguration>();
                  var dbPath = config["DatabasePath"];

                  if (string.IsNullOrWhiteSpace(dbPath))
                        throw new InvalidOperationException("DatabasePath is missing from appsettings.json");

                  return new SqliteService(dbPath);
            });


      }
}