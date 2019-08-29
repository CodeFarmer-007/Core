using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Common
{
    /// <summary>
    /// API 返回JSON字符串
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResult<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public ApiResult() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnCode"></param>
        /// <param name="message"></param>
        public ApiResult(ReturnCode returnCode, String message)
        {
            this.returnCode = returnCode;
            this.message = message;
        }

        /// <summary>
        /// 返回状态(0:成功; 1:失败;)
        /// </summary>
        public ReturnCode returnCode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public int statusCode { get; set; } = 200;
        /// <summary>
        /// 数据集
        /// </summary>
        public T data { get; set; }
    }

    /// <summary>
    /// 返回状态码
    /// </summary>
    public enum ReturnCode
    {
        [Description("成功")]
        OK = 0,
        [Description("失败")]
        Fail = 1,
        [Description("不合法")]
        Illegal = 2,
        [Description("异常")]
        Exception = 3,
        [Description("操作权限")]
        Operation = 4
    }
}
