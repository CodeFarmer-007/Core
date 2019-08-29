using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public enum LogEnum
    {
        /// <summary>
        /// 保存或添加
        /// </summary>
        [Text("保存或添加")]
        ADD = 1,

        /// <summary>
        /// 更新
        /// </summary>
        [Text("更新/修改")]
        UPDATE = 2,

        /// <summary>
        /// 更新
        /// </summary>
        [Text("审核")]
        AUDIT = 3,

        /// <summary>
        /// 删除
        /// </summary>
        [Text("删除")]
        DELETE = 4,

        /// <summary>
        /// 读取/查询
        /// </summary>
        [Text("读取/查询")]
        RETRIEVE = 5,

        /// <summary>
        /// 登录
        /// </summary>
        [Text("登录")]
        LOGIN = 6,

        /// <summary>
        /// 查看
        /// </summary>
        [Text("查看")]
        LOOK = 7,

        /// <summary>
        /// 更改状态
        /// </summary>
        [Text("更改状态")]
        STATUS = 8,

        /// <summary>
        /// 授权
        /// </summary>
        [Text("授权")]
        AUTHORIZE = 9,

        /// <summary>
        /// 退出登录
        /// </summary>
        [Text("退出登录")]
        LOGOUT = 10,

        /// <summary>
        /// 同步到微信
        /// </summary>
        [Text("同步到微信")]
        ASYWX = 11
    }

    public enum PermissionsEnum
    {
        /// <summary>
        /// 菜单归属角色
        /// </summary>
        [Text("菜单归属角色")]
        MenuToRole = 1,

        /// <summary>
        /// 管理员归属角色
        /// </summary>
        [Text("管理员归属角色")]
        AdminToRole = 2,

        /// <summary>
        /// 菜单上面的按钮功能
        /// </summary>
        [Text("菜单上面的按钮功能")]
        MenuToBtnFun = 3,
    }

    /// <summary>
    /// 店铺活动类型
    /// </summary>
    public enum ActivityTypeEnum
    {
        /// <summary>
        /// 商铺
        /// </summary>
        [Text("商铺")]
        Shops = 1,

        /// <summary>
        /// 商品
        /// </summary>
        [Text("商品")]
        Goods = 2,

        /// <summary>
        /// 地区
        /// </summary>
        [Text("地区")]
        City = 2
    }

    /// <summary>
    /// 店铺活动方式
    /// </summary>
    public enum ActivityMethodEnum
    {
        /// <summary>
        /// 打折
        /// </summary>
        [Text("打折")]
        Discount = 1,

        /// <summary>
        /// 满减
        /// </summary>
        [Text("满减")]
        Full = 2
    }

    /// <summary>
    /// 店铺活动方式
    /// </summary>
    public enum DbOrderEnum
    {
        /// <summary>
        /// 打折
        /// </summary>
        [Text("排序Asc")]
        Asc = 1,

        /// <summary>
        /// 满减
        /// </summary>
        [Text("排序Desc")]
        Desc = 2
    }

    public enum ApiEnum
    {
        /// <summary>
        /// 请求(或处理)成功
        /// </summary>
        [Text("请求(或处理)成功")]
        Status = 200, //请求(或处理)成功

        /// <summary>
        /// 内部请求出错
        /// </summary>
        [Text("内部请求出错")]
        Error = 500, //内部请求出错

        /// <summary>
        /// 未授权标识
        /// </summary>
        [Text("未授权标识")]
        Unauthorized = 401,//未授权标识

        /// <summary>
        /// 请求参数不完整或不正确
        /// </summary>
        [Text("请求参数不完整或不正确")]
        ParameterError = 400,//请求参数不完整或不正确

        /// <summary>
        /// 请求TOKEN失效
        /// </summary>
        [Text("请求TOKEN失效")]
        TokenInvalid = 403,//请求TOKEN失效

        /// <summary>
        /// HTTP请求类型不合法
        /// </summary>
        [Text("HTTP请求类型不合法")]
        HttpMehtodError = 405,//HTTP请求类型不合法

        /// <summary>
        /// HTTP请求不合法,请求参数可能被篡改
        /// </summary>
        [Text("HTTP请求不合法,请求参数可能被篡改")]
        HttpRequestError = 406,//HTTP请求不合法

        /// <summary>
        /// 该URL已经失效
        /// </summary>
        [Text("该URL已经失效")]
        URLExpireError = 407,//HTTP请求不合法
    }
}
