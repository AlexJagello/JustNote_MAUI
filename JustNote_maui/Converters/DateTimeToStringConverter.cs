using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNote_maui.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return FormatingDateAndTime(dateTime);
            }
            return "";

        }

        private string FormatingDateAndTime(DateTime dateTime)
        {
            string str = string.Empty;

            str = dateTime.ToShortDateString() + "  " + dateTime.Hour + ":" + dateTime.Minute;

            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
