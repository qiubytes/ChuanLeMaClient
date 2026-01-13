using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Models
{
    /// <summary>
    /// 任务Model
    /// </summary>
    public partial class TaskModel : ObservableObject
    {
        public string TaskId { get; set; }
        public string LocalPath { get; set; }
        public string RemotePath { get; set; }
        /// <summary>
        /// 总大小
        /// </summary>
        public long FileSize { get; set; }
        /// <summary>
        /// 进行中、已完成、暂停、停止、失败
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 已完成大小
        /// </summary>
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Progress))]  // ✅ 关键：当Completed改变时，自动通知Progress更新

        public long completedSize;

        // ✅ 计算属性1：进度百分比（只读）
        public double Progress
        {
            get
            {
                if (FileSize == 0) return 0; 
                double baseProgress = (double)CompletedSize / FileSize * 100; 
                return baseProgress;
            }
        }

    }
}
