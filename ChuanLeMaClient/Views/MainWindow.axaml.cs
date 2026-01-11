
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ChuanLeMaClient.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.Loaded += MainWindow_OnAttachedToVisualTree;
            InitializeComponent();
           
        }
        private void MainWindow_OnAttachedToVisualTree(object? sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                AtomUI.Desktop.Controls.WindowNotificationManager _notificationManager = new AtomUI.Desktop.Controls.WindowNotificationManager(this)
                {
                    MaxItems = 3,
                    Position = AtomUI.Desktop.Controls.NotificationPosition.TopRight
                };
                vm.SetNotificationManager(_notificationManager);
            }

        }
    }
}