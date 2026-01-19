using ChuanLeMaClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Services.Inteface
{
    public interface IDownloadService
    {
        /// <summary>
        /// 添加下载任务
        /// </summary>
        /// <param name="localfilepath"></param>
        /// <param name="remotefilepath"></param>
        /// <param name="token"></param>
        public void AddTask(FolderFileDataModel filemodel, string localfilepath, string remotefilepath, string token);
    }
}
