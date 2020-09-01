using E_Shop.Consts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace E_Shop.Core.Converters
{
    public class MessageDialogTypeToStyleConverter : IValueConverter
    {
        public string Success { get; set; }
        public string Error { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = (MessageDialogType)value;
            var result= input == MessageDialogType.Success ? Application.Current.FindResource(Success) as Style
                : Application.Current.FindResource(Error) as Style;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
