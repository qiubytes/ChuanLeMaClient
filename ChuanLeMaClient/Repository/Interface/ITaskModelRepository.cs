using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Repository.Interface
{
    public interface ITaskModelRepository
    {
        public Task<List<Models.TaskModel>> GetAllTaskModelsAsync();
        public Task<int> InsertTaskModelAsync(Models.TaskModel model);
        public Task<int> UpdateTaskModelAsync(Models.TaskModel model);
        public Task<Models.TaskModel> GetModel(string TaskId);
    }
}
