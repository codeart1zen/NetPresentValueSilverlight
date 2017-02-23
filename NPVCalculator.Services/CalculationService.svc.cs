using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.ServiceModel.Activation;
using System.Web;

namespace NPVCalculator.Services
{    
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class CalculationService : ICalculationService
    {
        public int SetupNPVRangeCalculation(decimal DiscountRateLowerBound, decimal DiscountRateUpperBound, decimal DiscountRateSampleInterval, List<Cashflow> Cashflows, string SessionId)
        {
            var dateNow = AppSvcCtx.Resolve<ICalendarService>().GetDate(); // simply to show SOA wiring of WCF to WCF done via DI
            AppSvcCtx.Resolve<ILoggingProvider>().Log("CalculationService.SetupNPVRangeCalculation [" + SessionId + "] " + dateNow.ToString());

            var state = new StateSettings() { DiscountRateLowerBound = DiscountRateLowerBound, DiscountRateUpperBound = DiscountRateUpperBound, DiscountRateSampleInterval = DiscountRateSampleInterval, Cashflows = Cashflows, NextDiscountRateToCalculate = DiscountRateLowerBound };
            AppSvcCtx.Resolve<IStateProvider>()[SessionId] = state;            

            return getNumberOfCalculations(state);
        }

        private int getNumberOfCalculations(StateSettings state)
        {
            int numberOfCalculations = 1;

            decimal discountRate = state.DiscountRateLowerBound + state.DiscountRateSampleInterval;

            while (discountRate < state.DiscountRateUpperBound)
            {
                discountRate += state.DiscountRateSampleInterval;
                numberOfCalculations += 1;
            }

            numberOfCalculations += 1; // for upper bound

            return numberOfCalculations;
        }

        public NetPresentValue GetNextNPV(string SessionId)
        {
            
            var state = AppSvcCtx.Resolve<IStateProvider>()[SessionId];

            if (state == null) { throw new ApplicationException("State missing, presumably caused by service restart, try again ( session = " + SessionId.ToString() + " )" ); }
            
            AppSvcCtx.Resolve<ILoggingProvider>().Log("CalculationService.GetNextNPV [" + SessionId + "] " + state.NextDiscountRateToCalculate.ToString());

            //TODO: Uncomment this to see the error handling in action - simple error handling instead of silverlight faults, future versions should have a full network stack and avoid the SilverlightFault for proper error information
            //if (state.NextDiscountRateToCalculate > 10M) { throw new Exception(); }

            var result = new NetPresentValue();
            result.DiscountRate = state.NextDiscountRateToCalculate;
            state.NextDiscountRateToCalculate += state.DiscountRateSampleInterval;
            if (state.NextDiscountRateToCalculate > state.DiscountRateUpperBound)
            {
                state.NextDiscountRateToCalculate = state.DiscountRateUpperBound;
            }

            result.Amount = CalculateCashflowSeriesAggregateNPV(result.DiscountRate, state.Cashflows);

            return result;
        }

        public static decimal CalculateCashflowSeriesAggregateNPV(decimal DiscountRate, List<Cashflow> Cashflows)
        {
            var totalNPV = 0M;
            foreach (var cashflow in Cashflows)
            {
                var cashflowNPV = CalculateSingleCashflowNPV(DiscountRate, cashflow);
                totalNPV += cashflowNPV;
            }

            return totalNPV;
        }
        
        public static decimal CalculateSingleCashflowNPV(decimal DiscountRate, Cashflow cashflow)
        {
            decimal cashflowNPV = 0;
            var periodsInYear = 12 / cashflow.PaymentIntervalInMonths;
            decimal discountRatePerPeriod = DiscountRate / periodsInYear;
            decimal discountFactorPerPeriod = (100M - (DiscountRate)) / 100M;
            decimal totalDiscountFactor = discountFactorPerPeriod;
            for (int i = 0; i < cashflow.NumberOfPayments; i++)
            {                
                var paymentNPV = cashflow.AmountPerPayment * totalDiscountFactor;
                cashflowNPV += paymentNPV;

                totalDiscountFactor = totalDiscountFactor * discountFactorPerPeriod;               
            }
            return cashflowNPV;
        }

    }

}
