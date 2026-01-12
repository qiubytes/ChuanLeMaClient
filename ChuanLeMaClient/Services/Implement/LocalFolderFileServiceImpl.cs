using ChuanLeMaClient.Models;
using ChuanLeMaClient.Services.Inteface;
using Serilog.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Services.Implement
{
    public class LocalFolderFileServiceImpl : ILocalFolderFileService
    {
        public void CreateFolder(string path, string folderName)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string filepath)
        {
            throw new NotImplementedException();
        }

        public List<FolderFileDataModel> GetAllFoldersFiles(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            DirectoryInfo[] dirs = directoryInfo.GetDirectories();
            FileInfo[] files = directoryInfo.GetFiles();
            List<FolderFileDataModel> folderFileDataModels = new List<FolderFileDataModel>();
            foreach (FileInfo file in files)
            {
                folderFileDataModels.Add(new FolderFileDataModel
                {
                    Name = file.Name,
                    Size = (long)file.Length,
                    IsFolder = false,
                    Tags = new List<TagInfo>() { new TagInfo { Name = "文件", Color = "geekblue" } } 
                });
            }
            foreach (DirectoryInfo dir in dirs)
            {
                folderFileDataModels.Add(new FolderFileDataModel
                {
                    Name = dir.Name,
                    Size = 0,
                    IsFolder = true,
                    Tags = new List<TagInfo>() { new TagInfo { Name = "目录", Color = "green" } }
                });
            }
            return folderFileDataModels.OrderByDescending(o => o.IsFolder).ToList();
        }
    }
}
