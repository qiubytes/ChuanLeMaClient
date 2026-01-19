using AtomUI.Icons.IconPark;
using ChuanLeMaClient.Dtos;
using ChuanLeMaClient.Models;
using ChuanLeMaClient.Services.Inteface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Services.Implement
{
    public class DownloadServiceImplSingleInstance : ObservableRecipient, IDownloadService
    {
        private readonly IFileService _fileService;
        public DownloadServiceImplSingleInstance(IFileService fileService)
        {
            IsActive = true;
            _fileService = fileService;
        }
        public void AddTask(FolderFileDataModel filemodel, string localfilepath, string remotefilepath, string token)
        {
            string taskid = Guid.NewGuid().ToString();
            Models.TaskModel taskModel = new Models.TaskModel
            {
                TaskId = taskid,
                LocalPath = localfilepath,
                RemotePath = remotefilepath,
                Direction = "下载",
                Status = "进行中",
                CompletedSize = 0,
                FileSize = filemodel.Size
            };
            _fileService.InsertTaskModelAsync(taskModel);
            Task.Factory.StartNew(async () => Download(localfilepath, remotefilepath, token, taskid));
        }
        public async Task Download(string localfilepath, string remotefilepath, string token, string taskid)
        {
            HttpClient _httpClient = new HttpClient();

            var jsonobj = new FileDownloadRequestDto { filepath = "qdrant-x86_64-pc-windows-msvc.zip" };
            // 发送POST请求并获取响应流
            using var response = await _httpClient.PostAsync("http://localhost:5210/File/downloadfile", JsonContent.Create(jsonobj));
            response.EnsureSuccessStatusCode();

            // 获取响应流
            using var stream = await response.Content.ReadAsStreamAsync();

            // 创建文件流
            using var fileStream = new FileStream(Path.GetDirectoryName(localfilepath) + "/xxx.zip", FileMode.Create, FileAccess.Write, FileShare.None);

            // 缓冲区大小（可以根据需要调整）
            var buffer = new byte[81920]; // 80KB
            int bytesRead;

            // 循环读取并写入文件
            while ((bytesRead = await stream.ReadAsync(buffer)) > 0)
            {
                await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                int progress = Convert.ToInt32((float)stream.Position / (float)stream.Length * 100);
                WeakReferenceMessenger.Default.Send(new DownloadProgressMessage(taskid, localfilepath, remotefilepath, progress), "downloadmsg");
                //Task.Delay(1000).Wait(); // 模拟上传延迟
                // 可以添加进度报告（可选）
                // ReportProgress(fileStream.Position, response.Content.Headers.ContentLength);
            }

            //while (true)
            //{
            //    // 模拟上传过程
            //    for (int progress = 0; progress <= 100; progress += 1)
            //    {
            //        // 发送上传进度消息
            //        WeakReferenceMessenger.Default.Send(new DownloadProgressMessage(taskid, localfilepath, remotefilepath, progress), "downloadmsg");
            //        Task.Delay(1000).Wait(); // 模拟上传延迟
            //    }
            //    break; // 上传完成后跳出循环
            //}
        }
    }
}
