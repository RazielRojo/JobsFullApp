using JobsCoreApp.Controllers;
using JobsCoreApp.Model;
using JobsCoreApp.Module;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace JobsUnitTest
{
    public class TestJobs
    {
        #region Property  
        public Mock<IDayStatisticsModule> mock = new Mock<IDayStatisticsModule>();
        #endregion
        private List<DayStatistics> mockList = new List<DayStatistics>();

        private void populateList()
        {

            DateTime baseDate =  DateTime.Parse("May 4, 2021");
            for (int i = 4; i < 29; i++)
            {
                mockList.Add(new DayStatistics { ID = i, ActiveJobs = 10 + i, JobsViews = 10 + i * 2, PredictedViews = i < 25 ? 10 + i * 3 : 10 + i * 3 + i * (3 ^ 2), DayDate = baseDate });
                baseDate = baseDate.AddDays(1);
            }
        }

        [Fact]
        public async void GetEmployeeDetails()
        {
            populateList();
            mock.Setup(p => p.GetDayStatistics()).ReturnsAsync(mockList);
            DayStatisticsController dayStatisticsController = new DayStatisticsController(mock.Object);
            var result = await dayStatisticsController.GetDayStatistics();
            Assert.True(mockList.Equals(((Microsoft.AspNetCore.Mvc.OkObjectResult)result).Value));
        }
    }
}
