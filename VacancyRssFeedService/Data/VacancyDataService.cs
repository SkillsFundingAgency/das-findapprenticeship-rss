using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VacancyRssFeedService.Common;
using VacancyRssFeedService.Data.Context;
using VacancyRssFeedService.Logic;

namespace VacancyRssFeedService.Data
{
    public class VacancyDataService : Data.IVacancyDataService
    {
        /// <summary>
        /// format an int as a vacancyType
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static VacancyType FormatIntAsVacancyType(int value)
        {
            VacancyType result = VacancyType.Unspecified;
            switch (value)
            {
                case 0:
                    result = VacancyType.Unspecified;
                    break;
                case 1:
                    result = VacancyType.IntermediateLevelApprenticeship;
                    break;
                case 2:
                    result = VacancyType.AdvancedLevelApprenticeship;
                    break;
                case 3:
                    result = VacancyType.HigherApprenticeship;
                    break;
                case 4:
                    result = VacancyType.Traineeship;
                    break;
            }
            return result;
        }

        public VacancyRssDTO GetVacanciesForRss(VacancySearchDetailForRssDTO parameters)
        {
            var retVacacies = new VacancyRssDTO();
            var vacancyList = new List<VacancyDTO>();
            var feedTitle = string.Empty;
            var feedDescription = string.Empty;
            var feedImageUrl = string.Empty;
            var feedCopyrightInfo = string.Empty;
            var alternateLink = string.Empty;
            using (var db = DataContextFactory.CreateDataContext<VacancySearchDataContext>())
            {
                var result = db.uspGetVacanciesForRSS((int)parameters.FeedType,
                                                      parameters.DayRange,
                                                      parameters.FrameworkCode,
                                                      parameters.OccupationCode,
                                                      parameters.CountyCode,
                                                      parameters.Town,
                                                      parameters.RegionCode,
                                                      parameters.VacancyReferenceId,
                                                      ref feedTitle,
                                                      ref feedDescription,
                                                      ref feedImageUrl,
                                                      ref feedCopyrightInfo,
                                                      ref alternateLink).ToList();


                for (int i = 0; i < result.Count(); i++)
                {
                    var vacancy = new VacancyDTO
                    {
                        VacancyId = result[i].VacancyId,
                        VacancyTitle = result[i].VacancyTitle,
                        EmployerName = result[i].EmployerTradingName,
                        ShortDescription = result[i].ShortDescription,
                        VacancyType = FormatIntAsVacancyType(result[i].VacancyType.GetValueOrDefault()),
                        ApprenticeshipFramework =
                                               new ApprenticeshipFramework { Description = result[i].JobRole },
                        ClosingDate = result[i].ClosingDate.GetValueOrDefault(),
                        VacancyAddress =
                                                new AddressDetails { Town = result[i].VacancyLocation },
                        PossibleStartDate = result[i].PublishDate,
                        VacancyLocationType = (VacancyLocationType)result[i].VacancyLocationTypeId,
                        VacancyReference = Convert.ToInt32(result[i].VacancyReferenceNumber),
                        WageText = result[i].WageText
                    };
                    vacancyList.Add(vacancy);

                }
            }

            retVacacies.FeedTitle = feedTitle;
            retVacacies.FeedDescription = feedDescription;
            retVacacies.FeedImageUrl = feedImageUrl;
            retVacacies.FeedCopyrightInformation = feedCopyrightInfo;
            retVacacies.Vacancies = vacancyList;
            retVacacies.AlternateLink = alternateLink;

            return retVacacies;
        }
    }
}