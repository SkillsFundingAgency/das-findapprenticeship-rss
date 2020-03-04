using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using VacancyRssFeedService.Configuration;

namespace VacancyRssFeedService
{
    public class Global : HttpApplication
    {
        private static IConfiguration _configuration = null;

        public static IConfiguration Configuration => _configuration ?? (_configuration = new Configuration.Configuration());

        void Application_Start(object sender, EventArgs e)
        {
            Configuration.AddConfig();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}