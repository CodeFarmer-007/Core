using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Bets
{
    public class IssueNumber
    {
        public int code { get; set; }
        public string msg { get; set; }
        public Datum[] data { get; set; }
    }

    public class Datum
    {
        public string lotteryCode { get; set; }
        public string issue { get; set; }
        public string openNumber { get; set; }
        public string openTime { get; set; }
        public bool open { get; set; }
        public string createdTime { get; set; }


        #region 额外判断
        public string BigOrSmall { get; set; }
        public string SingleOrDouble { get; set; }
        public int SumValue { get; set; }
        #endregion
    }

}
