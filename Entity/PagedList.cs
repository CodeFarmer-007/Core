using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class PagedList<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalItems { get; set; }
        /// <summary>
        /// 总页码
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// 数据实体
        /// </summary>
        public IReadOnlyList<T> Items { get; set; }
    }
}
