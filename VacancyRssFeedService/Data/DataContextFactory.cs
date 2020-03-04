using System;
using System.Configuration;
using System.Data.Linq;

namespace VacancyRssFeedService.Data
{
    public class DataContextFactory
    {
        public static T CreateDataContext<T>() where T : DataContext
        {
            var connection = Global.Configuration["NavmsConnectionString"];
            if (connection == null)
            {
                throw new ConfigurationErrorsException("No Navms Connection string");
            }

            return (T)Activator.CreateInstance(typeof(T), connection);
        }
    }
}