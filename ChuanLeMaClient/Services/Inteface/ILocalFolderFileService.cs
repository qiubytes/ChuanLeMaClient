using ChuanLeMaClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Services.Inteface
{
    /// <summary>
    /// 本地文件夹文件服务接口
    /// </summary>
    public interface ILocalFolderFileService
    {
        public List<FolderFileDataModel> GetAllFoldersFiles(string path);
        public void CreateFolder(string path, string folderName);
        public void DeleteFile(string filepath);
    }
}
