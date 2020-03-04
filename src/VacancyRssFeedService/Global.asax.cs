using Microsoft.Extensions.Configuration;
using System;
using System.Web;
using System.Web.Routing;
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