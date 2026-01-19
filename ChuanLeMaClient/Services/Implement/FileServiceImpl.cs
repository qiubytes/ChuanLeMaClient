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
        public FileServiceImpl(IConfiguration configuration, ITaskModelRepository taskModelRepository)
        {
            _configuration = configuration;
            _taskModelRepository = taskModelRepository;
        }
        public async Task<ResponseResult<List<FolderFileDataModel>>> FileDirList(string workpath)
        {
            HttpClientUtil client = new HttpClientUtil(_configuration);
            ResponseResult<List<FolderFileDataModel>> res = await client.PostRequest<FileListRequestDto, ResponseResult<List<FolderFileDataModel>>>("/File/FileDirList", new FileListRequestDto() { workpath = workpath });
            return res;
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

    }
}
