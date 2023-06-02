using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNote_maui.Converters
{
    public class TextToHeightConverter : IValueConverter
    {
        const int diffHeight = 25;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {        
            string text = value as string;

            if (text == null) return diffHeight;

            var splitedText = text.Split(new char[] { '\n', '\r' });

            if( splitedText.Length == 1 )
                return 40;

            return splitedText.Length * diffHeight;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
