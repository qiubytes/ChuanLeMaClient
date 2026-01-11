using AtomUI.Desktop.Controls;
using Avalonia;
using Avalonia.Data;
using Avalonia.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChuanLeMaClient.Behaviours
{
    /// <summary>
    /// 右键点击行为
    /// </summary>
    public class DataGridRightClickBehavior
    {

        /// <summary>
        /// 右键命令属性
        /// </summary>
        public static readonly AttachedProperty<ICommand> RightClickCommandProperty =
            AvaloniaProperty.RegisterAttached<DataGridRightClickBehavior, AtomUI.Desktop.Controls.DataGrid, ICommand>(
                "RightClickCommand",
                null,
                false,
                BindingMode.OneWay);

        /// <summary>
        /// 右键命令参数
        /// </summary>
        public static readonly AttachedProperty<object> RightClickCommandParameterProperty =
            AvaloniaProperty.RegisterAttached<DataGridRightClickBehavior, AtomUI.Desktop.Controls.DataGrid, object>(
                "RightClickCommandParameter", null, false, BindingMode.OneWay);

        public static ICommand GetRightClickCommand(AtomUI.Desktop.Controls.DataGrid element)
        {
            return element.GetValue(RightClickCommandProperty);
        }

        public static void SetRightClickCommand(AtomUI.Desktop.Controls.DataGrid element, ICommand value)
        {
            element.SetValue(RightClickCommandProperty, value);
        }

        public static object GetRightClickCommandParameter(DataGrid element)
        {
            return element.GetValue(RightClickCommandParameterProperty);
        }

        public static void SetRightClickCommandParameter(DataGrid element, object value)
        {
            element.SetValue(RightClickCommandParameterProperty, value);
        }

        // 静态构造函数 - 注册属性变更事件
        static DataGridRightClickBehavior()
        {
            RightClickCommandProperty.Changed.AddClassHandler<AtomUI.Desktop.Controls.DataGrid>(OnRightClickCommandChanged);
        }
        /// <summary>
        /// 当 RightClickCommand 属性变化时
        /// </summary>
        private static void OnRightClickCommandChanged(AtomUI.Desktop.Controls.DataGrid dataGrid, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.OldValue is ICommand)
            {
                // 移除旧的事件处理器
                dataGrid.PointerReleased -= OnDataGridPointerReleased;
            }

            if (e.NewValue is ICommand)
            {
                // 添加新的事件处理器
                dataGrid.PointerReleased += OnDataGridPointerReleased;
            }
        }

        /// <summary>
        /// 鼠标释放事件处理
        /// </summary>
        private static void OnDataGridPointerReleased(object sender, PointerEventArgs e)
        {
            if (sender is not AtomUI.Desktop.Controls.DataGrid dataGrid)
                return;

            // 检查是否是右键释放
            if (e.GetCurrentPoint(dataGrid).Properties.PointerUpdateKind == PointerUpdateKind.RightButtonReleased)
            {
                // 获取命令和参数
                var command = GetRightClickCommand(dataGrid);
                var parameter = GetRightClickCommandParameter(dataGrid);
                // 执行命令
                if (command != null && command.CanExecute(parameter))
                {
                    command.Execute(parameter);
                }

                // 标记事件已处理
                e.Handled = true;
            }
        }


    }
}
