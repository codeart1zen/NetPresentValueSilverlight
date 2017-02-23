using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPVCalculator.Services;
using Microsoft.Practices.Unity;

namespace NPVCalculatorServiceTests
{
    [TestClass]
    public class CalendarServiceTests
    {
        // Further tests are omitted for brevity - the purpose here is demonstrating the approach
        // Tests could also use various testing extensions such as a mocking library, again omitted for brevity

        /// This test demonstrates the secondary purpose of using DI - the ability to test each part of the system in isolation
        /// The primary purpose of course is good OO design avoiding coupling, creating orthagonal independant subsystems
        /// </summary>
        [TestMethod]
        public void TestGetDate()        
        {            
            AppSvcCtx.Testing_DIContainer = new UnityContainer();
            var mockLogger = new MockLoggingProvider();
            AppSvcCtx.Testing_DIContainer.RegisterInstance<ILoggingProvider>(mockLogger);
            var calSvc = new CalendarService();
            var expectedDate = DateTimeOffset.Now.Date;

            Assert.AreEqual(0, mockLogger.LogMsgs.Count);
            Assert.AreEqual(expectedDate, calSvc.GetDate());
            Assert.AreEqual(1, mockLogger.LogMsgs.Count);

            Assert.IsTrue(mockLogger.LogMsgs[0].StartsWith("CalendarService.GetDate"));
            Assert.IsTrue(mockLogger.LogMsgs[0].EndsWith(expectedDate.ToShortDateString()));
        }
    }
}
