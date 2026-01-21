using Avalonia.Threading;
using ChuanLeMaClient.Models;
using ChuanLeMaClient.Services.Inteface;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.ViewModels
{
    public partial class TaskWindowViewModel : ObservableRecipient  //ViewModelBase
    {
        [ObservableProperty]
        public ObservableCollection<TaskModel> uploadTasks = new();
        [ObservableProperty]
        public int uploadTaskCurrentPage = 2;
        [ObservableProperty]
        public ObservableCollection<TaskModel> downloadTasks = new();
        [ObservableProperty]
        public int downloadTaskCurrentPage = 2;
        // 自动生成的属性变更回调方法
        partial void OnUploadTaskCurrentPageChanged(int value)
        {

        }
        partial void OnDownloadTaskCurrentPageChanged(int value)
        {

        }

        protected override void OnActivated()
        {
            //注册消息接收
            Messenger.Register<TaskWindowViewModel, UploadProgressMessage, string>(this, "uploadmsg", (r, m) =>
            {
                Dispatcher.UIThread.Post(async () =>
                {
                    var task = UploadTasks.FirstOrDefault(t => t.TaskId == m.taskid);
                    if (task != null)
                    {
                        task.CompletedSize = Convert.ToInt64(m.progress / 100.0 * task.FileSize);
                        task.Status = m.progress >= 100 ? "已完成" : "进行中";
                        // 通知属性变更
                        //var index = UploadTasks.IndexOf(task);
                        //UploadTasks[index] = task; // 触发集合变更通知
                    }
                    else
                    {
                        await LoadedAsync();
                        task = UploadTasks.FirstOrDefault(t => t.TaskId == m.taskid);
                        if (task != null)
                        {
                            task.CompletedSize = Convert.ToInt64(m.progress / 100.0 * task.FileSize);
                            task.Status = m.progress >= 100 ? "已完成" : "进行中";
                        }
                    }
                });

            });

            Messenger.Register<TaskWindowViewModel, DownloadProgressMessage, string>(this, "downloadmsg", (r, m) =>
            {
                Dispatcher.UIThread.Post(async () =>
                {
                    var task = DownloadTasks.FirstOrDefault(t => t.TaskId == m.taskid);
                    if (task != null)
                    {
                        task.CompletedSize = Convert.ToInt64(m.progress / 100.0 * task.FileSize);
                        task.Status = m.progress >= 100 ? "已完成" : "进行中";
                        // 通知属性变更
                        //var index = UploadTasks.IndexOf(task);
                        //UploadTasks[index] = task; // 触发集合变更通知
                    }
                    else
                    {
                        await LoadedAsync();
                        task = UploadTasks.FirstOrDefault(t => t.TaskId == m.taskid);
                        if (task != null)
                        {
                            task.CompletedSize = Convert.ToInt64(m.progress / 100.0 * task.FileSize);
                            task.Status = m.progress >= 100 ? "已完成" : "进行中";
                        }
                    }
                });

            });
        }

        private IFileService _fileService;
        /// <summary>
        /// 窗口的Loaded事件处理程序
        /// </summary>
        /// <returns></returns>
        public async Task LoadedAsync()
        {
            List<TaskModel> tasks = await _fileService.GetAllTaskModelsAsync();
            DownloadTasks = new ObservableCollection<TaskModel>(tasks.Where(t => t.Direction == "下载"));
            UploadTasks = new ObservableCollection<TaskModel>(tasks.Where(t => t.Direction == "上传"));
        }

        public TaskWindowViewModel(IFileService fileService)
        {
            _fileService = fileService;
            //接收消息
            IsActive = true;
            //  InitializeAsync();

            //downloadTasks.Add();
            //UploadTasks.AddRange(
            //    new List<TaskModel>
            //    { 
            //        new TaskModel
            //        {
            //            TaskId = "6",
            //            LocalPath = "C:\\Files\\file3.txt",
            //            RemotePath = "/remote/file3.txt",
            //            FileSize = 8192,
            //            CompletedSize = 2048,
            //            Status = "暂停"
            //        }
            //    }
            //    );
        }
    }
}
