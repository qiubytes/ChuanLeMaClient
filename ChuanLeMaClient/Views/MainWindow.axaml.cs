using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ChuanLeMaClient.ViewModels;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ChuanLeMaClient.Views
{
    public partial class MainWindow : ActiveTopLevelWindow// AtomUI.Desktop.Controls.Window
    {
        public MainWindow()
        {
            if (Design.IsDesignMode)
            {
                this.Loaded += MainWindow_OnAttachedToVisualTree; 
                InitializeComponent();
            }
        }

        public MainWindow(ILogger<ActiveTopLevelWindow> logger) : base(logger)
        {
            this.Loaded += MainWindow_OnAttachedToVisualTree;
            InitializeComponent();

        }
        private async void MainWindow_OnAttachedToVisualTree(object? sender, RoutedEventArgs e)
        {
            if (DataContext is MainWindowViewModel vm)
            {
                AtomUI.Desktop.Controls.WindowNotificationManager _notificationManager = new AtomUI.Desktop.Controls.WindowNotificationManager(this)
                {
                    MaxItems = 3,
                    Position = AtomUI.Desktop.Controls.NotificationPosition.TopRight
                };
                await vm.SetNotificationManager(_notificationManager);
                AtomUI.Desktop.Controls.WindowMessageManager message = new AtomUI.Desktop.Controls.WindowMessageManager(this)
                {
                    MaxItems = 3,
                };
                vm.SetMessageManager(message);
            }

        }
    }
}