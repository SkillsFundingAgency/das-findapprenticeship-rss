using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VacancyRssFeedService.Configuration
{
    /// <summary>
    /// Configuration item
    /// </summary>
    public class ConfigurationItem : TableEntity
    {
        /// <summary>
        /// Configuration data
        /// </summary>
        public string Data { get; set; }
    }
}