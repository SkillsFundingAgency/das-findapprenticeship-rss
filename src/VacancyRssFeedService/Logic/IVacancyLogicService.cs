using VacancyRssFeedService.Common;

namespace VacancyRssFeedService.Logic
{
    public interface IVacancyLogicService
    {
        VacancyRssDTO GetVacanciesForRss(VacancySearchDetailForRssDTO parameters);
    }
}