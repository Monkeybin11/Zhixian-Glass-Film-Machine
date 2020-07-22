using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************************
 * CLR    Version：       4.0.30319.42000
 * Class     Name：       CfgAPI
 * Machine   Name：       DESKTOP-RSTK3M3
 * Name     Space：       ProCommon.Communal
 * File      Name：       CfgAPI
 * Creating  Time：       5/19/2020 5:30:58 PM
 * Author    Name：       xYz_Albert
 * Description   ：
 * Modifying Time：
 * Modifier  Name：
*************************************************************************************/

namespace ProCommon.Communal
{
    public delegate void ConfigExceptionOccuredDel(string err);

    public class CfgAPI
    {
        public static event ConfigExceptionOccuredDel ConfigExceptionOcuuredEvt;

        static CfgAPI()
        {
            ConfigExceptionOcuuredEvt = new ConfigExceptionOccuredDel(OnConfigExceptionOccured);
        }

        /// <summary>
        /// 配置文件异常事件回调
        /// </summary>
        /// <param name="err"></param>
        private static void OnConfigExceptionOccured(string err)
        {

        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <typeparam name="T">配置文件类型</typeparam>
        /// <param name="t">配置文件实例</param>
        /// <param name="filePath">保存路径</param>
        /// <returns></returns>
        public static bool Save<T>(T t, string filePath) where T : ProCommon.Communal.Config
        {
            bool rt = false;
            try
            {
                ProCommon.Communal.Serialization.SaveToFile(t, filePath);
                rt = true;
            }
            catch (System.Exception ex)
            {
                if (ConfigExceptionOcuuredEvt != null)
                    ConfigExceptionOcuuredEvt(string.Format("错误：保存配置文件[{0}]失败!\n异常描述:{1}", t.ToString(), ex.Message));
            }
            return rt;
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Load<T>(string filePath) where T : ProCommon.Communal.Config, new()
        {
            try
            {
                return (T)ProCommon.Communal.Serialization.LoadFromFile(typeof(T), filePath);
            }
            catch (System.Exception ex)
            {
                if (ConfigExceptionOcuuredEvt != null)
                    ConfigExceptionOcuuredEvt(string.Format("错误：加载配置文件[{0}]失败!\n异常描述:{1}", (new T()).ToString(), ex.Message));
                return default(T);
            }
        }

    }
}
