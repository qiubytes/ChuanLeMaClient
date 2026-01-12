using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Models
{
    /// <summary>
    /// 任务Model
    /// </summary>
    public class TaskModel
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

        public long CompletedSize { get; set; }

    }
}
