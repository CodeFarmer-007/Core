using Entity.Lottery;
using Help;
using IService;
using OtherHelp;
using Service.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using ViewModel.Bets;

namespace Service
{
    public class BetsService : BaseService<LotteryNumber>, IBetsService
    {
        private readonly string urlText = AppSettings.GetEntityValue("Bets:UrlAddress");

        private readonly string sessionValue = AppSettings.GetEntityValue("Bets:SessionValue");


        #region 插入历史号码数据
        public async Task<bool> AddRecord()
        {
            await Task.Delay(0);

            try
            {
                var list = GetIssueNumber();

                List<LotteryNumber> model = new List<LotteryNumber>();
                foreach (var item in list)
                {
                    var InsertNumberState = Db.Queryable<LotteryNumber>().Any(a => a.IssueNumber == item.issue);
                    if (!InsertNumberState)
                    {
                        LotteryNumber lottery = new LotteryNumber();
                        lottery.IssueNumber = item.issue;
                        lottery.One = Convert.ToInt32(item.openNumber.Split(',')[0]);
                        lottery.Two = Convert.ToInt32(item.openNumber.Split(',')[1]);
                        lottery.Three = Convert.ToInt32(item.openNumber.Split(',')[2]);
                        lottery.SumValue = item.SumValue;
                        lottery.BigOrSmall = item.BigOrSmall;
                        lottery.SingleOrDouble = item.SingleOrDouble;
                        lottery.CreatTime = DateTime.Now;
                        model.Add(lottery);
                    }
                }
                var count = Db.Insertable(model).ExecuteCommand();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion


        #region 第一版
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


        #endregion


        #region 第二版

        public async Task<bool> Bets_Lucky()
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

            //要购买的次数
            int PurchaseTimes = 50000;


            for (int i = 0; i < PurchaseTimes; i++)
            {
                //当前账户金额
                decimal money = GetThisAccontMoney();

                int add = i;

                //获取历史期号信息
                var NumberList = GetIssueNumber();

                if (NumberList.Count > 0)
                {
                    //判断历史期号是否存在
                    var HistoryList = Db.Queryable<LotteryNumber>().OrderBy(a => a.IssueNumber, OrderByType.Desc).Skip(0).Take(4).ToList();

                    HistoryList.Reverse();

                    #region 判断要押的值
                    //要买的值
                    string Pledge = "";

                    int big, small, single, doubleInt;
                    big = small = single = doubleInt = 0;

                    foreach (var item in HistoryList)
                    {
                        #region 大/小
                        if (item.SumValue > 10)
                        {
                            small = 0;
                            big += 1;
                        }
                        else
                        {
                            big = 0;
                            small += 1;
                        }
                        #endregion

                        #region 单/双
                        if (item.SingleOrDouble == "单")
                        {
                            doubleInt = 0;
                            single += 1;
                        }
                        else
                        {
                            single = 0;
                            doubleInt += 1;
                        }
                        #endregion

                    }

                    if (big == 3)
                    {
                        Pledge = "大";
                    }
                    else if (small == 3)
                    {
                        Pledge = "小";
                    }
                    else if (single == 3)
                    {
                        Pledge = "单";
                    }
                    else if (doubleInt == 3)
                    {
                        Pledge = "双";
                    }
                    #endregion

                    if (!string.IsNullOrWhiteSpace(Pledge))
                    {
                        //进行购买

                        //支付实体
                        List<BettingData> list = new List<BettingData>();
                        BettingData bettingData = new BettingData();
                        bettingData.lotteryCode = AppSettings.GetEntityValue("Bets:lotteryCode");
                        bettingData.playDetailCode = bettingData.lotteryCode + "A10";
                        bettingData.bettingNumber = Pledge;
                        bettingData.bettingCount = 1;
                        bettingData.bettingAmount = 1;
                        bettingData.bettingPoint = "1.0";
                        bettingData.bettingUnit = 1;
                        long number = Convert.ToInt64(GetThisDayMaxIssueNumber());
                        bettingData.bettingIssue = (number + 1).ToString();

                        var thisBettingIssue = (number + 1).ToString();

                        bettingData.graduationCount = 1;
                        list.Add(bettingData);

                        var encodeString = UrlEncode(list.ToJson()); //Encode参数数据

                        //买
                        var state = BettingAction("bettingData=" + encodeString);
                        if (state)
                        {
                            bool Next = false;

                            while (Next == false)
                            {
                                var thisNumberIssue = GetThisDayMaxIssueNumber();
                                if (thisBettingIssue == thisNumberIssue)
                                {
                                    LotteryNumber lottery = new LotteryNumber();

                                    var thisNumberList = GetIssueNumber();
                                    //判断数据库是否存在 该期号
                                    foreach (var item in thisNumberList)
                                    {
                                        var InsertNumberState = Db.Queryable<LotteryNumber>().Any(a => a.IssueNumber == item.issue);
                                        if (!InsertNumberState)
                                        {
                                            lottery.IssueNumber = item.issue;
                                            lottery.One = Convert.ToInt32(item.openNumber.Split(',')[0]);
                                            lottery.Two = Convert.ToInt32(item.openNumber.Split(',')[1]);
                                            lottery.Three = Convert.ToInt32(item.openNumber.Split(',')[2]);
                                            lottery.SumValue = item.SumValue;
                                            lottery.BigOrSmall = item.BigOrSmall;
                                            lottery.CreatTime = DateTime.Now;
                                            lottery.SingleOrDouble = item.SingleOrDouble;

                                            break;
                                        }
                                    }


                                    #region 是否盈利 并插入数据

                                    lottery.IsOK = true;
                                    lottery.BuyingTime = DateTime.Now;



                                    bool moneyState = false;

                                    while (moneyState == false)
                                    {
                                        decimal thisMoney = GetThisAccontMoney();
                                        if (thisMoney != money)
                                        {
                                            if (thisMoney > money)
                                            {
                                                lottery.IsWin = true;
                                            }
                                            else
                                            {
                                                lottery.IsWin = false;
                                            }
                                            LogHelper.Info("", "期号：" + thisBettingIssue + "，买入号码：" + Pledge + "，是否盈利：" + (lottery.IsWin == true ? "盈利" : "亏"));
                                            moneyState = true;
                                        }
                                    }



                                    Db.Insertable(lottery).ExecuteCommand();


                                    #endregion

                                    Next = true;
                                }
                            }
                        }
                        else
                        {
                            LogHelper.Info("", "购买过程中产生错误!");
                            i = i - 1;
                        }
                    }
                    else
                    {
                        LogHelper.Info("", "不符合购买规则执行跳过");

                        await Task.Delay(58000);

                        var thisRecordState = await AddRecord();
                        if (thisRecordState)
                        {
                            i = i - 1;
                        }
                    }
                }
                else
                {
                    LogHelper.Info("", "账号登录失败!");
                    i = i - 1;
                }
            }

            return true;
        }


        /// <summary>
        /// 获取历史期号信息
        /// </summary>
        /// <returns></returns>
        public List<Datum> GetIssueNumber()
        {
            List<Datum> list = new List<Datum>();
            var url = "https://m.caikz99.com/v1/lottery/openResult?lotteryCode=1407&dataNum=10";

            var http = HttpApi.RequestMethod("", url, "", GetSessionInfo(), "Get");

            if (http.StatusCode == HttpStatusCode.OK)
            {
                IssueNumber issue = http.Html.JsonToEntity<IssueNumber>();
                if (issue.code == 1)
                {
                    foreach (var item in issue.data)
                    {
                        Datum model = new Datum();
                        model.createdTime = item.createdTime;
                        model.issue = item.issue;
                        model.lotteryCode = item.lotteryCode;
                        model.open = item.open;
                        model.openTime = item.openTime;
                        model.openNumber = item.openNumber;

                        var spilt = item.openNumber.Split(',');
                        model.SumValue = Convert.ToInt32(spilt[0]) + Convert.ToInt32(spilt[1]) + Convert.ToInt32(spilt[2]);
                        model.BigOrSmall = model.SumValue > 10 ? "大" : "小";
                        model.SingleOrDouble = (model.SumValue) % 2 == 0 ? "双" : "单";

                        list.Add(model);
                    }
                }
            }
            return list;
        }

        #endregion


        #region 公共方法
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
        #endregion

    }
}
