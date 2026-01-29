using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChuanLeMaClient.Converters
{
    /// <summary>
    /// 文件全路径转换为文件名
    /// </summary>
    public class LocalFullPathToFileNameConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string fullname)
            {
                string filename = System.IO.Path.GetFileName(fullname);
                return filename;
            }
            else
            {
                return "";
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
