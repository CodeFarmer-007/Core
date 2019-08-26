using Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OtherHelp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;

namespace Lottery_Bets.Filter
{
    public class GlobalExceptionsFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        public GlobalExceptionsFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// IExceptionFilter接口实现
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            var json = new JsonErrorResponse();

            json.Message = context.Exception.Message;

            if (context.Exception is ApiException)
            {
                context.ExceptionHandled = true;

                context.Result = new JsonResult(new { ReturnCode = ReturnCode.Fail, Message = context.Exception.Message });

            }
            else
            {
                if (_env.IsDevelopment())
                {
                    json.DevelopmentMessage = context.Exception.StackTrace;
                }
                context.Result = new InternalServerErrorObjectResult(json);

                //记录Log日志
                LogHelper.Error(json.Message, WriteLog(json.Message, context.Exception));

            }

        }

        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string WriteLog(string throwMsg, Exception ex)
        {
            return ($"【自定义错误】：{throwMsg} \r\n【异常类型】：{ex.GetType().Name} \r\n【异常信息】：{ ex.Message} \r\n【异常发生方法】：{ex.TargetSite.Name}  \r\n【堆栈调用】：{ex.StackTrace}");
        }

        /// <summary>
        /// 内部服务器错误对象结果(500错误)
        /// </summary>
        private class InternalServerErrorObjectResult : ObjectResult
        {
            public InternalServerErrorObjectResult(object value) : base(value)
            {
                StatusCode = StatusCodes.Status500InternalServerError;
            }
        }

        /// <summary>
        /// 返回错误消息
        /// </summary>
        private class JsonErrorResponse
        {
            /// <summary>
            /// 生产环境的消息
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// 开发环境的消息
            /// </summary>
            public string DevelopmentMessage { get; set; }
        }

    }
}
