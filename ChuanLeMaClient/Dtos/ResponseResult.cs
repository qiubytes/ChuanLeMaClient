using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Dtos
{
    public class ResponseResult<T>
    {
        [JsonConstructor]
        public ResponseResult(T data = default(T), int code = 200, string msg = "")
        {
            this.data = data;
            this.code = code;
            this.msg = msg;
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; } = 200;

        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; } = "";

        /// <summary>
        /// 数据
        /// </summary>
        public T data { get; set; }
    }
}
