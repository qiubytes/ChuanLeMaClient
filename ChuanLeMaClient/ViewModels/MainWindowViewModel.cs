using AtomUI.Desktop.Controls;
using AtomUI.Icons.IconPark;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using ChuanLeMaClient.Dtos;
using ChuanLeMaClient.Models;
using ChuanLeMaClient.Services.Implement;
using ChuanLeMaClient.Services.Inteface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using Microsoft.Extensions.Configuration;
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
        /// <summary>
        /// Notification 通知提醒
        /// </summary>
        private WindowNotificationManager? _basicManager;
        /// <summary>
        /// Message 全局提示
        /// </summary>
        private WindowMessageManager? _messageManager;

        private TaskWindowViewModel? _taskWindowViewModel;
        /// <summary>
        /// 上传服务
        /// </summary>
        private IUploadService? _uploadService;
        /// <summary>
        /// 下载服务
        /// </summary>
        private IDownloadService? _downloadService;
        /// <summary>
        /// 配置管理器
        /// </summary>
        private IConfiguration _configuration;
        private IFileService _fileservice;
        /// <summary>
        /// 本地文件目录列表
        /// </summary>
        [ObservableProperty] public ObservableCollection<FolderFileDataModel> localFolderDataList = new();

        /// <summary>
        /// 本地工作目录
        /// </summary>
        [ObservableProperty] public string localWorkPath;
        /// <summary>
        /// 远程文件目录列表
        /// </summary>
        [ObservableProperty] public ObservableCollection<FolderFileDataModel> remoteFolderDataList = new();

        /// <summary>
        /// 远程工作目录
        /// </summary>
        [ObservableProperty] public string remoteWorkPath = "/";
        /// <summary>
        /// 登录按钮内容
        /// </summary>
        [ObservableProperty]
        public string loginButtonContent = "登录";
        /// <summary>
        /// 变更加载数据回调
        /// </summary>
        /// <param name="value"></param>
        partial void OnLoginButtonContentChanged(string value)
        {
            if (value == "已登录")
            {

            }
        }
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
        public void SetMessageManager(WindowMessageManager manager)
        {
            _messageManager = manager;
        }

        private ITestService _testService;
        private readonly ILocalFolderFileService _localFolderFileService;


        public MainWindowViewModel()
        {
            // 总是检查是否在设计模式下
            if (Design.IsDesignMode)
            {
            }
            else
            {
                // 如果没有通过依赖注入调用，则抛出异常
                throw new InvalidOperationException(
                    "这个 ViewModel 应该通过依赖注入创建");
            }

        }
        /// <summary>
        /// autofac 默认使用 可解析参数数量最多的构造函数
        /// </summary>
        /// <param name="testService"></param>
        /// <param name="localFolderFileService"></param>
        public MainWindowViewModel(ITestService testService,
            ILocalFolderFileService localFolderFileService,
            TaskWindowViewModel taskWindowViewModel,
            IUploadService uploadService,
            IDownloadService downloadService,
            IConfiguration configuration,
            IFileService fileService
            )
        {
            _testService = testService;
            _localFolderFileService = localFolderFileService;
            _taskWindowViewModel = taskWindowViewModel;
            _uploadService = uploadService;
            _downloadService = downloadService;
            _configuration = configuration;
            _fileservice = fileService;
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
            LocalWorkPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            this.LoadLocalFolderFiles();

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
            //_basicManager?.Show(new Notification(
            //    "温馨提示",
            //    $"上传成功!{info.Name}"
            //));
            _uploadService?.AddTask(System.IO.Path.Combine(LocalWorkPath, info.Name), "test", "token");
        }
        [RelayCommand]
        public void DownloadLink(FolderFileDataModel info)
        {
            //_basicManager?.Show(new Notification(
            //    "温馨提示",
            //    $"上传成功!{info.Name}"
            //));
            _downloadService?.AddTask(info, System.IO.Path.Combine(LocalWorkPath, info.Name), RemoteWorkPath + info.Name, "token");

        }
        /// <summary>
        /// 打开文件夹选择对话框
        /// </summary>
        [RelayCommand]
        public async Task OpenFolderDialog()
        {
            if (Avalonia.Application.Current?.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime lifetime)
            {
                Avalonia.Controls.Window window = lifetime.MainWindow;
                var storageprovider = window.StorageProvider;
                var folders = await storageprovider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
                {
                    AllowMultiple = false
                });
                if (folders.Count > 0)
                {
                    LocalWorkPath = folders[0].Path.LocalPath;
                    this.LoadLocalFolderFiles();
                }
            }
        }
        /// <summary>
        /// 打开目录
        /// </summary>
        /// <param name="info"></param>
        [RelayCommand]
        public void LinkOpenFolder(FolderFileDataModel info)
        {
            string fullPath = System.IO.Path.Combine(LocalWorkPath, info.Name);
            try
            {
                LocalWorkPath = fullPath;
                this.LoadLocalFolderFiles();
            }
            catch (Exception ex)
            {
                _basicManager?.Show(new Notification(
                    "温馨提示",
                    $"打开失败!{ex.Message}"
                ));
            }
        }
        /// <summary>
        /// 打开远程目录
        /// </summary>
        /// <param name="info"></param>
        [RelayCommand]
        public void LinkOpenFolderRemote(FolderFileDataModel info)
        {
            _messageManager.Show(new AtomUI.Desktop.Controls.Message(
                                    type: MessageType.Success,
                                    content: "功能开发中...",
                                    expiration: TimeSpan.FromSeconds(1)
                                  ));
        }
        /// <summary>
        /// 返回上级目录
        /// </summary>
        [RelayCommand]
        public void CancelParentFolder()
        {
            try
            {
                LocalWorkPath = LocalWorkPath.TrimEnd(System.IO.Path.DirectorySeparatorChar);
                var parentDir = System.IO.Directory.GetParent(LocalWorkPath);
                if (parentDir != null)
                {
                    LocalWorkPath = parentDir.FullName;
                    this.LoadLocalFolderFiles();
                }
            }
            catch (Exception ex)
            {
                _basicManager?.Show(new Notification(
                    "温馨提示",
                    $"打开失败!{ex.Message}"
                ));
            }
        }
        /// <summary>
        /// 加载本地目录、文件
        /// </summary>
        /// <param name="path"></param>
        private void LoadLocalFolderFiles()
        {
            if (LocalWorkPath != null)
            {
                var list = _localFolderFileService.GetAllFoldersFiles(LocalWorkPath);
                LocalFolderDataList.Clear();
                LocalFolderDataList.AddRange(list);
            }
        }
        [RelayCommand]
        public async void Login()
        {
            //_basicManager?.Show(new Notification(
            //    "温馨提示",
            //    $"登录失败,账号或密码错误！"
            //));
            _testService.Hello();
            _messageManager?.Show(new AtomUI.Desktop.Controls.Message(
                                    type: MessageType.Loading,
                                    content: "登录中...",
                                    expiration: TimeSpan.FromSeconds(1)
                                ));
            //string ServerUrl = _configuration["ServerUrl"];
            LoginButtonContent = "已登录";
            try
            {
                ResponseResult<List<FolderFileDataModel>> res = await _fileservice.FileDirList(this.RemoteWorkPath);
                this.RemoteFolderDataList.Clear();
                this.RemoteFolderDataList.AddRange(res.data);
            }
            catch (Exception ex)
            {
                _messageManager?.Show(new AtomUI.Desktop.Controls.Message(
                                  type: MessageType.Loading,
                                  content: ex.ToString(),
                                  expiration: TimeSpan.FromSeconds(10)
                              ));
            }

        }
        [RelayCommand]
        public async void OpenTaskWindow()
        {
            if (Avalonia.Application.Current?.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime lifetime)
            {
                var taskWindow = new TaskWindow() { DataContext = _taskWindowViewModel };
                //切换主窗口
                //var oldWindow = lifetime.MainWindow;
                //lifetime.MainWindow = taskWindow;
                //taskWindow.Show();
                //oldWindow.Close();
                taskWindow.Show();
            }
        }

    }
}