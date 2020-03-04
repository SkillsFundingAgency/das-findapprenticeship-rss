using VacancyRssFeedService.Common;

namespace VacancyRssFeedService.Logic
{
    public class VacancyLogicService : IVacancyLogicService
    {
        private readonly Data.IVacancyDataService _dataController;

        public VacancyLogicService(Data.IVacancyDataService dataController)
        {
            _dataController = dataController;
        }

        public VacancyRssDTO GetVacanciesForRss(VacancySearchDetailForRssDTO parameters)
        {
            return _dataController.GetVacanciesForRss(parameters);
        }
    }
}
