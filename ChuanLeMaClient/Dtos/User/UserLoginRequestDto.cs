using System;
using System.Collections.Generic;
using System.Text;

namespace ChuanLeMaClient.Dtos.User
{
    /// <summary>
    /// 登录DTO
    /// </summary>
    public class UserLoginRequestDto
    {
        public UserLoginRequestDto()
        {
        }
        public UserLoginRequestDto(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
