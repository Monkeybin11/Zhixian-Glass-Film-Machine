using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       LogWriter
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProCommon.Communal
 * File      Name：       LogWriter
 * Creating  Time：       4/21/2020 1:25:31 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProCommon.Communal
{
    /// <summary>
    /// 软件日志写操作类
    /// 对系统执行过程以及异常进行记录
    /// 写入到本地TXT文档
    /// 文件容量超出2MB字节,删除文件并重新创建
    /// </summary>
    public class LogWriter
    {
        const uint txtContentCapacity = 1; //日志文件的容量,单位MB  

        /// <summary>
        /// 根据指定的格式写日志到指定的文件
        /// </summary>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void WriteLog(string filePath, string format, params object[] args)
        {
            Write(filePath, string.Format(format, args));
        }

        public static void WriteException(string filePath, System.Exception ex)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
                    if (fi.Length > txtContentCapacity * 1024 * 1024)
                    {
                        System.IO.File.Delete(filePath);
                        System.IO.File.Create(filePath);
                    }
                }

                System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, true, new System.Text.UnicodeEncoding());
                sw.WriteLine(DateTime.Now.ToString("[yy-MM-dd HH:mm:ss]"));
                sw.WriteLine(ex.TargetSite);
                sw.WriteLine(ex.StackTrace);
                sw.WriteLine(ex.Message);
                sw.Flush();
                sw.Close();
            }
            catch { }
        }


        /// <summary>
        /// 方法：指定路径文件写入内容并检查是否超容量(2*1024*1024)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        private static void Write(string filePath, string content)
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
                    if (fi.Length > txtContentCapacity * 1024 * 1024)
                    {
                        System.IO.File.Delete(filePath);
                        System.IO.File.Create(filePath);
                    }
                }

                System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, true, new System.Text.UnicodeEncoding());
                sw.WriteLine(content);
                sw.Flush();
                sw.Close();
            }
            catch { }

        }
    }
}
