using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace Entity.Lottery
{
    [SugarTable("LotteryNumber")]
    public class LotteryNumber
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] //是主键, 还是标识列
        public long ID { get; set; }
        /// <summary>
        /// 期号
        /// </summary>
        public string IssueNumber { get; set; }
        /// <summary>
        /// 第一个数字
        /// </summary>
        public int One { get; set; }
        /// <summary>
        /// 第二个数字
        /// </summary>
        public int Two { get; set; }
        /// <summary>
        /// 第三个数字
        /// </summary>
        public int Three { get; set; }
        /// <summary>
        /// 和值
        /// </summary>
        public int SumValue { get; set; }
        /// <summary>
        /// 大/小
        /// </summary>
        public string BigOrSmall { get; set; }
        /// <summary>
        /// 单/双
        /// </summary>
        public string SingleOrDouble { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatTime { get; set; }
        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime BuyingTime { get; set; }
        /// <summary>
        /// 购买是否成功
        /// </summary>
        public bool IsOK { get; set; }
        /// <summary>
        /// 是否盈利
        /// </summary>
        public bool IsWin { get; set; }
    }
}
