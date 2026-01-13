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
    /// <summary>
    /// 任务进度百分比转换器
    /// </summary>
    public class TaskCompletePercentConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is TaskModel model)
            {
                if (model.FileSize == 0)
                {
                    return 0;
                }
                double percent = (double)model.CompletedSize / model.FileSize * 100.0;
                return percent;
            }
            else
                return 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
