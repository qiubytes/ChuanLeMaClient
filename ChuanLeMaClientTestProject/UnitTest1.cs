using ChuanLeMaClient.Common;
using ChuanLeMaClient.Dtos;
using ChuanLeMaClient.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ChuanLeMaClientTestProject
{
    public class Tests
    {
        private IConfiguration _configuration;
        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                //.AddEnvironmentVariables()
                .Build();
            _configuration = configuration;
        }

        [Test]
        public async Task Test1()
        {
            HttpClientUtil client = new HttpClientUtil(_configuration);
            var q = await client.PostRequest<FileListRequestDto, ResponseResult<List<FolderFileDataModel>>>("/File/FileDirList", new FileListRequestDto() { workpath = "/" });
            Assert.Pass();
        }
    }
}
