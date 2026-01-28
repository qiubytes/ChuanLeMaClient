using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Models.Message
{
    /// <summary>
    /// 上传进度消息
    /// </summary>
    /// <param name="taskid"></param>
    /// <param name="localfilepath"></param>
    /// <param name="remotefilepath"></param>
    /// <param name="progress"></param>
    public record UploadProgressMessage(string taskid, string localfilepath, string remotefilepath, long progress);
    /// <summary>
    /// 更新上传状态消息
    /// </summary>
    /// <param name="taskid"></param>
    /// <param name="Status"></param>
    public record UploadUpdateMessage(string taskid, string Status);

}
