using AtomUI.Icons.AntDesign;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChuanLeMaClient.Converters
{
    /// <summary>
    /// 任务列表操作按钮可见性转换器
    /// </summary>
    public class TaskModelOpVisibleConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            // T1: DataTemplate 实例化，绑定建立（此时 DataContext 可能为 null）
            //    → 转换器第一次调用，values[0] = {(unset)}
            //    → 返回 false，按钮隐藏 

            // 快速检查：如果有 unset，直接返回 false
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] is Avalonia.UnsetValueType)
                    return false;
            }

            if (values.Count != 2) return false;

            string TaskStatus = values[0]?.ToString() ?? string.Empty;
            string OpName = values[1]?.ToString() ?? string.Empty;
            switch (TaskStatus)
            {
                case "进行中":
                    if (OpName == "暂停" || OpName == "停止" || OpName == "删除")
                    { return true; }
                    else
                    {
                        return false;
                    }
                case "已完成":
                    if (OpName == "删除")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "暂停":
                    if (OpName == "继续" || OpName == "停止" || OpName == "删除")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "停止":
                    if (OpName == "删除")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case "失败":
                    if (OpName == "继续" || OpName == "删除")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }
    }
}
