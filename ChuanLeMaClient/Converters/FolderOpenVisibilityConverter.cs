using Avalonia.Data.Converters;
using ChuanLeMaClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Converters
{
    internal class FolderOpenVisibilityConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is List<TagInfo> infos)
            {
                if (infos.Any() && infos.Any(o => o.Name == "目录")) return true;
                return false;
            }
            else
            {
                return false;
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
