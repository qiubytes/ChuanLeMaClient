using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Models
{
    /// <summary>
    /// 上传完成消息
    /// </summary>
    /// <param name="taskid"></param>
    /// <param name="localfilepath"></param>
    /// <param name="remotefilepath"></param>
    /// <param name="progress"></param>
    public record UploadCompletedMessage(string taskid, string localfilepath, string remotefilepath);


}
