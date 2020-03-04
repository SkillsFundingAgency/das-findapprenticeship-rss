using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using VacancyRssFeedService.Common;

namespace VacancyRssFeedService.Presentation
{
    public class QueryStringHelper
    {
        public static string CreateVacancyUrlForRssFeed(VacancyDTO vacancy)
        {
            return CreateExternalVacancyLink(vacancy);
        }

        private static string VacancyLinkUrlExternal
        {
            get
            {
                return Global.Configuration["VacancyLinkUrlExternal"];
            }
        }

        private static string CreateExternalVacancyLink(VacancyDTO vacancy)
        {
            var type = "apprenticeship";

            if (vacancy.VacancyType == VacancyType.Traineeship)
            {
                type = "traineeship";
            }

            return new Uri(new Uri(VacancyLinkUrlExternal), Path.Combine(type, vacancy.VacancyId.ToString())).ToString();
        }
    }
}