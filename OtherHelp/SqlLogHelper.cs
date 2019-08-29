using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OtherHelp
{
    public class SqlLogHelper
    {
        public static void InsertLog(string sqltxt, string sqlLogPath)
        {
            StreamWriter sw = File.AppendText(sqlLogPath);
            sw.WriteLine(sqltxt);
            sw.Flush();
            sw.Close();
        }

        public static void InsertSqlLog(string sqltxt)
        {
            try
            {
                var basePath = Directory.GetCurrentDirectory();

                string path = $"{basePath}/SqlLog";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string sqlLogPath = $"{path}/{DateTime.Now.ToString("yyyyMMdd")}_Sql.log";

                if (!File.Exists(sqlLogPath))
                    File.Create(sqlLogPath).Close();


                StreamWriter sw = File.AppendText(sqlLogPath);
                sw.WriteLine(sqltxt);
                sw.Flush();
                sw.Close();
            }
            catch (Exception)
            {
            }
        }
    }
}
