using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ViewModel
{
    public class ResponseRsp<T>
    {
        public ResponseRsp() { }
        public ResponseRsp(ReturnCode returnCode, String message)
        {
            this.ReturnCode = returnCode;
            this.Message = message;
        }

        /// <summary>
        /// 返回消息
        /// </summary>
        public String Message { get; set; }
        /// <summary>
        /// 返回数据
        /// </summary>
        public T ReturnObject { get; set; }
        /// <summary>
        /// 返回状态(0:成功; 1:失败;)
        /// </summary>
        public ReturnCode ReturnCode { get; set; }
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
