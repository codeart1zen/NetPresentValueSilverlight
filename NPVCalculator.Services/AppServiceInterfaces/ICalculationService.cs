using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace NPVCalculator.Services
{
    [ServiceContract]
    public interface ICalculationService
    {
        [OperationContract]
        int SetupNPVRangeCalculation(decimal DiscountRateLowerBound, decimal DiscountRateUpperBound, decimal DiscountRateSampleInterval, List<Cashflow> Cashflows, string SessionId);
        [OperationContract]
        NetPresentValue GetNextNPV(string SessionId);
    }
}
