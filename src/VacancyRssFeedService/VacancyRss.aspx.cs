using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.ServiceModel.Syndication;
using System.Text;
using System.Web;
using System.Xml;
using VacancyRssFeedService.Common;
using VacancyRssFeedService.Logic;
using VacancyRssFeedService.Presentation;

namespace VacancyRssFeedService
{
    public partial class VacancyRss : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                VacancySearchDetailForRssDTO parameters = GetParameters();

                
                
                CreateFeed(parameters);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <returns></returns>
        private VacancySearchDetailForRssDTO GetParameters()
        {
            VacancySearchDetailForRssDTO searchCriteria = new VacancySearchDetailForRssDTO();

            // retrieve the reference number
            int vacencyReference = 0;
            string referenceNumber = HttpContext.Current.Request.QueryString["referenceNumber"];
            if (!string.IsNullOrWhiteSpace(referenceNumber))
            {
                if (!int.TryParse(referenceNumber, out vacencyReference))
                    throw new FormatException("The reference number provided is not in a valid format");
                else
                    searchCriteria.VacancyReferenceId = vacencyReference;
            }

            // feed type
            string feedType = HttpContext.Current.Request.QueryString["feedType"];
            if (!Enum.TryParse(feedType, out searchCriteria.FeedType))
            {
                throw new FormatException("The feed type parameter provided is not in a valid format");
            }
            else if (!Enum.IsDefined(typeof(VacancyRssFeedType), searchCriteria.FeedType))
            {
                throw new FormatException("The feed type parameter provided is not in a valid format");
            }

            // day range
            string dayRange = HttpContext.Current.Request.QueryString["dayRange"];
            if (!string.IsNullOrWhiteSpace(dayRange))
            {
                if (!int.TryParse(dayRange, out searchCriteria.DayRange))
                    throw new FormatException("The day range parameter provided is not in a valid format");
            }

            if (searchCriteria.FeedType == VacancyRssFeedType.DateTimeRange && searchCriteria.DayRange <= 0)
            {
                throw new InvalidOperationException("The day range parameter cannot be blank, zero or less than zero");
            }

            searchCriteria.FrameworkCode = HttpContext.Current.Request.QueryString["frameworkCode"];
            searchCriteria.OccupationCode = HttpContext.Current.Request.QueryString["occupationCode"];

            searchCriteria.Town = HttpContext.Current.Request.QueryString["town"];
            searchCriteria.RegionCode = HttpContext.Current.Request.QueryString["regionCode"];
            searchCriteria.CountyCode = HttpContext.Current.Request.QueryString["countyCode"];

            int numAreaCriteria = (String.IsNullOrEmpty(searchCriteria.Town)? 0: 1) 
                + (String.IsNullOrEmpty(searchCriteria.RegionCode)? 0: 1) 
                + (String.IsNullOrEmpty(searchCriteria.CountyCode)? 0: 1);
            if (numAreaCriteria > 2)
                throw new InvalidOperationException("Only one or two types of geographic parameter maybe used.");

            return searchCriteria;
        }

        /// <summary>
        /// Creates the feed.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        private void CreateFeed(VacancySearchDetailForRssDTO parameters)
        {
            SyndicationFeed feed = null;
            var items = new List<SyndicationItem>();
            var vacancyController = new VacancyLogicService(new Data.VacancyDataService());
            
            var results = vacancyController.GetVacanciesForRss(parameters);

            if (DateTime.Now.Minute < 10)
            {
                feed = new SyndicationFeed(results.FeedTitle, results.FeedDescription, new Uri(results.AlternateLink));
                items.Add(new SyndicationItem("Sorry, this service is closing soon","From 1 April 2022, you will not be able to log in or use this service. Use our new recruitment Application Programming Interfaces (APIs) service to create adverts using your existing systems or display adverts from Find an apprenticeship on your websites. Get started with the recruitment APIs as an employer", new Uri("https://www.gov.uk/guidance/get-started-with-the-recruitment-apis-as-an-employer")));
                
            }
            else
            {
                if (results != null)
                {
                    feed = new SyndicationFeed(results.FeedTitle, results.FeedDescription, new Uri(results.AlternateLink));
                
                    if (!string.IsNullOrWhiteSpace(results.FeedImageUrl))
                        feed.ImageUrl = new Uri(results.FeedImageUrl);

                    if (!string.IsNullOrWhiteSpace(results.FeedCopyrightInformation))
                        feed.Copyright = new TextSyndicationContent(results.FeedCopyrightInformation);

                    feed.Links.Add(new SyndicationLink(new Uri(Request.Url.ToString()), "self", null, "application/rss+xml", 1000));

                    //foreach and LINQ statements are slower than for loop
                    //even if they are more readable, and as performance is critical, I have stuck with for.
                    for (var i = 0; i < results.Vacancies.Count; i++)
                    {
                        var vacancyTitle = results.Vacancies[i].VacancyTitle;
                        var vacancyDesc = FormatFeed(results.Vacancies[i]);

                        string url = QueryStringHelper.CreateVacancyUrlForRssFeed(results.Vacancies[i]);

                        //Now create the RSS item
                        var item = new SyndicationItem(vacancyTitle, vacancyDesc, new Uri(url));
                        item.Categories.Add(new SyndicationCategory(results.Vacancies[i].ApprenticeshipFramework.Description));
                        item.Id = url;
                        //As it is using the vacancy DTO to deliver the results, possible start date is actually date the vacancy went live.
                        item.PublishDate = results.Vacancies[i].PossibleStartDate;
                   

                        items.Add(item);
                    }
                }
            }
            
            
            if (feed == null) return;

            feed.Items = items;

            SyndicationFeedFormatter formatter;
            string format = HttpContext.Current.Request.QueryString["format"] ?? "";
            if (format.Trim().ToLower() == "atom")
            {
                Response.ContentType = "application/atom+xml";
                formatter = new Atom10FeedFormatter(feed);
            }
            else
            {
                Response.ContentType = "application/rss+xml";
                formatter = new Rss20FeedFormatter(feed);
            }
            var settings = new XmlWriterSettings {
                                             NewLineHandling = NewLineHandling.None,
                                             Indent = true,
                                             Encoding = Encoding.UTF32,
                                             ConformanceLevel = ConformanceLevel.Document,
                                             OmitXmlDeclaration = true
                                         };
            var buffer = new StringBuilder();
            var cachedOutput = new StringWriter(buffer);
            using (XmlWriter writer = XmlWriter.Create(cachedOutput, settings))
            {
                if (writer != null)
                {
                    formatter.WriteTo(writer);
                    writer.Close();
                }
                
            }

            buffer.Replace(" xmlns:a10=\"http://www.w3.org/2005/Atom\"", " xmlns:atom=\"http://www.w3.org/2005/Atom\"");
            buffer.Replace("a10:", "atom:");

            Response.Output.Write(buffer.ToString());
        }

