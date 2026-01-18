using ChuanLeMaClient.Dtos;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Common
{
    public class HttpClientUtil
    {
        private readonly IConfiguration _configuration;
        public HttpClientUtil(IConfiguration configuration)
        {
            // 创建配置构建器
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //    //.AddEnvironmentVariables()
            //    //.AddCommandLine(args)
            //    .Build();
            _configuration = configuration;
        }
        public async Task<V> PostRequest<T, V>(string relativeUrl, T jsonBody)
        {
            string ServerUrl = _configuration["ServerUrl"];
            HttpClient _httpClient = new HttpClient();
            //var jsonobj = new FileDownloadRequestDto { filepath = "qdrant-x86_64-pc-windows-msvc.zip" };
            // 发送POST请求并获取响应流
            using var response = await _httpClient.PostAsync($"{ServerUrl}{relativeUrl}", JsonContent.Create(jsonBody));
            response.EnsureSuccessStatusCode();
            // 获取响应流
            string stringresult = await response.Content.ReadAsStringAsync();
            V res = System.Text.Json.JsonSerializer.Deserialize<V>(stringresult);
            return res;
        }

    }
}
