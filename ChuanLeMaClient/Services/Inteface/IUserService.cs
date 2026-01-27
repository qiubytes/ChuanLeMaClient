using ChuanLeMaClient.Dtos;
using ChuanLeMaClient.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Services.Inteface
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public Task<ResponseResult<string>> Login(UserLoginRequestDto requestDto);
    }
}
