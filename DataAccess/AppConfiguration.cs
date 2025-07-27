using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess
{
    public static class AppConfiguration
    {
        private static IConfigurationRoot configuration;

        static AppConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            configuration = builder.Build();
        }

        public static string GetConnectionString()
        {
            return configuration.GetConnectionString("DefaultConnection");
        }

        public static string GetAdminEmail()
        {
            return configuration["AdminAccount:Email"];
        }

        public static string GetAdminPassword()
        {
            return configuration["AdminAccount:Password"];
        }
    }
} 