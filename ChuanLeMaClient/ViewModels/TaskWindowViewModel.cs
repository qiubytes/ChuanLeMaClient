using Avalonia.Threading;
using ChuanLeMaClient.Models;
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
                Dispatcher.UIThread.Post(() =>
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
                });

            });
        }
        
        public TaskWindowViewModel()
        {
            //接收消息
            IsActive = true;

            UploadTasks.AddRange(
                new List<TaskModel>
                {
                    new TaskModel
                    {
                        TaskId = "1",
                        LocalPath = "C:\\Files\\file1.txt",
                        RemotePath = "/remote/file1.txt",
                        FileSize = 2048,
                        CompletedSize = 1024,
                        Status = "进行中"
                    },
                    new TaskModel
                    {
                        TaskId = "2",
                        LocalPath = "C:\\Files\\file2.txt",
                        RemotePath = "/remote/file2.txt",
                        FileSize = 4096,
                        CompletedSize = 0,
                        Status = "已完成"
                    },
                    new TaskModel
                    {
                        TaskId = "3",
                        LocalPath = "C:\\Files\\file3.txt",
                        RemotePath = "/remote/file3.txt",
                        FileSize = 8192,
                        CompletedSize = 2048,
                        Status = "暂停"
                    },
                      new TaskModel
                    {
                        TaskId = "4",
                        LocalPath = "C:\\Files\\file1.txt",
                        RemotePath = "/remote/file1.txt",
                        FileSize = 2048,
                        CompletedSize = 1024,
                        Status = "进行中"
                    },
                    new TaskModel
                    {
                        TaskId = "5",
                        LocalPath = "C:\\Files\\file2.txt",
                        RemotePath = "/remote/file2.txt",
                        FileSize = 4096,
                        CompletedSize = 4096,
                        Status = "已完成"
                    },
                    new TaskModel
                    {
                        TaskId = "6",
                        LocalPath = "C:\\Files\\file3.txt",
                        RemotePath = "/remote/file3.txt",
                        FileSize = 8192,
                        CompletedSize = 2048,
                        Status = "暂停"
                    },
                      new TaskModel
                    {
                        TaskId = "1",
                        LocalPath = "C:\\Files\\file1.txt",
                        RemotePath = "/remote/file1.txt",
                        FileSize = 2048,
                        CompletedSize = 1024,
                        Status = "进行中"
                    },
                    new TaskModel
                    {
                        TaskId = "2",
                        LocalPath = "C:\\Files\\file2.txt",
                        RemotePath = "/remote/file2.txt",
                        FileSize = 4096,
                        CompletedSize = 4096,
                        Status = "已完成"
                    },
                    new TaskModel
                    {
                        TaskId = "3",
                        LocalPath = "C:\\Files\\file3.txt",
                        RemotePath = "/remote/file3.txt",
                        FileSize = 8192,
                        CompletedSize = 2048,
                        Status = "暂停"
                    },
                      new TaskModel
                    {
                        TaskId = "4",
                        LocalPath = "C:\\Files\\file1.txt",
                        RemotePath = "/remote/file1.txt",
                        FileSize = 2048,
                        CompletedSize = 1024,
                        Status = "进行中"
                    },
                    new TaskModel
                    {
                        TaskId = "5",
                        LocalPath = "C:\\Files\\file2.txt",
                        RemotePath = "/remote/file2.txt",
                        FileSize = 4096,
                        CompletedSize = 4096,
                        Status = "已完成"
                    },
                    new TaskModel
                    {
                        TaskId = "6",
                        LocalPath = "C:\\Files\\file3.txt",
                        RemotePath = "/remote/file3.txt",
                        FileSize = 8192,
                        CompletedSize = 2048,
                        Status = "暂停"
                    }
                }
                );
        }
    }
}
