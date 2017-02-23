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
using System.Windows.Data;
using System.Globalization;

namespace NPVCalculator.ViewModel
{
    /// <summary>
    /// There is an inbuilt .net class for this, but it doesn't support reversing the visibility and this is easier than using triggers
    /// </summary>
    public class BoolToVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Boolean))
            {
                return value;
            }
            else
            {
                var show = (bool)value;
                if (Reverse)
                {
                    show = !show;
                }
                return show ? Visibility.Visible : Visibility.Collapsed; 
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public bool Reverse { get; set; }
    }
 
}
