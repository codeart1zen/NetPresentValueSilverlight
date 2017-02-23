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

namespace NPVCalculator.Model
{
    public class NPVPoint
    {
        public decimal DiscountRate { get; set; }
        public decimal NPV { get; set; }
    }
}
