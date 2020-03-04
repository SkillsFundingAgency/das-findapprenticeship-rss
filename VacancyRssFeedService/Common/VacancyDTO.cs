using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VacancyRssFeedService.Common
{
    public class VacancyDTO
    {
        public int VacancyId { get; set; }
        public int VacancyReference { get; set; }
        public string WageText { get; set; }
        public string VacancyTitle { get; set; }
        public string EmployerName { get; set; }
        public string ShortDescription { get; set; }
        public AddressDetails VacancyAddress { get; set; }

        public VacancyLocationType VacancyLocationType { get; set; }

        public VacancyType VacancyType { get; set; }

        /// <summary>
        /// Is this a valid one as another attribute Occupation already exists???  There is
        /// no reference to ApprenticeshipFrameworkId field in the VacancyManager and
        /// Vacancy tables.  Is this going to be referenced through EmployerID???
        /// </summary>
        public ApprenticeshipFramework ApprenticeshipFramework { get; set; }

        private DateTime _closingDate = new DateTime(1753, 1, 1);
        public DateTime ClosingDate
        {
            get { return _closingDate; }
            set
            {
                // Check for minimum date supported by SQL server
                _closingDate = (DateTime.Compare(value, new DateTime(1753, 1, 1)) < 0)
                                   ? new DateTime(1753, 1, 1)
                                   : value;
            }
        }

        private DateTime _possibleStartDate = new DateTime(1753, 1, 1);
        public DateTime PossibleStartDate
        {
            get { return _possibleStartDate; }
            set
            {
                // Check for minimum date supported by SQL server
                _possibleStartDate = (DateTime.Compare(value, new DateTime(1753, 1, 1)) < 0)
                                         ? new DateTime(1753, 1, 1)
                                         : value;
            }
        }


        public VacancyDTO()
        {
            VacancyAddress = new AddressDetails();
            ApprenticeshipFramework = new ApprenticeshipFramework();
            VacancyLocationType = VacancyLocationType.Standard;
            VacancyType = VacancyType.IntermediateLevelApprenticeship;
        }
    }
}