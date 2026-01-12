using ChuanLeMaClient.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.ViewModels
{
    public partial class TaskWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        public ObservableCollection<TaskModel> uploadTasks = new();
        [ObservableProperty]
        public int uploadTaskCurrentPage = 2;
        // 自动生成的属性变更回调方法
        partial void OnUploadTaskCurrentPageChanged(int value)
        {
         
        }
        public TaskWindowViewModel()
        {
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
