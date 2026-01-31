using ChuanLeMaClient.Common;
using ChuanLeMaClient.Dtos;
using ChuanLeMaClient.Models;
using ChuanLeMaClient.Repository.Interface;
using ChuanLeMaClient.Services.Inteface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Services.Implement
{
    public class FileServiceImpl : IFileService
    {
        private readonly IConfiguration _configuration;
        private readonly ITaskModelRepository _taskModelRepository;
        private readonly IApplicationGlobalVarService _applicationGlobalVarService;
        private readonly HttpClientUtil _httpClientUtil;
        public FileServiceImpl(IConfiguration configuration, ITaskModelRepository taskModelRepository, IApplicationGlobalVarService applicationGlobalVarService,HttpClientUtil httpClientUtil)
        {
            _configuration = configuration;
            _taskModelRepository = taskModelRepository;
            _applicationGlobalVarService = applicationGlobalVarService;
            _httpClientUtil = httpClientUtil;
        }
        public async Task<ResponseResult<List<FolderFileDataModel>>> FileDirList(string workpath)
        {
            //HttpClientUtil client = new HttpClientUtil(_configuration, _applicationGlobalVarService);
            ResponseResult<List<FolderFileDataModel>> res = await _httpClientUtil.PostRequest<FileListRequestDto, ResponseResult<List<FolderFileDataModel>>>("/File/FileDirList", new FileListRequestDto() { workpath = workpath });
            return res;
        }

        public async Task<List<TaskModel>> GetAllTaskModelsAsync()
        {
            return await _taskModelRepository.GetAllTaskModelsAsync();
        }

        /// <summary>
        /// 新增任务记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> InsertTaskModelAsync(Models.TaskModel model)
        {
            return await _taskModelRepository.InsertTaskModelAsync(model);
        }

        public async Task<int> UpdateTaskModelAsync(TaskModel model)
        {
            return await _taskModelRepository.UpdateTaskModelAsync(model);
        }
        public async Task<TaskModel> GetModel(string TaskId)
        {
            return await _taskModelRepository.GetModel(TaskId);
        }

        public async Task<int> GetDoingTaskCount()
        {
            List<Models.TaskModel> tasks = await _taskModelRepository.GetAllTaskModelsAsync();
            return tasks.Where(t => t.Status == "进行中").Count();
        }

        public async Task<int> DeleteTaskModelAsync(string taskId)
        {
            return await _taskModelRepository.DeleteTaskModelAsync(taskId);
        }
    }
}
