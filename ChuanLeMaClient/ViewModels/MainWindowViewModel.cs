using AtomUI.Desktop.Controls;
using AtomUI.Icons.IconPark;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using ChuanLeMaClient.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Splat;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ChuanLeMaClient.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private WindowNotificationManager? _basicManager;
        [ObservableProperty]
        public ObservableCollection<DataGridBaseInfo> dataList = new();
        public void SetNotificationManager(WindowNotificationManager manager)
        {
            _basicManager = manager;
            List<DataGridBaseInfo> items =
               [
                   new DataGridBaseInfo
                    {
                        Key   = "1", Name = "John Brown", Size = 32,
                        Tags =
                        [
                           new TagInfo { Name = "目录", Color = "geekblue" }
                        ]
                    },
                    new DataGridBaseInfo
                    {
                        Key   = "2", Name = "Jim Green", Size = 42,
                        Tags =
                        [
                            new TagInfo { Name = "目录", Color = "geekblue" }
                        ]
                    },
                    new DataGridBaseInfo
                    {
                        Key   = "3", Name = "Joe Black", Size = 32,
                        Tags =
                        [
                            new TagInfo { Name = "文件", Color    = "green" },
                            new TagInfo { Name = "TEACHER", Color = "geekblue" }
                        ]
                    }
               ];
            dataList.AddRange(items);
        }
        public MainWindowViewModel()
        {
            // 延迟到UI线程空闲时初始化
            //Dispatcher.UIThread.Post(() =>
            //{
            //    if (Avalonia.Application.Current?.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime lifetime &&
            //        lifetime.MainWindow != null &&
            //        lifetime.MainWindow.IsActive)
            //    {
            //        var topLevel = TopLevel.GetTopLevel(lifetime.MainWindow);
            //        _basicManager = new WindowNotificationManager(topLevel)
            //        {
            //            MaxItems = 3,
            //            Position = NotificationPosition.TopLeft
            //        };
            //    }
            //}, DispatcherPriority.Background);
        }
        public string Greeting { get; } = "Welcome to Avalonia!";
        [RelayCommand]
        public async void ClickMe()
        {
            var box = MessageBoxManager
    .GetMessageBoxStandard("Caption", "Are you sure you would like to delete appender_replace_page_1?",
        ButtonEnum.Ok);
            var result = await box.ShowAsync();
        }
        [RelayCommand]
        public void ClickSub()
        {
            _basicManager?.Show(new Notification(
                                    "Notification Title",
                                    "Hello, AtomUI/Avalonia!"
                                ));
        }
        [RelayCommand]
        public void UploadLink(DataGridBaseInfo info)
        {
            _basicManager?.Show(new Notification(
                                 "温馨提示",
                                 $"上传成功!{info.Name}"
                             ));
        }
    }
}
