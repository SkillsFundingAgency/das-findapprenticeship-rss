using VacancyRssFeedService.Common;

namespace VacancyRssFeedService.Data
{
    public interface IVacancyDataService
    {
        VacancyRssDTO GetVacanciesForRss(VacancySearchDetailForRssDTO parameters);
    }
}