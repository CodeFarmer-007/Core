using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.ExpandingMethod
{
    public static class ReturnClass
    {
        public static ResponseRsp<PagedList<T>> ReturnPagedList<T>(this PagedList<T> list) where T : class
        => new ResponseRsp<PagedList<T>>() { ReturnObject = list, Message = (list == null || list.TotalItems == 0) ? "未找到相关内容" : string.Empty };

        public static ResponseRsp<List<T>> ReturnList<T>(this List<T> list) where T : class
       => new ResponseRsp<List<T>>() { ReturnObject = list, Message = (list == null || list.Count == 0) ? "未找到相关内容" : string.Empty };

        public static ResponseRsp<List<KeyValuePair<string, int>>> ReturnKeyValueList(this List<KeyValuePair<string, int>> list)
          => new ResponseRsp<List<KeyValuePair<string, int>>>() { ReturnObject = list, Message = (list == null || list.Count == 0) ? "未找到相关内容" : string.Empty };

        public static ResponseRsp<T> ReturnEntity<T>(this T entity) where T : class
        => new ResponseRsp<T>() { ReturnObject = entity, Message = entity == null ? "未找到相关内容" : string.Empty };

        public static ResponseRsp<bool> ReturnBool(this bool isresult)
        => new ResponseRsp<bool>() { ReturnCode = isresult ? ReturnCode.OK : ReturnCode.Fail, Message = isresult ? "成功" : "失败" };
    }
}
