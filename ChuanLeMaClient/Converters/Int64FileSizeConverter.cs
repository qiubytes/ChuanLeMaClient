using Avalonia.Data.Converters;
using ChuanLeMaClient.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChuanLeMaClient.Converters
{
    public class Int64FileSizeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Int64 size)
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
