using System;
using System.Reflection;

namespace VacancyRssFeedService
{
    public partial class Ping : System.Web.UI.Page
    {
        public string AppVersion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            AppVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}