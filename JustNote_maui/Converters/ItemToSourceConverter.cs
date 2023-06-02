using JustNote_maui.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNote_maui.Converters
{
    public class ItemToSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Type type)
            {
                if(type == typeof(NoteModel)) {
                    return "simplenote.png";
                }
                if(type == typeof(NoteListModel))
                {
                    return "listnote.png";
                }
            }
            return string.Empty;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
