using System;
using System.Net;
using ViewModel;

namespace Help
{
    public class HttpApi
    {
        public static HttpResults RequestMethod(string url, string pdata, string method = "Post")
        {
            HttpHelpers helper = new HttpHelpers();//发起请求对象
            HttpItems items = new HttpItems() { Url = url, Postdata = pdata, Method = method };//请求设置对象
            HttpResults hr = new HttpResults();//请求结果

            return helper.GetHtml(items);
        }

        public static HttpResults RequestMethod(string type, string url, string pdata, SessionInfo sessionInfo, string method = "Post")
        {
            HttpItems items;
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(new Cookie() { Name = sessionInfo.CookieID, Value = sessionInfo.CookieValue, Domain = sessionInfo.Domain });

            HttpHelpers helper = new HttpHelpers();//发起请求对象

            string contentType;
            string referer;
            string userAgent;
            if (type == "Bets")
            {
                contentType = "application/x-www-form-urlencoded;charset=UTF-8";
                referer = "https://m.caikz99.com/lottery/K3/1407";
                userAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1";

                items = new HttpItems() { Url = url, Postdata = pdata, Method = method, Container = cookieContainer, ContentType = contentType, Referer = referer, UserAgent = userAgent };//请求设置对象
            }
            else
            {
                items = new HttpItems() { Url = url, Postdata = pdata, Method = method, Container = cookieContainer };//请求设置对象
            }

          
            HttpResults hr = new HttpResults();//请求结果

            return helper.GetHtml(items);
        }
    }
}
