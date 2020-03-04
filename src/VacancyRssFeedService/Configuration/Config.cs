using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VacancyRssFeedService.Configuration
{
    public static class Config
    {
        /// <summary>
        /// Adds configuration to the <see cref="IConfigurationBuilder"/> from files and table storage
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IConfiguration AddConfig(this IConfiguration config)
        {
            config["ConfigurationStorageConnectionString"] = System.Configuration.ConfigurationManager.AppSettings["ConfigurationStorageConnectionString"];
            config["AppName"] = System.Configuration.ConfigurationManager.AppSettings["AppName"];
            config["EnvironmentName"] = System.Configuration.ConfigurationManager.AppSettings["EnvironmentName"];
            
            config = Load(config);

            return config;
        }

        private static IConfiguration Load(IConfiguration config)
        {
            if (config["EnvironmentName"].Equals("DEV", StringComparison.CurrentCultureIgnoreCase))
            {
                return config;
            }

            var table = GetTable(config["ConfigurationStorageConnectionString"]);
            var operation = GetOperation(config["AppName"], config["EnvironmentName"], "1.0");
            var result = table.ExecuteAsync(operation).Result;

            var configItem = (ConfigurationItem)result.Result;
            var jsonObject = JObject.Parse(configItem.Data);

            config["NavmsConnectionString"] = jsonObject.SelectToken("NavmsConnectionString").Value<string>();
            config["VacancyLinkUrlExternal"] = jsonObject.SelectToken("VacancyLinkUrlExternal").Value<string>();

            return config;
        }

        private static CloudTable GetTable(string connection)
        {
            var storageAccount = CloudStorageAccount.Parse(connection);
            var tableClient = storageAccount.CreateCloudTableClient();
            return tableClient.GetTableReference("Configuration");
        }

        private static TableOperation GetOperation(string serviceName, string environmentName, string version)
        {
            return TableOperation.Retrieve<ConfigurationItem>(environmentName, $"{serviceName}_{version}");
        }
    }
}