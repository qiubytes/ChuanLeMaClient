using ChuanLeMaClient.Dtos;
using ChuanLeMaClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Services.Inteface
{
    public interface IFileService
    {
        public Task<ResponseResult<List<FolderFileDataModel>>> FileDirList(string workpath);
        public Task<int> InsertTaskModelAsync(Models.TaskModel model);
        public Task<List<Models.TaskModel>> GetAllTaskModelsAsync();
        public Task<int> UpdateTaskModelAsync(Models.TaskModel model);
        public Task<TaskModel> GetModel(string TaskId);
        /// <summary>
        /// 正在进行中的任务数量
        /// </summary>
        /// <returns></returns>
        public Task<int> GetDoingTaskCount();
    }
}
