using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Bets
{
    public class PaymentObject
    {
        public string bettingData { get; set; }
    }

    public class BettingData
    {
        public string lotteryCode { get; set; }
        public string playDetailCode { get; set; }
        /// <summary>
        /// 下注号码
        /// </summary>
        public string bettingNumber { get; set; }
        public int bettingCount { get; set; }
        /// <summary>
        /// 下注金额
        /// </summary>
        public int bettingAmount { get; set; }
        public string bettingPoint { get; set; }
        public int bettingUnit { get; set; }
        public string bettingIssue { get; set; }
        public int graduationCount { get; set; }
    }

}
