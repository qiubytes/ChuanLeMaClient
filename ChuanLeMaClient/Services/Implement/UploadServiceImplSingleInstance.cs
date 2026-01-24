using ChuanLeMaClient.Common;
using ChuanLeMaClient.Models;
using ChuanLeMaClient.Services.Inteface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
namespace ChuanLeMaClient.Services.Implement
{
    /// <summary>
    /// 上传服务单实例实现  这里后缀不是Impl 而是 ImplSingleInstance 以示区别 
    /// </summary>
    public class UploadServiceImplSingleInstance : ObservableRecipient, IUploadService
    {
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;
        public UploadServiceImplSingleInstance(IFileService fileService, IConfiguration configuration)
        {
            // 激活消息接收
            IsActive = true;
            _fileService = fileService;
            _configuration = configuration;
        }
        public void AddTask(FolderFileDataModel filemodel, string localfilepath, string remotefilepath, string token)
        {
            string taskid = Guid.NewGuid().ToString();
            Models.TaskModel taskModel = new Models.TaskModel
            {
                TaskId = taskid,
                LocalPath = localfilepath,
                RemotePath = remotefilepath,
                Direction = "上传",
                Status = "进行中",
                CompletedSize = 0,
                FileSize = filemodel.Size
            };
            _fileService.InsertTaskModelAsync(taskModel);
            Task.Factory.StartNew(async () => DoUpload(localfilepath, remotefilepath, token, taskid));
        }
        public async Task DoUpload(string localfilepath, string remotefilepath, string token, string taskid)
        {
            try
            {
                string ServerUrl = _configuration["ServerUrl"];
                using HttpClient _httpClient = new HttpClient();
                var content = new MultipartFormDataContent();

                long totalBytesRead = 0;
                TaskModel taskModel = await _fileService.GetModel(taskid);
                // 1. 添加 workpath 字符串字段
                content.Add(new StringContent(remotefilepath), "workpath");

                // 手动设置 boundary（某些服务器对 boundary 格式有要求）
                //var boundary = "----WebKitFormBoundary" + DateTime.Now.Ticks.ToString("x");
                //content.Headers.Remove("Content-Type");
                //content.Headers.TryAddWithoutValidation("Content-Type", $"multipart/form-data; boundary={boundary}");

                // 使用文件流添加文件
                using var fileStream = System.IO.File.OpenRead(localfilepath);

                // 包装文件流以监控读取进度
                var progressStream = new ProgressStream(fileStream, async
                    (bytesRead) =>
                {
                    totalBytesRead += bytesRead;
                    var percent = Convert.ToInt64((double)totalBytesRead / fileStream.Length * 100);
                    WeakReferenceMessenger.Default.Send(new UploadProgressMessage(taskid, localfilepath, remotefilepath, percent), "uploadmsg");
                    //((IProgress<double>)progress).Report(percent);
                    //更新数据库已完成大小
                    taskModel.CompletedSize = totalBytesRead;
                    await _fileService.UpdateTaskModelAsync(taskModel);
                });

                var streamContent = new StreamContent(progressStream);

                //streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(streamContent, "File", System.IO.Path.GetFileName(localfilepath));

                var request = new HttpRequestMessage(HttpMethod.Post, $"{ServerUrl}/File/uploadfile")
                {
                    Content = content
                };

                using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                taskModel.Status = "已完成";
                await _fileService.UpdateTaskModelAsync(taskModel);
                //发送上传完成消息
                WeakReferenceMessenger.Default.Send(new UploadCompletedMessage(taskid, localfilepath, remotefilepath), "uploadmsg");
               
            }
            catch (Exception ex)
            {

                throw;
            }


            //while (true)
            //{
            //    // 模拟上传过程
            //    for (int progress = 0; progress <= 100; progress += 1)
            //    {
            //        // 发送上传进度消息
            //        WeakReferenceMessenger.Default.Send(new UploadProgressMessage(taskid, localfilepath, remotefilepath, progress), "uploadmsg");
            //        Task.Delay(1000).Wait(); // 模拟上传延迟
            //    }
            //    break; // 上传完成后跳出循环
            //}
        }
    }
}
