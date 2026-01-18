using ChuanLeMaClient.Common;
using ChuanLeMaClient.Dtos;
using ChuanLeMaClient.Models;
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
        public FileServiceImpl(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<ResponseResult<List<FolderFileDataModel>>> FileDirList(string workpath)
        {
            HttpClientUtil client = new HttpClientUtil(_configuration);
            ResponseResult<List<FolderFileDataModel>> res = await client.PostRequest<FileListRequestDto, ResponseResult<List<FolderFileDataModel>>>("/File/FileDirList", new FileListRequestDto() { workpath = workpath });
            return res;
        }

    }
}
