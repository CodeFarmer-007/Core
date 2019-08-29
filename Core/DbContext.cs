using OtherHelp;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class DbContext
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = AppSettings.GetEntityValue("DbConnection:MySqlConnectionString"),
                DbType = DbType.MySql,
                IsAutoCloseConnection = true
            });

            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                AddSqlLog(sql, pars);
            };
        }
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作


        #region sql日志处理
        void AddSqlLog(string sql, SugarParameter[] pars)
        {

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("**********************************************Sql-Begin**********************************************************************");
                sb.AppendLine(string.Format(@"执行时间：{0}", DateTime.Now));
                sb.AppendLine(string.Format(@"Sql语句：{0}", sql));
                sb.AppendLine(string.Format(@"Sql参数：{0}", Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value))));

                string sqltxt = sql;
                foreach (var item in pars)
                {
                    var value = item.Value + "";
                    if (item.Value.GetType() == typeof(string) || item.Value.GetType() == typeof(DateTime) || item.Value.GetType() == typeof(bool))
                    {
                        value = $"'{item.Value}'";
                    }

                    sqltxt = sqltxt.Replace(item.ParameterName, value);
                }

                sb.AppendLine(string.Format(@"Sql执行语句：{0}", sqltxt));
                sb.AppendLine("**********************************************Sql-End************************************************************************");

                SqlLogHelper.InsertSqlLog(sb.ToString());
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region 注释
        ////微信设置
        //public SimpleClient<WxSetting> WxSettingDb => new SimpleClient<WxSetting>(Db);
        //public SimpleClient<WxMaterial> WxMaterialDb => new SimpleClient<WxMaterial>(Db);

        ////Cms设置
        //public SimpleClient<CmsImage> CmsImageDb => new SimpleClient<CmsImage>(Db);
        //public SimpleClient<CmsAdvClass> CmsAdvClassDb => new SimpleClient<CmsAdvClass>(Db);
        //public SimpleClient<CmsAdvList> CmsAdvListDb => new SimpleClient<CmsAdvList>(Db);
        //public SimpleClient<CmsArticle> CmsArticleDb => new SimpleClient<CmsArticle>(Db);
        //public SimpleClient<CmsColumn> CmsColumnDb => new SimpleClient<CmsColumn>(Db);
        //public SimpleClient<CmsComment> CmsCommentDb => new SimpleClient<CmsComment>(Db);
        //public SimpleClient<CmsDownload> CmsDownloadDb => new SimpleClient<CmsDownload>(Db);
        //public SimpleClient<CmsSite> CmsSiteDb => new SimpleClient<CmsSite>(Db);
        //public SimpleClient<CmsTags> CmsTagsDb => new SimpleClient<CmsTags>(Db);
        //public SimpleClient<CmsTemplate> CmsTemplateDb => new SimpleClient<CmsTemplate>(Db);
        //public SimpleClient<CmsVote> CmsVoteDb => new SimpleClient<CmsVote>(Db);
        //public SimpleClient<CmsVoteItem> CmsVoteItemDb => new SimpleClient<CmsVoteItem>(Db);
        //public SimpleClient<CmsVoteLog> CmsVoteLogDb => new SimpleClient<CmsVoteLog>(Db);

        ////系统权限设置
        //public SimpleClient<SysCode> SysCodeDb => new SimpleClient<SysCode>(Db);
        //public SimpleClient<SysCodeType> SysCodeTypeDb => new SimpleClient<SysCodeType>(Db);
        //public SimpleClient<SysOrganize> SysOrganizeDb => new SimpleClient<SysOrganize>(Db);
        //public SimpleClient<SysLog> SysLogDb => new SimpleClient<SysLog>(Db);
        //public SimpleClient<SysMenu> SysMenuDb => new SimpleClient<SysMenu>(Db);
        //public SimpleClient<SysPermissions> SysPermissionsDb => new SimpleClient<SysPermissions>(Db);
        //public SimpleClient<SysRole> SysRoleDb => new SimpleClient<SysRole>(Db);
        //public SimpleClient<SysAdmin> SysAdminDb => new SimpleClient<SysAdmin>(Db);
        //public SimpleClient<SysAppSetting> SysAppSettingDb => new SimpleClient<SysAppSetting>(Db);
        #endregion

    }
}
