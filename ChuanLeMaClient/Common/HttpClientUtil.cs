using ChuanLeMaClient.Dtos;
using ChuanLeMaClient.Services.Inteface;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Common
{
    public class HttpClientUtil
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationGlobalVarService _applicationGlobalVarService;
        public HttpClientUtil(IConfiguration configuration, IApplicationGlobalVarService applicationGlobalVarService)
        {
            // 创建配置构建器
            //var configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            //    //.AddEnvironmentVariables()
            //    //.AddCommandLine(args)
            //    .Build();
            _configuration = configuration;
            _applicationGlobalVarService = applicationGlobalVarService;
        }
        public async Task<V> PostRequest<T, V>(string relativeUrl, T jsonBody)
        {

            string ServerUrl = _configuration["ServerUrl"];
            using HttpClient _httpClient = new HttpClient();
            // 方法1：使用 HttpRequestMessage（推荐）
            var request = new HttpRequestMessage(HttpMethod.Post, $"{ServerUrl}{relativeUrl}");
            // 设置请求体
            request.Content = JsonContent.Create(jsonBody);

            // 设置授权头到请求头（正确的位置）
            if (!string.IsNullOrEmpty(_applicationGlobalVarService.UserToken))
                request.Headers.Add("Authorization", _applicationGlobalVarService.UserToken);

            using var response = await _httpClient.SendAsync(request);

            string stringresult = await response.Content.ReadAsStringAsync();
            V res = System.Text.Json.JsonSerializer.Deserialize<V>(stringresult);
            return res;
        }

    }
}
