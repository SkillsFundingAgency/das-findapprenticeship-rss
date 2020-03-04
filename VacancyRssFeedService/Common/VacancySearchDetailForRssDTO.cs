using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VacancyRssFeedService.Common
{
    public class VacancySearchDetailForRssDTO
    {
        /// <summary>
        /// the type of the feed.
        /// </summary>
        public VacancyRssFeedType FeedType;

        /// <summary>
        /// The day range.
        /// </summary>
        public int DayRange;

        /// <summary>
        /// The unique reference for a vacancy on which to search
        /// </summary>
        public int? VacancyReferenceId { get; set; }

        /// <summary>
        /// The framework id for a specific framework on which to search
        /// </summary>
        public string FrameworkCode { get; set; }

        /// <summary>
        /// The occupation id for a specific occupation on which to search
        /// </summary>
        public string OccupationCode { get; set; }

        /// <summary>
        /// The comma separated list for counties on which to search
        /// </summary>
        public string CountyCode { get; set; }

        /// <summary>
        /// The comma separated list for towns on which to search
        /// </summary>
        public string Town { get; set; }

        /// <summary>
        /// The comma separated list of regions on which to search
        /// </summary>
        public string RegionCode { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? PageIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DetailRequired { get; set; }
    }
}