using MySql.Data.MySqlClient;
using OtherHelp;
using SQLBackup;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace Core
{
    public class DbBackup
    {
        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="path">备份文件地址如D://abc.sql</param>
        /// <returns></returns>
        public static bool BackupDb(object path)
        {
            bool isSuccess = false;

            Stream someStream = new MemoryStream();
            try
            {
                MySqlConnection myconn = new MySqlConnection(AppSettings.GetEntityValue("Bets:UrlAddress"));
                if (myconn.State == ConnectionState.Closed)
                {
                    myconn.Open();
                }
                try
                {
                    using (myconn)
                    {
                        var command = myconn.CreateCommand();
                        var backupProvider = new SqlBackup(command);
                        backupProvider.BackupDb(someStream);

                        isSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error($"BackupDB_备份数据库异常。{ex.Message}", "SQLServerIMPL");
                }
                finally
                {
                    if (myconn.State == ConnectionState.Open)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"BackupDB_备份数据库异常。{ex.Message}", "SQLServerIMPL");
            }

            return isSuccess;
        }


        /// <summary>
        /// 还原数据库
        /// </summary>
        /// <param name="path">指定还原文件***.sql的绝对路径</param>
        /// <param name="dbName">还原到指定数据库</param>
        /// <returns></returns>
        public static bool RestoreDb(string path, string dbName)
        {
            bool isSuccess = false;
            Stream someStream = new MemoryStream();

            try
            {
                MySqlConnection myconn = new MySqlConnection(AppSettings.GetEntityValue("Bets:UrlAddress"));
                if (myconn.State == ConnectionState.Closed)
                {
                    myconn.Open();
                }
                try
                {
                    using (myconn)
                    {
                        var command = myconn.CreateCommand();
                        // To Create a SQLBackup instance, you need an instance of IDBCommand
                        var backupProvider = new SqlBackup(command);
                        backupProvider.RestoreDb(someStream);

                        isSuccess = true;
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error($"BackupDB_还原数据库异常。{ex.Message}", "SQLServerIMPL");
                }
                finally
                {
                    if (myconn.State == ConnectionState.Open)
                    {
                        myconn.Close();
                        myconn.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error($"BackupDB_还原数据库异常。{ex.Message}", "SQLServerIMPL");
            }
            return isSuccess;
        }
    }
}
