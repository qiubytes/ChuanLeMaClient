using System;
using System.Collections.Generic;
using System.Text;

namespace ChuanLeMaClient.Services.Inteface
{
    /// <summary>
    /// 应用程序全局变量服务接口
    /// </summary>
    public interface IApplicationGlobalVarService
    {
        /// <summary>
        /// 用户token
        /// </summary>
        public string UserToken { get; set; }
    }
}
