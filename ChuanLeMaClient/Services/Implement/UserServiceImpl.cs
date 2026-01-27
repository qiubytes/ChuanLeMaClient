using ChuanLeMaClient.Common;
using ChuanLeMaClient.Dtos;
using ChuanLeMaClient.Dtos.User;
using ChuanLeMaClient.Services.Inteface;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Services.Implement
{
    public class UserServiceImpl : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationGlobalVarService _applicationGlobalVarService;
        public UserServiceImpl(IConfiguration configuration, IApplicationGlobalVarService applicationGlobalVarService)
        {
            _configuration = configuration;
            _applicationGlobalVarService = applicationGlobalVarService;
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public async Task<ResponseResult<string>> Login(UserLoginRequestDto requestDto)
        {
            HttpClientUtil httpClientUtil = new HttpClientUtil(_configuration, _applicationGlobalVarService);
            ResponseResult<string> res = await httpClientUtil.PostRequest<UserLoginRequestDto, ResponseResult<string>>("/User/Login", requestDto);
            return res;
        }
    }
}