        private static string FormatFeed(VacancyDTO vacancy)
        {
            var sb = new StringBuilder();

            if (vacancy.VacancyLocationType == VacancyLocationType.National)
            {
                sb.Append("Nationwide ");
                if (vacancy.VacancyType == VacancyType.IntermediateLevelApprenticeship)
                {
                    sb.Append("Intermediate Level Apprenticeship");
                }
                else if (vacancy.VacancyType == VacancyType.AdvancedLevelApprenticeship)
                {
                    sb.Append("Advanced Level Apprenticeship");
                }
                else if (vacancy.VacancyType == VacancyType.HigherApprenticeship)
                {
                    sb.Append("Higher Apprenticeship");
                }
                else if (vacancy.VacancyType == VacancyType.Traineeship)
                {
                    sb.Append("Traineeship");
                }
                sb.Append(" with ");
                sb.Append(vacancy.EmployerName);
                sb.Append(". ");
                sb.Append(vacancy.ApprenticeshipFramework.Description);
                sb.Append(". ");
                sb.Append(vacancy.ShortDescription);
                sb.Append(" Weekly Wage: ");
                if (vacancy.WageText == "unknown")
                {
                    sb.Append("Unpaid.");
                }
                else
                {
                    //Did not work on one line testing if splitting the code works.
                    string weeklyWageTextString = vacancy.WageText;
                    sb.Append(weeklyWageTextString);
                }
                sb.Append(" Vacancy Reference: ");
                sb.Append(vacancy.VacancyReference);
                sb.Append(". Closing Date: ");
                sb.Append(vacancy.ClosingDate.ToString("dd/MM/yyyy"));
            }
            else
            {
                if (vacancy.VacancyType == VacancyType.IntermediateLevelApprenticeship)
                {
                    sb.Append("Intermediate Level Apprenticeship");
                }
                else if (vacancy.VacancyType == VacancyType.AdvancedLevelApprenticeship)
                {
                    sb.Append("Advanced Level Apprenticeship");
                }
                else if (vacancy.VacancyType == VacancyType.HigherApprenticeship)
                {
                    sb.Append("Higher Apprenticeship");
                }
                else if (vacancy.VacancyType == VacancyType.Traineeship)
                {
                    sb.Append("Traineeship");
                }
                sb.Append(" with ");
                sb.Append(vacancy.EmployerName);
                sb.Append(" in ");
                sb.Append(vacancy.VacancyAddress.Town);
                sb.Append(". ");
                sb.Append(vacancy.ApprenticeshipFramework.Description);
                sb.Append(". ");
                sb.Append(vacancy.ShortDescription);
                sb.Append(" Weekly Wage: ");
                if (vacancy.WageText == "unknown")
                {
                    sb.Append("Unpaid.");
                }
                else
                {
                    //Did not work on one line testing if splitting the code works.
                    string weeklyWageTextString = vacancy.WageText;
                    sb.Append(weeklyWageTextString);
                }
                sb.Append(" Vacancy Reference: ");
                sb.Append(vacancy.VacancyReference);
                sb.Append(". Closing Date: ");
                sb.Append(vacancy.ClosingDate.ToString("dd/MM/yyyy"));
            }

            return sb.ToString();
        }
    }
}
