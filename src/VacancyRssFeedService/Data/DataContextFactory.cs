using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Configuration;
using System.Data.Linq;
using System.Data.SqlClient;

namespace VacancyRssFeedService.Data
{
    public class DataContextFactory
    {
        private static string AzureResource = "https://database.windows.net/";

        public static T CreateDataContext<T>() where T : DataContext
        {
            var connectionString = Global.Configuration["NavmsConnectionString"];
            var env = Global.Configuration["EnvironmentName"];
            var isDevEnvironment = (env?.Equals("DEV") ?? false) || (env?.Equals("DEVELOPMENT") ?? false) || (env?.Equals("LOCAL") ?? false);

            if (connectionString == null)
            {
                throw new ConfigurationErrorsException("No Navms Connection string");
            }
            if (isDevEnvironment)
            {
                return (T)Activator.CreateInstance(typeof(T), connectionString);
            }
            else
            {
                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                var sqlConnection = new SqlConnection
                {
                    ConnectionString = connectionString,
                    AccessToken = azureServiceTokenProvider.GetAccessTokenAsync(AzureResource).Result,
                };
                return (T)Activator.CreateInstance(typeof(T), sqlConnection);
            }
        }
    }
}