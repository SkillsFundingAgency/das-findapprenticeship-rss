using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacancyRssFeedService.Common;
using VacancyRssFeedService.Logic;

namespace SFA.FindApprenticeship.Rss.UnitTests
{
    [TestFixture]
    public class WhenTheLogicControllerIsCalled
    {
        private Mock<VacancyRssFeedService.Data.IVacancyDataService> _controller = new Mock<VacancyRssFeedService.Data.IVacancyDataService>(MockBehavior.Strict);

        [Test]
        public void TheDataLayerIsCalledOnce()
        {
            _controller.Setup(s => s.GetVacanciesForRss(It.IsAny<VacancySearchDetailForRssDTO>()))
                .Returns(new VacancyRssDTO());

            var sut = new VacancyLogicService(_controller.Object);
            sut.GetVacanciesForRss(new VacancySearchDetailForRssDTO());

            _controller.Verify(s => s.GetVacanciesForRss(It.IsAny<VacancySearchDetailForRssDTO>()), Times.Once);
        }
    }
}
