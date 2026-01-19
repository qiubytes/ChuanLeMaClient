using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Models
{
    /// <summary>
    /// 下载完成消息
    /// </summary>
    /// <param name="taskid"></param>
    /// <param name="localfilepath"></param>
    /// <param name="remotefilepath"></param>
    public record DownloadCompletedMessage(string taskid, string localfilepath, string remotefilepath);

}
