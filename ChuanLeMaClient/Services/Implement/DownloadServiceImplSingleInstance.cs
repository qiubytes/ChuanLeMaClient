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
    public class DownloadServiceImplSingleInstance : ObservableRecipient, IDownloadService
    {
        public DownloadServiceImplSingleInstance()
        {
            IsActive = true;
        }
        public void AddTask(string localfilepath, string remotefilepath, string token)
        {
            string taskid = "3";
            Task.Factory.StartNew(() => Download(localfilepath, remotefilepath, token, taskid));
        }
        public void Download(string localfilepath, string remotefilepath, string token, string taskid)
        {
            while (true)
            {
                // 模拟上传过程
                for (int progress = 0; progress <= 100; progress += 1)
                {
                    // 发送上传进度消息
                    WeakReferenceMessenger.Default.Send(new DownloadProgressMessage(taskid, localfilepath, remotefilepath, progress), "downloadmsg");
                    Task.Delay(1000).Wait(); // 模拟上传延迟
                }
                break; // 上传完成后跳出循环
            }
        }
    }
}
