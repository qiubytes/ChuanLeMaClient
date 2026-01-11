using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Converters
{
    /// <summary>
    /// 文件Size转换为可读单位
    /// </summary>
    public class FileSizeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is long size)
            {
                //单位 B
                if (size < 1024)
                {
                    return $"{size} B";
                }
                else if (size < 1024 * 1024)
                {
                    return $"{(size / 1024.0):F2} KB";
                }
                else if (size < 1024 * 1024 * 1024)
                {
                    return $"{(size / (1024.0 * 1024.0)):F2} MB";
                }
                else
                {
                    return $"{(size / (1024.0 * 1024.0 * 1024.0)):F2} GB";
                }

            }
            else return "";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return "";
            //throw new NotImplementedException();
        }
    }
}
