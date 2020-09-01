using E_Shop.Consts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace E_Shop.Core.Converters
{
    public class MessageDialogTypeToStringConverter : IValueConverter
    {
        public string Success { get; set; }
        public string Error { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;
            MessageDialogType input = (MessageDialogType)value;
            return input == MessageDialogType.Success ? Success : Error;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
