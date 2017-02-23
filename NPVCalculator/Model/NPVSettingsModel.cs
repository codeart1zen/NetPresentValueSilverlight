using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace NPVCalculator.Model
{
    public class NPVSettingsModel 
    {
        public decimal DiscountRateLowerBound;
        public decimal DiscountRateUpperBound;
        public decimal DiscountRateSampleInterval;
    }
}
