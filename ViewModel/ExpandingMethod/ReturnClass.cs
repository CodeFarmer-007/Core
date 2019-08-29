using Common;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.ExpandingMethod
{
    public static class ReturnClass
    {
        public static ApiResult<PagedList<T>> ReturnPagedList<T>(this PagedList<T> list) where T : class
        => new ApiResult<PagedList<T>>() { data = list, message = (list == null || list.TotalItems == 0) ? "未找到相关内容" : string.Empty };

        public static ApiResult<List<T>> ReturnList<T>(this List<T> list) where T : class
       => new ApiResult<List<T>>() { data = list, message = (list == null || list.Count == 0) ? "未找到相关内容" : string.Empty };

        public static ApiResult<List<KeyValuePair<string, int>>> ReturnKeyValueList(this List<KeyValuePair<string, int>> list)
          => new ApiResult<List<KeyValuePair<string, int>>>() { data = list, message = (list == null || list.Count == 0) ? "未找到相关内容" : string.Empty };

        public static ApiResult<T> ReturnEntity<T>(this T entity) where T : class
        => new ApiResult<T>() { data = entity, message = entity == null ? "未找到相关内容" : string.Empty };

        public static ApiResult<bool> ReturnBool(this bool isresult)
        => new ApiResult<bool>() { returnCode = isresult ? ReturnCode.OK : ReturnCode.Fail, message = isresult ? "成功" : "失败" };
    }
}
