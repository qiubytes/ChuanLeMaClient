using ChuanLeMaClient.Models;
using ChuanLeMaClient.Services.Inteface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ChuanLeMaClient.Services.Implement
{
    /// <summary>
    /// 上传服务单实例实现  这里后缀不是Impl 而是 ImplSingleInstance 以示区别 
    /// </summary>
    public class UploadServiceImplSingleInstance : ObservableRecipient, IUploadService
    {
        public UploadServiceImplSingleInstance()
        {
            // 激活消息接收
            IsActive = true;
        }
        public void AddTask(string localfilepath, string remotefilepath, string token)
        {
            string taskid = "2";
            Task.Factory.StartNew(() => DoUpload(localfilepath, remotefilepath, token, taskid));
        }
        public void DoUpload(string localfilepath, string remotefilepath, string token, string taskid)
        {
            while (true)
            {
                // 模拟上传过程
                for (int progress = 0; progress <= 100; progress += 1)
                {
                    // 发送上传进度消息
                    WeakReferenceMessenger.Default.Send(new UploadProgressMessage(taskid,localfilepath, remotefilepath, progress),"uploadmsg");
                    Task.Delay(1000).Wait(); // 模拟上传延迟
                }
                break; // 上传完成后跳出循环
            }
        }
    }
}
