using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeYourPhone.Core.Services.Interface;
using TradeYourPhone.Test.SetupData;

namespace TradeYourPhone.Test.TradeYourPhone.Core.Services
{
    [TestClass]
    public class ReportingServiceTest
    {
        IReportingService reportingService;
        CreateMockData cmd;

        public ReportingServiceTest()
        {
            cmd = new CreateMockData();
            reportingService = cmd.GetReportingService();
        }

        [TestMethod]
        public void GetDashboardDataTest()
        {
            var dashboardVM = reportingService.GetDashboardData(DateTime.Parse("13-10-2015", new CultureInfo("en-AU")), DateTime.Parse("17-10-2015", new CultureInfo("en-AU")));

            Assert.AreEqual(3, dashboardVM.NoOfCreatedQuotes);
            Assert.AreEqual(1, dashboardVM.NoOfFinalisedQuotes);
            Assert.AreEqual(1050, dashboardVM.TotalAmountToPay);
        }

        [TestMethod]
        public void GetDashboardDataTest2()
        {
            var dashboardVM = reportingService.GetDashboardData(DateTime.Parse("14-10-2015", new CultureInfo("en-AU")), DateTime.Parse("17-10-2015", new CultureInfo("en-AU")));

            Assert.AreEqual(2, dashboardVM.NoOfCreatedQuotes);
            Assert.AreEqual(1, dashboardVM.NoOfFinalisedQuotes);
            Assert.AreEqual(1050, dashboardVM.TotalAmountToPay);
        }

        [TestMethod]
        public void GetDashboardDataTest3()
        {
            var dashboardVM = reportingService.GetDashboardData(DateTime.Parse("17-10-2015", new CultureInfo("en-AU")), DateTime.Parse("17-10-2015", new CultureInfo("en-AU")));

            Assert.AreEqual(1, dashboardVM.NoOfCreatedQuotes);
            Assert.AreEqual(1, dashboardVM.NoOfFinalisedQuotes);
            Assert.AreEqual(1050, dashboardVM.TotalAmountToPay);
        }
    }
}
