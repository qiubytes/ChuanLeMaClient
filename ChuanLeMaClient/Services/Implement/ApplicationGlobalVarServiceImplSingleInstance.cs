using ChuanLeMaClient.Services.Inteface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChuanLeMaClient.Services.Implement
{
    /// <summary>
    /// 应用程序全局变量服务实现类（单例）
    /// </summary>
    public class ApplicationGlobalVarServiceImplSingleInstance : IApplicationGlobalVarService
    {
        public string UserToken { get; set; }
    }
}
