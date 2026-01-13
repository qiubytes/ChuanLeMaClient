using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Services.Inteface
{
    public interface IUploadService
    {
        /// <summary>
        /// 添加上传任务
        /// </summary>
        /// <param name="localfilepath"></param>
        /// <param name="remotefilepath"></param>
        /// <param name="token"></param>
        public void AddTask(string localfilepath, string remotefilepath, string token);
    }
}
