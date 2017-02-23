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
    public class CalculationServiceTests
    {
        // Further tests are omitted for brevity - the purpose here is demonstrating the approach
        // Tests could also use various testing extensions such as a mocking library, again omitted for brevity

        [TestInitialize]
        public void Setup()
        {            
            AppSvcCtx.Testing_DIContainer = new UnityContainer();            
            AppSvcCtx.Testing_DIContainer.RegisterInstance<ILoggingProvider>(new MockLoggingProvider());
            AppSvcCtx.Testing_DIContainer.RegisterInstance<IStateProvider>(new MockStateProvider());
            AppSvcCtx.Testing_DIContainer.RegisterInstance<ICalendarService>(new CalendarService());
        }

        [TestMethod]
        public void TestSetupNPVRangeCalculationStoresState()
        {
            var cashflows = new List<Cashflow>();
            var calc = new CalculationService();
            var sessionId = Guid.NewGuid().ToString();
                        
            calc.SetupNPVRangeCalculation(1M, 5M, 2M, null, sessionId);
            var calcState = AppSvcCtx.Resolve<IStateProvider>()[sessionId];
            Assert.AreEqual(1M,  calcState.DiscountRateLowerBound);            
            Assert.AreEqual(5M, calcState.DiscountRateUpperBound);
            Assert.AreEqual(2M, calcState.DiscountRateSampleInterval);
            Assert.AreEqual(1M, calcState.NextDiscountRateToCalculate);
            Assert.ReferenceEquals(cashflows, calcState.Cashflows); 
        }

        [TestMethod]
        public void TestSetupNPVRangeCalculationReturnCorrectNumberOfCalculations()
        {
            var calc = new CalculationService();
            var actualNumberOfCalculations = calc.SetupNPVRangeCalculation(1M, 10M, 1M, null, "");
            Assert.AreEqual(10, actualNumberOfCalculations);
            actualNumberOfCalculations = calc.SetupNPVRangeCalculation(1M, 10M, 1.1M, null, "");
            Assert.AreEqual(10, actualNumberOfCalculations);
            actualNumberOfCalculations = calc.SetupNPVRangeCalculation(1M, 10M, 1.2M, null, "");
            Assert.AreEqual(9, actualNumberOfCalculations);
        }

        [TestMethod]
        public void TestCalculateSingleCashflowNPV()
        {
            var cashflow = new Cashflow() { AmountPerPayment = 100, NumberOfPayments = 1, PaymentIntervalInMonths = 12, Name = "TST" };
            var expectedValue = 90M;
            var actualValue = CalculationService.CalculateSingleCashflowNPV(10M, cashflow);
            Assert.AreEqual(expectedValue, actualValue);

            cashflow.NumberOfPayments = 2;
            expectedValue = 171M;
            actualValue = CalculationService.CalculateSingleCashflowNPV(10M, cashflow);
            Assert.AreEqual(expectedValue, actualValue);

            cashflow.NumberOfPayments = 3;
            expectedValue = 243.9M;
            actualValue = CalculationService.CalculateSingleCashflowNPV(10M, cashflow);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void TestCalculateCashflowSeriesNPVSumsValues()
        {
            var cashflow = new Cashflow() { AmountPerPayment = 100, NumberOfPayments = 1, PaymentIntervalInMonths = 12, Name = "TST" };
            var expectedValue = 90M;
            var actualValue = CalculationService.CalculateSingleCashflowNPV(10M, cashflow);
            Assert.AreEqual(expectedValue, actualValue);

            var cashflows = new List<Cashflow>();
            cashflows.Add(new Cashflow() { AmountPerPayment = 100, NumberOfPayments = 1, PaymentIntervalInMonths = 12, Name = "1" });
            cashflows.Add(new Cashflow() { AmountPerPayment = 100, NumberOfPayments = 1, PaymentIntervalInMonths = 12, Name = "2" });
            cashflows.Add(new Cashflow() { AmountPerPayment = 100, NumberOfPayments = 1, PaymentIntervalInMonths = 12, Name = "3" });

            expectedValue = 90M * 3;
            actualValue = CalculationService.CalculateCashflowSeriesAggregateNPV(10M, cashflows);
            Assert.AreEqual(expectedValue, actualValue);
        }

        
    }
}
