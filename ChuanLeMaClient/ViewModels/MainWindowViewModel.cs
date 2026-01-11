using AtomUI.Desktop.Controls;
using AtomUI.Icons.IconPark;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using ChuanLeMaClient.Models;
using ChuanLeMaClient.Services.Implement;
using ChuanLeMaClient.Services.Inteface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using Splat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ChuanLeMaClient.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private WindowNotificationManager? _basicManager;
        /// <summary>
        /// 本地文件目录列表
        /// </summary>
        [ObservableProperty]
        public ObservableCollection<FolderFileDataModel> localFolderDataList = new();
        /// <summary>
        /// 本地工作目录
        /// </summary>
        [ObservableProperty]
        public string localWorkPath;
        /// <summary>
        /// 由窗口调用 传入通知管理器
        /// </summary>
        /// <param name="manager"></param>
        public void SetNotificationManager(WindowNotificationManager manager)
        {
            _basicManager = manager;
            //List<FolderFileDataModel> items =
            //   [
            //       new FolderFileDataModel
            //        {
            //             Name = "John Brown", Size = 32,
            //            Tags =
            //            [
            //               new TagInfo { Name = "目录", Color = "geekblue" }
            //            ]
            //        },
            //        new FolderFileDataModel
            //        {
            //             Name = "Jim Green", Size = 42,
            //            Tags =
            //            [
            //                new TagInfo { Name = "目录", Color = "geekblue" }
            //            ]
            //        },
            //        new FolderFileDataModel
            //        {
            //            Name = "Joe Black", Size = 32,
            //            Tags =
            //            [
            //                new TagInfo { Name = "文件", Color    = "green" }
            //            ]
            //        }
            //   ];
            //LocalFolderDataList.AddRange(items);
        }
        private ITestService _testService;
        private readonly ILocalFolderFileService _localFolderFileService;
        public MainWindowViewModel(ITestService testService, ILocalFolderFileService localFolderFileService)
        {
            _testService = testService;
            _localFolderFileService = localFolderFileService;
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

            //初始化本地文件列表
            InitLocalFolderFiles();
        }
        private void InitLocalFolderFiles()
        {
            LocalWorkPath= Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var list = _localFolderFileService.GetAllFoldersFiles(LocalWorkPath);
            LocalFolderDataList.Clear();
            LocalFolderDataList.AddRange(list);
        } 
        [RelayCommand]
        public async Task ClickMe()
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
        public void UploadLink(FolderFileDataModel info)
        {
            _basicManager?.Show(new Notification(
                                 "温馨提示",
                                 $"上传成功!{info.Name}"
                             ));
        }
        [RelayCommand]
        public void Login()
        {
            _basicManager?.Show(new Notification(
                                 "温馨提示",
                                 $"登录失败,账号或密码错误！"
                             ));
            _testService.Hello();
        }
    }
}
