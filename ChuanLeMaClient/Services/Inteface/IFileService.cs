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
    }
}
