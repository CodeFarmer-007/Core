using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Help;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Timing_Tasks.Models;

namespace Timing_Tasks.Controllers
{
    public class HomeController : Controller
    {
        log4net.ILog log = LogManager.GetLogger(typeof(HomeController));

        //执行定时任务  （loction/hangfire）
        public IActionResult Index()
        {
            //添加任务

            RecurringJob.AddOrUpdate("请求【百度】", () => httpGet("https://www.baidu.com"), Cron.Daily(1, 30), TimeZoneInfo.Local);

            RecurringJob.AddOrUpdate("请求【阿里云】", () => httpGet("https://www.aliyun.com"), Cron.Daily(2, 30), TimeZoneInfo.Local);


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void httpGet(string url)
        {
            try
            {
                HttpHelpers helper = new HttpHelpers();//发起请求对象
                HttpItems items = new HttpItems();//请求设置对象
                HttpResults hr = new HttpResults();//请求结果                               
                items.Url = url; //设置请求地址
                items.Timeout = int.MaxValue;
                hr = helper.GetHtml(items);//发起请求
                string res = hr.Html;//得到请求结果
                log.Info(url + ":" + hr);
            }
            catch (Exception)
            {


            }
        }
    }
}
