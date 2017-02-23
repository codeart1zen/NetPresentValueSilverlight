using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPVCalculator.Services
{
    public class StateSettings
    {
        public decimal DiscountRateLowerBound;
        public decimal DiscountRateUpperBound;
        public decimal DiscountRateSampleInterval;
        public decimal NextDiscountRateToCalculate;
        public List<Cashflow> Cashflows;
    }
}