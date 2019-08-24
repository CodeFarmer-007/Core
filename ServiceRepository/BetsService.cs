using Help;
using IService;
using OtherHelp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Bets;

namespace Service
{
    public class BetsService : IBetsService
    {
        private readonly string urlText = AppSettings.GetEntityValue("Bets:UrlAddress");

        private readonly string sessionValue = AppSettings.GetEntityValue("Bets:SessionValue");

        /// <summary>
        /// 下注（m.caikz99.com）
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Bets()
        {
            await Task.Delay(0);

            if (string.IsNullOrWhiteSpace(urlText))
            {
                throw new Exception("网站地址【Bets:UrlAddress】找不到");
            }

            if (string.IsNullOrWhiteSpace(sessionValue))
            {
                throw new Exception("账号Cookie【Bets:SessionValue】找不到");
            }

            var maximumLoss = AppSettings.GetEntityValue("Bets:MaximumLoss");

            //获取当前账号下的余额
            var money = GetThisAccontMoney();  //1

            //规则
            //int[] rule = { 1, 3, 9, 27, 81, 240, 720 };
            int[] rule = { 1, 3, 9, 1, 3, 9 };

            if (money > 0)
            {
                var thisMoney = GetThisAccontMoney();  //1
                int paymentAmount = rule[0];
                int i = 0;

                string type = "大";
                int k = 0;
                while (true)
                {
                    if (thisMoney >= money)
                    {
                        paymentAmount = rule[0];
                    }
                    else
                    {
                        if ((money - thisMoney) >= Convert.ToDecimal(maximumLoss))
                        {
                            throw new Exception($"投资已超出，限额：{maximumLoss}元，已停止投注！");
                        }
                        if (i <= rule.Length - 2)  //6
                        {
                            if (i == 2)
                            {
                                paymentAmount = rule[0];
                                type = "单";
                            }
                            else if (i == 3)
                            {
                                paymentAmount = rule[1];
                                type = "单";
                            }
                            else if (i == 4)
                            {
                                paymentAmount = rule[2];
                                type = "单";
                            }
                            else
                            {
                                paymentAmount = rule[(i + 1)];
                            }
                            i += 1;
                            k += 1;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(GetThisDayMaxIssueNumber()))
                    {
                        //支付实体
                        List<BettingData> list = new List<BettingData>();
                        BettingData bettingData = new BettingData();
                        bettingData.lotteryCode = AppSettings.GetEntityValue("Bets:lotteryCode");
                        bettingData.playDetailCode = bettingData.lotteryCode + "A10";
                        bettingData.bettingNumber = type;
                        bettingData.bettingCount = 1;
                        bettingData.bettingAmount = paymentAmount;
                        bettingData.bettingPoint = "1.0";
                        bettingData.bettingUnit = 1;
                        long number = Convert.ToInt64(GetThisDayMaxIssueNumber());
                        bettingData.bettingIssue = (number + 1).ToString();
                        bettingData.graduationCount = 1;
                        list.Add(bettingData);

                        var encodeString = UrlEncode(list.ToJson()); //Encode参数数据

                        //买
                        var state = BettingAction("bettingData=" + encodeString);

                        //验
                        if (state)
                        {
                            money = GetThisAccontMoney();

                            await Task.Delay(60000);

                            thisMoney = GetThisAccontMoney();

                        }
                        else
                        {
                            throw new Exception("购买失败！");
                        }

                    }
                }

            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取当前账号下的余额
        /// </summary>
        /// <returns></returns>
        public decimal GetThisAccontMoney()
        {
            var url = "https://m.caikz99.com/v1/balance/queryBalanceFront";

            var http = HttpApi.RequestMethod("", url, "", GetSessionInfo(), "Get");

            if (http.StatusCode == HttpStatusCode.OK)
            {
                CurrentAmount currentAmount = http.Html.JsonToEntity<CurrentAmount>();
                if (currentAmount.code == 1)
                {
                    return Convert.ToDecimal(currentAmount.data);
                }
            }
            return 0;
        }

        /// <summary>
        /// 获取期号
        /// </summary>
        /// <returns></returns>
        public string GetThisDayMaxIssueNumber()
        {
            var url = "https://m.caikz99.com/v1/lottery/openResult?lotteryCode=1407&dataNum=10";

            var http = HttpApi.RequestMethod("", url, "", GetSessionInfo(), "Get");

            if (http.StatusCode == HttpStatusCode.OK)
            {
                IssueNumber issue = http.Html.JsonToEntity<IssueNumber>();
                if (issue.code == 1)
                {
                    foreach (var item in issue.data)
                    {
                        return item.issue;
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 下注
        /// </summary>
        /// <returns></returns>
        public bool BettingAction(string pData)
        {
            var url = "https://m.caikz99.com/v1/betting/addBetting";

            var http = HttpApi.RequestMethod("Bets", url, pData, GetSessionInfo());

            if (http.StatusCode == HttpStatusCode.OK)
            {
                PayReturn payReturn = http.Html.JsonToEntity<PayReturn>();
                if (payReturn.code == 1)
                {
                    return true;
                }
            }
            return false;
        }


        public SessionInfo GetSessionInfo()
        {
            SessionInfo sessionInfo = new SessionInfo();
            sessionInfo.CookieID = AppSettings.GetEntityValue("Bets:SessionID");
            sessionInfo.CookieValue = AppSettings.GetEntityValue("Bets:SessionValue");
            sessionInfo.Domain = AppSettings.GetEntityValue("Bets:Domain");
            return sessionInfo;
        }

        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }
    }
}
