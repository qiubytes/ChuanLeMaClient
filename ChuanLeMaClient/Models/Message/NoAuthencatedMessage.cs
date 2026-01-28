using System;
using System.Collections.Generic;
using System.Text;

namespace ChuanLeMaClient.Models.Message
{
    /// <summary>
    /// 未认证消息
    /// </summary>
    public record NoAuthencatedMessage();
    /// <summary>
    /// 上传未认证消息 
    /// </summary>
    public record DownLoadNoAuthencatedMessage(): NoAuthencatedMessage;
    public record UploadNoAuthencatedMessage();
}
