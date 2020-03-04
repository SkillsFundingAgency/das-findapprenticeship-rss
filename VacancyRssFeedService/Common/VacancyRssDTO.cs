using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VacancyRssFeedService.Common
{
    public class VacancyRssDTO
    {
        public IList<VacancyDTO> Vacancies { get; set; }
        public string FeedImageUrl { get; set; }
        public string FeedTitle { get; set; }
        public string FeedDescription { get; set; }
        public string FeedCopyrightInformation { get; set; }
        public string AlternateLink { get; set; }
    }
}