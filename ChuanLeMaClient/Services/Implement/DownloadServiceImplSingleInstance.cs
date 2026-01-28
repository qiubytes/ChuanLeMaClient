using AtomUI.Icons.IconPark;
using ChuanLeMaClient.Dtos;
using ChuanLeMaClient.Models;
using ChuanLeMaClient.Models.Message;
using ChuanLeMaClient.Services.Inteface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly IApplicationGlobalVarService _applicationGlobalVarService;
        public DownloadServiceImplSingleInstance(IFileService fileService, IConfiguration configuration, IApplicationGlobalVarService applicationGlobalVarService)
        {
            IsActive = true;
            _fileService = fileService;
            _configuration = configuration;
            _applicationGlobalVarService = applicationGlobalVarService;
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
            string ServerUrl = _configuration["ServerUrl"];
            using HttpClient _httpClient = new HttpClient();

            var jsonobj = new FileDownloadRequestDto { filepath = remotefilepath };
            // 发送POST请求并获取响应流
            // using var response = await _httpClient.PostAsync($"{ServerUrl}/File/downloadfile", JsonContent.Create(jsonobj));
            // response.EnsureSuccessStatusCode();

            // // 获取响应流
            // using var stream = await response.Content.ReadAsStreamAsync();

            // 修改这一行：使用 SendAsync 而不是 PostAsync，并指定 HttpCompletionOption.ResponseHeadersRead
            var request = new HttpRequestMessage(HttpMethod.Post, $"{ServerUrl}/File/downloadfile")
            {
                Content = JsonContent.Create(jsonobj)
            };
            if (!string.IsNullOrEmpty(_applicationGlobalVarService.UserToken))
                request.Headers.Add("Authorization", _applicationGlobalVarService.UserToken);

            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            // 获取响应流
            using var stream = await response.Content.ReadAsStreamAsync();

            // 创建文件流
            using var fileStream = new FileStream(localfilepath, FileMode.Create, FileAccess.Write, FileShare.None);

            // 缓冲区大小（可以根据需要调整）
            var buffer = new byte[81920]; // 80KB
            int bytesRead;
            long downloadedBytes = 0;
            long? totalBytes = response.Content.Headers.ContentLength;
            TaskModel taskModel = await _fileService.GetModel(taskid);
            // 循环读取并写入文件
            while ((bytesRead = await stream.ReadAsync(buffer)) > 0)
            {
                await fileStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                downloadedBytes += bytesRead;

                int progress = Convert.ToInt32((double)downloadedBytes / (double)totalBytes * 100);
                WeakReferenceMessenger.Default.Send(new DownloadProgressMessage(taskid, localfilepath, remotefilepath, progress), "downloadmsg");
                //更新数据库已完成大小
                taskModel.CompletedSize = fileStream.Position;
                await _fileService.UpdateTaskModelAsync(taskModel);
                //Task.Delay(1000).Wait(); // 模拟上传延迟
                // 可以添加进度报告（可选）
                // ReportProgress(fileStream.Position, response.Content.Headers.ContentLength);
            }
            //发送下载完成消息
            taskModel.Status = "已完成";
            fileStream.Flush();
            fileStream.Close(); 
            await _fileService.UpdateTaskModelAsync(taskModel);
            WeakReferenceMessenger.Default.Send(new DownloadCompletedMessage(taskid, localfilepath, remotefilepath), "downloadmsg");
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
            // 修改3：增强版垃圾回收
            //await Task.Delay(200); // 给系统处理时间
            //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);
            //GC.WaitForPendingFinalizers();
            //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);
        }
    }
}
