using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Models
{
    /// <summary>
    /// 上传进度消息
    /// </summary>
    /// <param name="taskid"></param>
    /// <param name="localfilepath"></param>
    /// <param name="remotefilepath"></param>
    /// <param name="progress"></param>
    public record UploadProgressMessage(string taskid, string localfilepath, string remotefilepath, long progress);

}
